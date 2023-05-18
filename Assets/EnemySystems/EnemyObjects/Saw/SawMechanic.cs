using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SawMechanic : EnemyObject
{
    enum direction
    {
        XAxis,
        YAxis
    }

    [Header("Saw Settings")]
    [SerializeField] private direction directionOfSawDisc = direction.XAxis;
    [SerializeField] private int distance = 4;
    [SerializeField] [Min(0)] private float speed = 2f;

    [SerializeField] private Sprite chainOfSawSprite;
    [SerializeField][Tooltip("If You want use this, min value must be 2")] private int numOfChainSprites = 0;
    private int prevChainNum = 0;
    private List<GameObject> allChains = new List<GameObject>();

    private GameObject chainOfSaw;

    private Vector3 startPos;
    private Vector3 endPos;

    private void Awake()
    {
        startPos = (Vector2)this.transform.position;
        endPos = directionOfSawDisc == direction.XAxis ? new Vector2(startPos.x + distance, startPos.y) : new Vector2(startPos.x, startPos.y + distance);
    }

    private void Start()
    {
        StartCoroutine("MovingToEnd");
    }

    private void SetChain()
    {
        if(allChains.Count>0)
            for(int a= allChains.Count-1; a>-1; a--)
                Destroy(allChains[a].gameObject);

        for (int i=0; i<numOfChainSprites+1; i++)
        {
            Vector2 tempPos = Vector2.Lerp(startPos, endPos, (float)i / (float)numOfChainSprites);
            GameObject temp = Instantiate(new GameObject("ChainDot"), tempPos, this.transform.rotation, chainOfSaw.transform);
            temp.AddComponent<SpriteRenderer>();
            temp.GetComponent<SpriteRenderer>().sprite = chainOfSawSprite;
            allChains.Add(temp);
        }

        prevChainNum = numOfChainSprites;
    }


    private void Update()
    {
#if UNITY_EDITOR
        if (prevChainNum != numOfChainSprites)
            SetChain();
#endif
    }

    private IEnumerator MovingToEnd()
    {
        Vector2 tempA = startPos;
        Vector2 tempB = endPos;
        float timer = 0f;

        while(true)
        {
            if ((timer * Mathf.Abs(speed)) / Mathf.Abs(distance) > 1)
            {
                Vector2 tempC = tempA;
                tempA = tempB;
                tempB = tempC;
                timer = 0;
            }

            this.transform.position = Vector2.Lerp(tempA, tempB, (timer * Mathf.Abs(speed)) / Mathf.Abs(distance));

            timer += Time.deltaTime;

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            if (distance != 0 && speed != 0)
            {
                Handles.DrawDottedLine(startPos, endPos, 4f);
                Gizmos.DrawSphere(startPos, 0.2f);
                Gizmos.DrawSphere(endPos, 0.2f);
            }

        }
        else
        {

            Vector2 tempStartPos = (Vector2)this.transform.position;
            Vector2 tempEndPos = directionOfSawDisc == direction.XAxis ? new Vector2(tempStartPos.x + distance, tempStartPos.y) : new Vector2(tempStartPos.x, tempStartPos.y + distance);

            if (distance != 0 && speed != 0)
            {
                Handles.DrawDottedLine(tempStartPos, tempEndPos, 4f);
                Gizmos.DrawSphere(tempStartPos, 0.2f);
                Gizmos.DrawSphere(tempEndPos, 0.2f);
            }

        }
    }
}
