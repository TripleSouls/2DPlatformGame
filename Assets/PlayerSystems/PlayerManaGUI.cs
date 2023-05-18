using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManaGUI : MonoBehaviour
{
    [Header("GUI Settings")]
    [SerializeField] private PlayerMana manaSystem;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Image manaColor;
    [SerializeField] private Color maxColor = new Color(1f, 234f, 255f, 1f);
    [SerializeField] private Color minColor = new Color(255f, 21f, 1f, 1f);


    void Start()
    {
        manaColor.color = Color.Lerp(minColor, maxColor, 1);
        StartCoroutine("GUIUpdate");
    }

    IEnumerator GUIUpdate()
    {
        while(true)
        {
            float tempA = manaSystem.GetMana();
            float tempB = manaSystem.GetMaxMana();
            manaSlider.value = Mathf.Lerp(manaSlider.value, (float)tempA / (float)tempB, Time.deltaTime/0.1f);

            if ((float)tempA / (float)tempB == 0)
                manaSlider.value = 0;
            else if ((float)tempA / (float)tempB == 1)
                manaSlider.value = 1;

            manaColor.color = Color.Lerp(minColor, maxColor, (float)tempA / (float)tempB);
            yield return null;
        }
    }
}
