using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField] private LayerMask walkableLayers;
    public bool isGrounded;

    private void Start()
    {
        isGrounded = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.gameObject.layer == walkableLayers)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == walkableLayers)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
