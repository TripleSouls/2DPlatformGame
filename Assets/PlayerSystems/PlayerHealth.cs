using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health = 6;
    [Space]

    [Header("Health System Settings")]
    [SerializeField] [Min(0f)] private float cooldownTime = 1f;
    public bool canItBeDamaged = true;
    [SerializeField] private UnityEvent dieEvents;

    public void GetDamage(int val = 1)
    {
        if (val <= 0)
            val = 1;

        if (canItBeDamaged)
        {
            health -= val;
            CheackHealth();
            StartCoroutine("CooldownForDamage");
        }
    }

    private void CheackHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    IEnumerator CooldownForDamage()
    {
        float timer = cooldownTime;
        float quarterOfTimer = cooldownTime / 8f;
        float temp = 0f;
        int temp2 = 0;
        bool toggle = false;

        canItBeDamaged = false;
        while(timer>0f)
        {
            if (temp2 < 5)
            {
                this.transform.GetComponent<SpriteRenderer>().color = toggle == false ?
                    Color.Lerp(Color.white, Color.red, temp / quarterOfTimer) :
                    Color.Lerp(Color.red, Color.white, temp / quarterOfTimer);

                if (this.transform.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    toggle = true;
                    temp = 0;
                    temp2++;
                }
                else if (this.transform.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    toggle = false;
                    temp = 0;
                    temp2++;
                }

                temp += Time.deltaTime;
            }
            timer -= Time.deltaTime;
            yield return null;
        }
        canItBeDamaged = true;
    }

    private void Die()
    {
        dieEvents.Invoke();
    }

    public void ResetScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
