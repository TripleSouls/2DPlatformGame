using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public float timerForLerp = 1f;
    public SpriteRenderer spriteForLerp;

    public GhostScript(SpriteRenderer spriteRenderer, float timer)
    {
        spriteForLerp = spriteRenderer;
        timerForLerp = timer;
    }

    private void Start()
    {
        StartCoroutine("LerpThisSprite");
    }

    public void Set(SpriteRenderer spriteRenderer, float timer)
    {
        spriteForLerp = spriteRenderer;
        timerForLerp = timer;
    }

    IEnumerator LerpThisSprite()
    {
        float timer = 0;
        while(true)
        {

            spriteForLerp.color = Color.Lerp(Color.white, new Color(0f, 0f, 0f, 0f), (float)timer / (float)timerForLerp);
            if(timer>=timerForLerp)
            {
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
