using UnityEngine;
using System;

public class PlayerMovementState : MonoBehaviour
{
    public enum MoveState { idle, run, jump, double_jump, wall_jump, fall }
    public MoveState CurrentMoveState { get; private set; }

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;

    private const string idleAnm = "idle";
    private const string runAnm = "run";
    private const string jumpAnm = "jump";
    private const string dubleJumpAnm = "double jump";
    private const string wallJumpAnm = "wall jump";
    private const string fallAnm = "fall";
    public static Action<MoveState> OnPlayerMoveStateChanged;

    //public Action<MoveState> OnPlayerJumpStateChanged;
    private float xPosLastFrame;

    private void Update()
    {
        if (transform.position.x == xPosLastFrame && rigidBody.linearVelocityY == 0)
        {
            setMoveState(MoveState.idle);
        }
        else if(transform.position.x != xPosLastFrame && rigidBody.linearVelocityY == 0)
        {
            setMoveState(MoveState.run);
        }
        else if( rigidBody.linearVelocityY < 0)
        {
            setMoveState(MoveState.fall);
        }

            xPosLastFrame = transform.position.x;
    }

    public void setMoveState(MoveState moveState)
    {
        if (moveState == CurrentMoveState) return;

        switch (moveState)
        {
            case MoveState.idle:
                HandleIdle();
            break;

            case MoveState.run:
                HandleRun();
            break;

            case MoveState.jump:
                HandleJump();
            break;

            case MoveState.double_jump:
                HandleDubleJump();
            break;

            case MoveState.wall_jump:
                HandleWallJump();
            break;

            case MoveState.fall:
                HandleFall();
            break;

            default:
                Debug.LogError($"Invalid State: {moveState}");
                break;
        }

        OnPlayerMoveStateChanged?.Invoke( moveState );
        CurrentMoveState = moveState;
    }
    private void HandleIdle()
    {
        animator.Play(idleAnm);
    }
    private void HandleRun()
    {
        animator.Play(runAnm);
    }
    private void HandleJump()
    {
        animator.Play(jumpAnm);

    }
    private void HandleDubleJump()
    {
        animator.Play(dubleJumpAnm);

    }
    private void HandleWallJump()
    {
        animator.Play(wallJumpAnm);

    }
    private void HandleFall()
    {
        animator.Play(fallAnm);

    }

}
