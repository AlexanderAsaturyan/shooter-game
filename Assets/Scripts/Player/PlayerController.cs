using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;

    private float speed = 12f;
    private float gravity = -9.81f * 2f;
    private Vector3 velocity;

    private float groundRadius = 0.5f;
    private bool isGrounded;
    private float jumpHeight = 3f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}