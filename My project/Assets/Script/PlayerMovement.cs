using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movementInput;
    private Rigidbody rb;
    public float startPlayerSpeed = 5f;
    public static float playerSpeed;


    public static bool canMove;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerSpeed = startPlayerSpeed;
        canMove = true;
    }
    private void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 move = transform.TransformDirection(movementInput) * playerSpeed;

            rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

            if (rb.velocity.magnitude > 0)
                anim.SetBool("IsMoving", true);
            else
                anim.SetBool("IsMoving", false);
            
        }

    }
}