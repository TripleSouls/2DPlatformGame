using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolEnemy : DynamicEnemy
{
    private Rigidbody2D rb;
    private bool canMove = true;

    [Header("Direction")]
    [SerializeField] private bool isFaceRight = true;
    [Space]

    [Header("Patrol Settings")]
    [SerializeField][Min(0)] private float speed = 2f;
    [SerializeField][Min(0)] private float maxDistanceForTurn = 1f;
    [Space]

    [Header("Layers Settings")]
    [SerializeField] private LayerMask walkableLayers;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private LayerMask playerLayer;
    [Space]

    [Header("Floor Detector Settings")]
    [SerializeField] private Vector2 offset;
    [Space]

    [Header("Enemy Settings")]
    [SerializeField] [Min(1)] private int damageVal = 2;

    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        
    }

    protected void Update()
    {
        CheckForAttack();
    }

    protected void FixedUpdate()
    {
        Movement();
    }
    
    private void CheckForAttack()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(this.transform.position, new Vector2(1.1f, 1.1f), 0f, playerLayer);

        if (playerCollider != null)
            playerCollider.GetComponent<PlayerHealth>().GetDamage(damageVal);
    }

    private void CheckingsForPatrolling()
    {
        Vector2 bottomPos = (Vector2)this.transform.position + (Vector2)(1f * (isFaceRight ? this.transform.right : this.transform.right * -1f) + this.transform.up * -1f);
        Vector2 rayDir = isFaceRight ? this.transform.right : this.transform.right * -1f;

        Collider2D[] bottomCollider = Physics2D.OverlapBoxAll(bottomPos, Vector2.one, 0f, walkableLayers);
        RaycastHit2D[] faceRay = Physics2D.RaycastAll((Vector2)this.transform.position + Vector2.right * (isFaceRight ? this.transform.GetComponent<Collider2D>().bounds.size.x / 2 : -1f * this.transform.GetComponent<Collider2D>().bounds.size.x / 2), rayDir, maxDistanceForTurn, obstacleLayers);
        Debug.DrawRay((Vector2)this.transform.position + Vector2.right * (isFaceRight ? this.transform.GetComponent<Collider2D>().bounds.size.x / 2 : -1f * this.transform.GetComponent<Collider2D>().bounds.size.x / 2), rayDir * maxDistanceForTurn, Color.green);

        if (faceRay == null || faceRay.Length == 0)
        {
            if (bottomCollider != null && bottomCollider.Length > 0)
            {
                canMove = true;
            }
            else
            {
                canMove = false;
                Turn();
                return;
            }
        }
        else
        {
            Turn();
            return;
        }
    }

    private void Turn()
    {
        isFaceRight = !isFaceRight;

        this.transform.localScale = new Vector2(
            isFaceRight ? (MyUtils.GetSign(this.transform.localScale.x) == 1 ? this.transform.localScale.x * -1f : this.transform.localScale.x) :
            (MyUtils.GetSign(this.transform.localScale.x) == -1 ? this.transform.localScale.x * -1f : this.transform.localScale.x),
            1f);
    }

    private void Movement()
    {
        CheckingsForPatrolling();

        Collider2D[] floor = Physics2D.OverlapBoxAll((Vector2)this.transform.position - offset, new Vector2(1f, 0.1f), 0, walkableLayers);

        if (floor == null || floor.Length == 0)
        {
            Vector2 temp = rb.velocity;
            temp.y = -9.8f;
            rb.velocity = temp;
        }

        if (floor != null && floor.Length > 0)
        {
            Vector2 temp = rb.velocity;
            temp.y = 0f;
            rb.velocity = temp;
        }

        if (canMove)
        {
            Vector2 move = isFaceRight ? speed * this.transform.right : -1f * speed * this.transform.right;
            move += rb.velocity.y * (Vector2)this.transform.up;
            rb.velocity = move;
        }
    }
}
