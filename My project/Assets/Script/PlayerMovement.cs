using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float startPlayerSpeed = 5f;
    public float jumpHeight = 2f;
    public static float playerSpeed;
    private Vector3 playerVelocity;

    //GroundCheck
    private bool groundedPlayer;
    private float groundCollisionFix = 0.4f;
    public float gravityValue = -9.81f;

    public static bool canMove;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        playerSpeed = startPlayerSpeed;
        canMove = true;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -groundCollisionFix;
        }
        if (canMove)
        {
            float vectorX = Input.GetAxis("Horizontal");
            float vectorZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * vectorX + transform.forward * vectorZ;

            controller.Move(move * playerSpeed * Time.deltaTime);

            if (controller.velocity.magnitude > 0)
                anim.SetBool("IsMoving", true);
            else
                anim.SetBool("IsMoving", false);
            

            if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue - groundCollisionFix);
            }
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

    }
}