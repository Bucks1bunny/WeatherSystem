using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float sensitivityX;
    [SerializeField]
    private float sensitivityY;
    [SerializeField]
    private Transform player;
    private Camera cam;

    private float rotationX;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * 10 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * 10 * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -70f, 55f);

        transform.localEulerAngles = new Vector3(rotationX, 0f, 0f);

        player.Rotate(Vector3.up, mouseX);
    }
}