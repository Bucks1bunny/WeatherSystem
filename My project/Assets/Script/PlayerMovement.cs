using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField]
    public float StartSpeed
    {
        get;
        private set;
    }
    public float PlayerSpeed
    {
        get;
        set;
    }

    private Vector3 movementInput;
    private Rigidbody rb;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        PlayerSpeed = StartSpeed;
    }
    private void Update()
    {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

    }

    private void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(movementInput) * PlayerSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (rb.velocity.x > 0 || rb.velocity.z > 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}