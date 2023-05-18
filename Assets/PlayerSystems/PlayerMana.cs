using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Mana Settings")]
    [SerializeField] private bool infiniteMana = false;
    [SerializeField] [Min(0)] private float mana = 7f;
    [SerializeField] [Min(0)] private float maxMana = 7f;
    [SerializeField] [Min(0)] private float cooldownTime = 1f;
    private float _cooldownTimer;
    private bool triggerTheMana = false;

    void Start()
    {
        _cooldownTimer = cooldownTime;

        if(infiniteMana)
        {
            maxMana = 999f;
            mana = 999f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Decrease(6f);
    }

    public void Decrease(float val)
    {
        if (infiniteMana)
            return;

        val = Mathf.Abs(val);
        
        if(mana-val < 0f)
        {
            Debug.Log("[M-2] Mana yetersiz. (Gereken: " + val + " | Mevcut: " + mana+")");
            return;
        }

        mana -= val;

        if(_cooldownTimer<0.9f)
        {
            StopCoroutine("CooldownTimer");
            StartCoroutine("CooldownTimer");
        }
        else if(_cooldownTimer==1f)
        {
            StopCoroutine("CooldownTimer");
            StartCoroutine("CooldownTimer");
        }
    }

    public void Increase(float val)
    {
        if (infiniteMana)
            return;

        val = Mathf.Abs(val);

        if(mana+val>maxMana)
        {
            mana = maxMana;
        }
        else
        {
            mana += val;
        }
    }

    IEnumerator CooldownTimer()
    {
        _cooldownTimer = cooldownTime;

        while(_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer < 0f)
                _cooldownTimer = 0f;

            yield return null;
        }
        StopCoroutine("ManaFuller");
        StartCoroutine("ManaFuller");
    }

    IEnumerator ManaFuller()
    {
        while (mana<maxMana)
        {
            if(_cooldownTimer>0f)
            {
                break;
            }

            mana += Time.deltaTime;

            if (mana > maxMana)
                mana = maxMana;

            yield return null;
        }
    }

    public void Buff(float addingValue)
    {
        if (addingValue <= 0)
        {
            Debug.Log("[M-1.1] BUFF Geçersiz. (" + addingValue + ")");
        }
        else
        {
            maxMana += addingValue;
            Debug.Log("BUFF Baþarýlý. (" + addingValue + ")" + "\t| Max Mana: " + maxMana);
        }
    }

    public void DeBuff(float subtractingValue)
    {
        if(maxMana-Mathf.Abs(subtractingValue) <= 0)
        {
            Debug.Log("[M-1] DEBUFF Geçersiz. (" + subtractingValue + ")");
        }
        else
        {
            if (subtractingValue >= 0)
            {
                maxMana -= subtractingValue;
                Debug.Log("DEBUFF Baþarýlý. (" + subtractingValue + ")" + "\t| Max Mana: " + maxMana);
            }
            else
            {
                maxMana += subtractingValue;
                Debug.Log("DEBUFF Baþarýlý. (" + subtractingValue + ")" + "\t| Max Mana: " + maxMana);
            }
            if (mana > maxMana)
                mana = maxMana;
        }
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetMaxMana()
    {
        return maxMana;
    }

    public bool CheckAndDecrease(float val)
    {
        val = Mathf.Abs(val);
        if(mana>=val)
        {
            Decrease(val);
            return true;
        }
        else
        {
            Debug.Log("[M-CAD] Mana yetersiz. (Gereken: " + val + " | Mevcut: " + mana + ")");
            return false;
        }
    }

}
