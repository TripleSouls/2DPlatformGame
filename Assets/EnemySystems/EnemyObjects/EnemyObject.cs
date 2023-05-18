using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField][Min(1)] private int damageVal = 2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == playerLayer)
        {
            collision.transform.GetComponent<PlayerHealth>().GetDamage(damageVal);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().GetDamage(damageVal);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.gameObject.layer == playerLayer)
        {
            collision.transform.GetComponent<PlayerHealth>().GetDamage(damageVal);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerHealth>().GetDamage(damageVal);
        }
    }
}
