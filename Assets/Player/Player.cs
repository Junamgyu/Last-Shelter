using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float gravity = -9.81f;
    public float moveSpeed = 3f;
    public float jumpPower = 5f;
    public float rotationSpeed = 5f;
    
    [Header("References")]
    public Player_Cam orbitCamera;  //Player_Cam 참조

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (orbitCamera == null) Debug.Log("not found Connect Camera");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gravity();
        Jump();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; 
        controller.Move(move * moveSpeed * Time.deltaTime);

        Vector3 Camforward = orbitCamera.GetCameraForward();
        Vector3 CamRight = orbitCamera.GetCameraRight();

        moveDirection = (Camforward * z + CamRight * x).normalized;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void Gravity()
    {
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if(controller.isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpPower * -0.5f * gravity);
        }
    }
}
