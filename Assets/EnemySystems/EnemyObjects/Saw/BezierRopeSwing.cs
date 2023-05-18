using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRopeSwing : MonoBehaviour
{
    [SerializeField] private Transform hingePoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] [Min(0)] private float speedOfSwing = 1f;
    [SerializeField] [Range(0, 1)] private float timer = 0f;
    [SerializeField] private bool toggle = false;
    [SerializeField] [Min(0)] private float waitTime = 0.1f;
    [SerializeField] Transform debugObj;

    private void Start()
    {
        StartCoroutine("Swing");
    }

    private void Update()
    {
        debugObj.transform.position = getSwingPos();
        
    }

    IEnumerator Swing()
    {
        while(true)
        {
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

    private Vector2 getSwingPos()
    {
        Vector2 posA = Vector2.Lerp(startPoint.transform.position, new Vector2(hingePoint.transform.position.x, hingePoint.transform.position.y * -1), timer);
        Vector2 posB = Vector2.Lerp(new Vector2(hingePoint.transform.position.x, hingePoint.transform.position.y * -1), endPoint.transform.position, timer);
        return Vector2.Lerp(posA, posB, getVal(timer));
    }

    private float getVal(float timeV)
    {
        //-3.2*X^3 + 4.8f * x^2 - 0.6*x
        //3*x^2 - 2*x^3

        return ((3 * timeV * timeV)-(2 * timeV * timeV * timeV));
    }
}
