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
    
    public float jumpPower;

    Rigidbody2D body;
    public bool isGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        playerActions = input.actions;
        move = playerActions.FindAction("Move");
        jump = playerActions.FindAction("Jump");
    }

    void OnEnable()
    {
        
    }

    void doJump(int jumpCount) {
        print("Jumping");
        isGrounded = false;
        body.AddForceY(jumpPower);
        animator.SetInteger("Jumps", jumpCount+1);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInfo = move.ReadValue<Vector2>();
        float xMove = moveInfo.x;
        float jumpInfo = jump.ReadValue<float>();
        int jumpCount = animator.GetInteger("Jumps");
        bool tryJump = moveInfo.y > 0 || jumpInfo > 0;
        print(transform.position);
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        foreach (Collider2D collision in collisions) {
            if (collision.gameObject.tag == "Floor" && body.linearVelocityY <= 0) {
                if(isGrounded == false) {
                    body.linearVelocityY = 0;
                    isGrounded = true;
                    animator.SetInteger("Jumps", 0);
                    animator.SetBool("Jumping", false);
                }
            }
        }
        if (tryJump) {
            if (jumpCount < maxJumps || isGrounded) {
                doJump(jumpCount);
            }
        }
        
    }
}
