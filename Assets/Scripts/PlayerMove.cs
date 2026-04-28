using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
{
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    Vector3 move = transform.right * x + transform.forward * z;
    controller.Move(move * speed * Time.deltaTime);

    // Mouse look
    float mouseX = Input.GetAxis("Mouse X") * 2f;
    float mouseY = Input.GetAxis("Mouse Y") * 2f;
    transform.Rotate(Vector3.up * mouseX);
    
    if (Camera.main != null)
        Camera.main.transform.Rotate(Vector3.left * mouseY);
    
    // Press Escape to unlock mouse
    if (Input.GetKeyDown(KeyCode.Escape))
        Cursor.lockState = CursorLockMode.None;
}
}