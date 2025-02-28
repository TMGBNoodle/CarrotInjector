using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput input;

    public InputActionAsset playerActions;

    InputAction move;

    InputAction jump;

    InputAction dash;

    Animator animator;
    SpriteRenderer sprite;

    AudioSource jumpSFX;

    public int maxJumps = 2;

    public int maxDashes = 1;
    
    public float jumpPower = 250;

    public float dashDur = 1;

    public float dashPower = 5;

    public float exitMod = 1.3f;

    public float maxSpeed = 5;

    public float jumpDebounce = 0.3f;

    public float dashDebounce = 0.3f;

    public float accel = 100;
    
    public float decel = 200;

    float jumptime = 0;

    float dashTime = 0;

    bool dashPause = false;


    Rigidbody2D body;
    public bool isGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpSFX = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerActions = input.actions;
        move = playerActions.FindAction("Move");
        jump = playerActions.FindAction("Jump");
        dash = playerActions.FindAction("Dash");
    }

    void OnEnable()
    {
        
    }

    void doJump(int jumpCount) {
        print("jumping");
        jumpSFX.Play();
        animator.SetInteger("Jumps", jumpCount+1);
        animator.SetBool("Jumping", true);
        jumptime = Time.time;
        isGrounded = false;
        body.linearVelocityY = jumpPower;
    }

    void doDash(int dashCount, float moveDir) {
        print("Dashing");
        dashTime = Time.time;
        animator.SetInteger("Dashes", dashCount+1);
        animator.SetBool("Dashing", true);
        dashPause = true;
        float initSpeed = body.linearVelocityX;
        body.gravityScale = 0;
        body.linearVelocityY = 0;
        body.linearVelocityX = dashPower * moveDir;
        StartCoroutine(endDash(initSpeed));
    }

    IEnumerator endDash(float initSpeed) {
        yield return new WaitForSeconds(dashDur);
        dashPause = false;
        body.gravityScale = 1;
        animator.SetBool("Dashing", false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInfo = move.ReadValue<Vector2>();
        float xMove = moveInfo.x;
        float jumpInfo = jump.ReadValue<float>();
        float dashInfo = dash.ReadValue<float>();
        int jumpCount = animator.GetInteger("Jumps");
        int dashCount = animator.GetInteger("Dashes");
        bool tryJump = moveInfo.y > 0 || jumpInfo > 0;
        float timeSinceJump = Time.time - jumptime;
        float timeSinceDash = Time.time - dashTime;
        bool canJump = timeSinceJump >= jumpDebounce;
        bool canDash = timeSinceDash >= dashDebounce && dashCount < maxDashes;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        bool floorFound = false;
        foreach (Collider2D collision in collisions) {
            if (collision.gameObject.tag == "Floor") {
                floorFound = true;
                if(body.linearVelocityY <= 0 && canJump) {
                    if(isGrounded == false) {
                        floorFound = true;
                        body.linearVelocityY = 0;
                        isGrounded = true;
                        animator.SetInteger("Jumps", 0);
                        animator.SetInteger("Dashes", 0);
                        animator.SetBool("Falling", false);
                        animator.SetBool("Jumping", false);
                    }
                }
            }
        }
        isGrounded = floorFound;
        if (dashInfo > 0 && canDash) {
            doDash(dashCount, xMove);
        }
        if(!dashPause) {
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

            if (body.linearVelocityY < 0 && !isGrounded) {
                animator.SetBool("Falling", true);
            } else if (body.linearVelocityY >= 0 && !isGrounded) {
                animator.SetBool("Falling", false);
                animator.SetBool("Jumping", true);
            } 

            if (xMove > 0) {
                sprite.flipX = false;
            } else if (xMove < 0) {
                sprite.flipX = true;
            }
            print(goalSpeed);
            if (goalSpeed == 0 && Math.Abs(body.linearVelocityX) > 0) {
                print("move1");
                int sign = -1 * Math.Sign(body.linearVelocityX);
                body.linearVelocityX += Math.Min(decel, Math.Abs(body.linearVelocityX)) * sign;
            } else if (Math.Abs(goalSpeed) > Math.Abs(body.linearVelocityX) || Math.Sign(goalSpeed) != Math.Sign(body.linearVelocityX)) {
                print("Move2");
                body.linearVelocityX = Math.Min(accel, maxSpeed) * xMove;
            }
            if(Math.Abs(goalSpeed - body.linearVelocityX) < 0.1f) {
                print("Correct");
                body.linearVelocityX = goalSpeed;
            }
        }
    }
}
