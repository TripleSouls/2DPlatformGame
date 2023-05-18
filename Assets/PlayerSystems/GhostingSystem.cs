using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostingSystem : MonoBehaviour
{
    [Header("Ghosting Settings")]
    [SerializeField] private bool ghostingEffectActive = false;
    [SerializeField] private Sprite ghostSprite;
    [SerializeField] private GameObject targetObj;
    [SerializeField][Min(0.001f)] private float delayForGhosting = 0.2f;
    [SerializeField][Min(1)] private int limitForGhosts = 7;
    [SerializeField] private Queue<GameObject> ghostObjs = new Queue<GameObject>();

    // Update is called once per frame
    void Start()
    {
        if(targetObj != null)
        {
            StartCoroutine("Ghosting");
        }
    }

    public void ActivateGhosts()
    {
        ghostingEffectActive = true;
        StartCoroutine("Ghosting");
    }

    public void DeactivateGhosts()
    {
        ghostingEffectActive = false;
        DeleteAllGhosts();
        StopCoroutine("Ghosting");
    }

    public void DeleteAllGhosts()
    {
        for(int i=0; i<ghostObjs.Count; i++)
            if (ghostObjs.Count > limitForGhosts)
                Destroy(ghostObjs.Dequeue());
    }

    IEnumerator Ghosting()
    {
        float timer = 0f;
        while (true)
        {
            if (ghostingEffectActive == false)
                break;

            Vector2 tempPos = targetObj.transform.position;

            while(true)
            {
                timer += Time.deltaTime;
                if(timer>=delayForGhosting)
                {
                    timer -= delayForGhosting;

                    if (Mathf.Approximately(Vector2.Distance((Vector2)targetObj.transform.position, tempPos), 0f) == false)
                    {
                        GameObject temp = new GameObject("Ghost Of "+targetObj.transform.name);
                        temp.AddComponent<SpriteRenderer>().sprite = targetObj.GetComponent<SpriteRenderer>().sprite;
                        temp.transform.position = tempPos;
                        temp.transform.localScale = targetObj.transform.localScale;
                        temp.AddComponent<GhostScript>();
                        temp.GetComponent<GhostScript>().Set(temp.GetComponent<SpriteRenderer>(), delayForGhosting * limitForGhosts);
                        ghostObjs.Enqueue(temp);
                    }

                    if(ghostObjs.Count>limitForGhosts)
                    {
                        Destroy(ghostObjs.Dequeue());
                    }
                    break;
                }
                yield return null;
            }

            yield return null;
        }
    }
}
