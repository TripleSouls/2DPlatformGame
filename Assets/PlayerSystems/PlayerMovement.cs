using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [HideInInspector] public PlayerState playerState = PlayerState.Idle;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private bool isGrounded = false;
    [HideInInspector] public bool IsGrounded => isGrounded;
    [SerializeField] private bool isJumpable = true;
    [Space]

    private bool isFaceDirRight = true;
    [HideInInspector] public bool IsFaceDirRight => isFaceDirRight;

    private Rigidbody2D rb;
    [Header("Rigidbody Settings")]
    [SerializeField] private Collider2D[] footCollider;
    [SerializeField] private LayerMask walkableLayers;
    [Space]

    [Header("CheckTimer")]
    [SerializeField] [Min(0)] private float jumpCheckTimer = 0.1f;
    [SerializeField] private float jumpCheckTimerVal = 0.1f;

    private PlayerMana playerMana;

    [Header("Mana Variables")]
    [SerializeField] [Min(0)] private float manaForRun = 0.5f;
    [SerializeField] [Min(0)] private float manaForJump = 1f;


    Vector2 inputs = new Vector2();

    private bool runKey = false;
    private bool jumpKey = false;
    private bool jumped = false;
    private bool jumpInterrupt = false;
    private bool grabTrigger = false;

    [Header("Grab System")]
    [SerializeField] private bool isGrabbed = false;
    [SerializeField] private LayerMask grabbableLayers;
    [SerializeField] private float grabPower = 5f;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerMana = this.GetComponent<PlayerMana>();
        jumpCheckTimerVal = jumpCheckTimer;
    }

    void Start()
    {
        StartCoroutine("AnimationSystem");
    }

    void Update()
    {
        Inputs();

        if (Input.GetKey(KeyCode.O))
            playerMana.Decrease(3f * Time.deltaTime);

        MovementState();
    }

    private void MovementState()
    {
        if (isGrounded)
        {
            if (inputs.x == 0f)
            {
                playerState = PlayerState.Idle;
            }
            else
            {
                playerState = PlayerState.Run;
            }
        }
        else
        {
            if (rb.velocity.y > 0.01f)
            {
                playerState = PlayerState.Jump;
            }
            else
            {
                playerState = PlayerState.Fall;
            }
        }
    }


    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if(rb.velocity.y < 0)
        {
            Vector2 vel = rb.velocity;
            vel.y -= 3f*Time.deltaTime;
            if (vel.y < -11f)
                vel.y = -11f;
            rb.velocity = vel;
        }

        SetFaceDirection();
        Vector2 move = inputs.x * speed * this.transform.right;
        rb.velocity = move + new Vector2(0f, rb.velocity.y);
        JumpFunctions();
    }

    IEnumerator AnimationSystem()
    {
        while(true)
        {
            yield return null;
        }
    }

    private void Inputs()
    {
        inputs = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (Input.GetKeyDown(KeyCode.Space) && isJumpable)
            jumpKey = true;

        if(Input.GetKeyUp(KeyCode.Space) && jumped)
        {
            jumpInterrupt = true;
        }

    }

    private void JumpFunctions()
    {

        if(jumpInterrupt)
        {
            jumpInterrupt = false;
            if (rb.velocity.y > 0f)
            {
                Vector2 vel = rb.velocity;
                vel.y = Mathf.Lerp(vel.y, 0f, 0.5f);
                rb.velocity = vel;
            }
        }

        footCollider = Physics2D.OverlapBoxAll(
            (Vector2)this.transform.position + new Vector2(0, -1f * this.transform.localScale.y / 2),
            new Vector2(0.5f, 0.05f),
            0f,
            walkableLayers);



        if (footCollider.Length == 0)
        {
            isGrounded = false;
            if (jumpCheckTimerVal > 0f)
            {
                if (jumped)
                {
                    jumpCheckTimerVal = 0f;
                }
                else
                {
                    jumpCheckTimerVal -= Time.fixedDeltaTime;
                    if (jumpCheckTimerVal < 0)
                        jumpCheckTimerVal = 0;
                }
            }
        }
        else
        {
            isGrounded = true;
            if (jumped)
                jumped = false;
            jumpCheckTimerVal = jumpCheckTimer;
        }

        if (jumped == false)
            if (isGrounded)
                isJumpable = true;
            else
                if (jumpCheckTimerVal > 0)
                isJumpable = true;
            else
                isJumpable = false;

        if (jumpKey && jumped == false)
        {
            if (playerMana.CheckAndDecrease(manaForJump)==false)
            {
                jumped = false;
            }
            else
            {
                float velocityForSpesificJumpHeight = Mathf.Sqrt(Mathf.Abs(jumpHeight * 2 * Physics2D.gravity.y));
                rb.velocity = rb.velocity + (Vector2)this.transform.up * velocityForSpesificJumpHeight;
                jumpCheckTimerVal = 0f;
                jumped = true;
            }
            jumpKey = false;
        }

        if (jumpKey && jumped)
            jumpKey = false;
    }

    private void SetFaceDirection()
    {
        if (inputs.x < 0)
        {
            isFaceDirRight = false;
        }
        else if (inputs.x > 0)
        {
            isFaceDirRight = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(
            (Vector2)this.transform.position + new Vector2(0, -1f * this.transform.localScale.y/2),
            new Vector2(0.5f, 0.05f));
    }
}
