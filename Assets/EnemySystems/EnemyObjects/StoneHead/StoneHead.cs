using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHead : MonoBehaviour
{
    [SerializeField] private LayerMask allLayers;
    [SerializeField] private LayerMask activateLayers;

    [SerializeField] private bool workWithUp = true;
    [SerializeField] private bool workWithDown = true;
    [SerializeField] private bool workWithLeft = true;
    [SerializeField] private bool workWithRight = true;

    [SerializeField] private float maxWorkingDistance = 5f;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool smashIt = false;

    private bool isBusy = false;


    private bool activeDir;


    private void Awake()
    {
        startPos = this.transform.position;
    }

    IEnumerator SmashIt()
    {
        bool toggle = false;
        float timer = 0f;
        isBusy = true;
        while (isBusy == true)
        {
            if(toggle==false)
            {
                this.transform.position = Vector2.Lerp(startPos, endPos, timer);
                if (Mathf.Approximately(Vector2.Distance(this.transform.position,endPos), 0))
                {
                    toggle = true;
                    timer = 0;
                }
            }
            else
            {
                this.transform.position = Vector2.Lerp(endPos, startPos, timer);
                if (Mathf.Approximately(Vector2.Distance(this.transform.position, startPos), 0))
                {
                    isBusy = false;
                    smashIt = false;
                    break;
                }
            }

            timer += Time.deltaTime;
            if (timer > 1)
                timer = 1;

            yield return null;
        }


    }

    private void Update()
    {

        if (smashIt)
            return;

        RaycastHit2D tempUp = Physics2D.BoxCast(startPos, Vector2.one, 0f, this.transform.up, maxWorkingDistance, allLayers);
        RaycastHit2D tempDown = Physics2D.BoxCast(startPos, Vector2.one, 0f, this.transform.up * -1f, maxWorkingDistance, allLayers);
        RaycastHit2D tempLeft = Physics2D.BoxCast(startPos, Vector2.one, 0f, this.transform.right * -1f, maxWorkingDistance, allLayers);
        RaycastHit2D tempRight = Physics2D.BoxCast(startPos, Vector2.one, 0f, this.transform.right, maxWorkingDistance, allLayers);

        checkAndSmash(workWithUp, tempUp);
        checkAndSmash(workWithDown, tempDown);
        checkAndSmash(workWithLeft, tempLeft);
        checkAndSmash(workWithRight, tempRight);

    }

    private void checkAndSmash(bool workWithDir, RaycastHit2D temp)
    {
        if (workWithDir && temp.transform != null && isBusy == false)
        {
            if ((activateLayers.value & (1 << temp.transform.gameObject.layer)) != 0)
            {
                endPos = new Vector2(this.transform.position.x, temp.point.y);
                smashIt = true;
                StartCoroutine("SmashIt");
                Debug.Log("EZDI");
            }
        }
    }
}
