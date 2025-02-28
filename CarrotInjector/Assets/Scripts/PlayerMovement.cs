using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput input;

    public InputActionAsset playerActions;

    public InputAction move;

    public InputAction jump;

    public int maxJumps = 2;

    public Animator animator;
    
    public float jumpPower = 250;

    public float maxSpeed = 5;

    public float debounce = 0.3f;

    public float accel = 100;
    
    public float decel = 200;

    SpriteRenderer sprite;

    float jumptime = 0;

    Rigidbody2D body;
    public bool isGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerActions = input.actions;
        move = playerActions.FindAction("Move");
        jump = playerActions.FindAction("Jump");
    }

    void OnEnable()
    {
        
    }

    void doJump(int jumpCount) {
        animator.SetInteger("Jumps", jumpCount+1);
        animator.SetBool("Jumping", true);
        jumptime = Time.time;
        print("Jumping");
        isGrounded = false;
        body.AddForceY(jumpPower);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInfo = move.ReadValue<Vector2>();
        float xMove = moveInfo.x;
        float jumpInfo = jump.ReadValue<float>();
        int jumpCount = animator.GetInteger("Jumps");
        bool tryJump = moveInfo.y > 0 || jumpInfo > 0;
        float timeSinceJump = Time.time - jumptime;
        bool canJump = timeSinceJump >= debounce;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collision in collisions) {
            if (collision.gameObject.tag == "Floor" && body.linearVelocityY <= 0 && canJump) {
                if(isGrounded == false) {
                    body.linearVelocityY = 0;
                    isGrounded = true;
                    animator.SetInteger("Jumps", 0);
                    animator.SetBool("Jumping", false);
                }
            }
        }
        if (tryJump && canJump) {
            if (jumpCount < maxJumps || isGrounded) {
                doJump(jumpCount);
            }
        }
        float goalSpeed = xMove * maxSpeed;
        if (xMove != 0) {
            animator.SetBool("Walking", true);
        } else {
            animator.SetBool("Walking", false);
        }

        if (xMove > 0) {
            sprite.flipX = false;
        } else if (xMove < 0) {
            sprite.flipX = true;
        }
        print(goalSpeed);
        if (goalSpeed == 0 && Math.Abs(body.linearVelocityX) > 0) {
            int sign = -1 * Math.Sign(body.linearVelocityX);
            body.linearVelocityX += Math.Min(decel, Math.Abs(body.linearVelocityX)) * sign;
        } else if (Math.Abs(goalSpeed) > Math.Abs(body.linearVelocityX) || Math.Sign(goalSpeed) != Math.Sign(body.linearVelocityX)) {
            body.AddForceX(accel * xMove);
        }
        if(Math.Abs(goalSpeed - body.linearVelocityX) < 0.1f) {
            body.linearVelocityX = goalSpeed;
        }
    }
}
