using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{
    public float gravity = -9.81f;
    public float moveSpeed = 3f;
    public float jumpPower = 5f;
    public float rotationSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
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
