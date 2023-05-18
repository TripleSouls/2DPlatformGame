using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    [Header("Animation Settings")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite jumpingSprite;
    [SerializeField] private Sprite fallingSprite;

    private bool isFaceDirRight => this.GetComponent<PlayerMovement>().IsFaceDirRight;
    private PlayerMovement playerMovement => this.GetComponent<PlayerMovement>();


    private void Start()
    {
        if(this.TryGetComponent<Animator>(out Animator _animator))
            animator = _animator;
    }

    void Update()
    {
        SetAnimation();
        SetPlayerSpriteDirection();
    }

    private void SetAnimation()
    {
        if (playerMovement.playerState == PlayerState.Idle)
        {
            animator.enabled = true;
            this.GetComponent<SpriteRenderer>().sprite = idleSprite;
            animator.Play("Idle");
        }
        else if (playerMovement.playerState == PlayerState.Run)
        {
            animator.enabled = true;
            animator.Play("Run");
        }
        else if (playerMovement.playerState == PlayerState.Jump)
        {
            animator.enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = jumpingSprite;
        }
        else if (playerMovement.playerState == PlayerState.Fall)
        {
            animator.enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = fallingSprite;
        }
    }

    private void SetPlayerSpriteDirection()
    {
        if (isFaceDirRight == false)
        {
            if (MyUtils.GetSign(this.transform.localScale.x) != -1f)
                this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (isFaceDirRight == true)
        {
            if (MyUtils.GetSign(this.transform.localScale.x) == -1f)
                this.transform.localScale = new Vector3(this.transform.localScale.x * -1f, this.transform.localScale.y, this.transform.localScale.z);
        }
    }
}
