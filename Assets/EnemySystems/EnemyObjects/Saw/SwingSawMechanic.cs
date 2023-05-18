using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SwingSawMechanic : MonoBehaviour
{
    [Header("Saw")]
    [SerializeField] Transform sawObj;
    [Space]

    [Header("Pos")]
    [SerializeField] private Transform hingePoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [Space]

    [Header("Settings")]
    [SerializeField] [Min(0)] private float speedOfSwing = 1f;
    private float timer = 0f;
    private bool toggle = false;
    [SerializeField] [Min(0)] private float waitTime = 0.1f;

    private void Start()
    {
        StartCoroutine("Swing");
    }


    IEnumerator Swing()
    {
        while (true)
        {

            sawObj.transform.localPosition = GetSwingPos();

            if (toggle == false)
            {
                timer += Time.deltaTime * speedOfSwing;
                if (timer >= 1)
                {
                    timer = 1;
                    toggle = !toggle;
                    yield return new WaitForSeconds(waitTime);
                }
            }
            else
            {
                timer -= Time.deltaTime * speedOfSwing;
                if (timer <= 0)
                {
                    timer = 0;
                    toggle = !toggle;
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }
    }

    private Vector2 GetSwingPos()
    {
        Vector2 posA = Vector2.Lerp(startPoint.transform.localPosition, new Vector2(hingePoint.transform.localPosition.x, hingePoint.transform.localPosition.y * -1), timer);
        Vector2 posB = Vector2.Lerp(new Vector2(hingePoint.transform.localPosition.x, hingePoint.transform.localPosition.y * -1), endPoint.transform.localPosition, timer);
        return Vector2.Lerp(posA, posB, GetVal(timer));
    }

    private float GetVal(float timeV)
    {
        return ((3 * timeV * timeV) - (2 * timeV * timeV * timeV));
    }

    private void OnDrawGizmos()
    {

        Vector3[] tempPoints = new Vector3[11];
        for (int a = 0; a < 11; a++)
        {
            Vector2 posA = Vector2.Lerp(startPoint.transform.localPosition, new Vector2(hingePoint.transform.localPosition.x, hingePoint.transform.localPosition.y * -1), (float)a / 10f);
            Vector2 posB = Vector2.Lerp(new Vector2(hingePoint.transform.localPosition.x, hingePoint.transform.localPosition.y * -1), endPoint.transform.localPosition, (float)a / 10f);
            tempPoints[a] = Vector2.Lerp((Vector2)this.transform.position+posA, (Vector2)this.transform.position + posB, (float)a / 10f);
        }
        for (int a = 0; a < 10; a++)
        {
            Gizmos.DrawLine(tempPoints[a], tempPoints[a + 1]);
        }
    }
}
