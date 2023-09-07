using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;

    WaitForSeconds waitForFiveSeconds = new WaitForSeconds(5);

    private float speed = 10f;
    private float gravity = -9.81f * 2f;
    private Vector3 velocity;

    private float groundRadius = 0.5f;
    private bool isGrounded;

    private float score;
    public float Score => score;

    private float health = 100;
    public float Health => health;

    private void Start()
    {
        StartCoroutine(DecreaseHealth());
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void IncrementScore()
    {
        score++;
    }

    private IEnumerator DecreaseHealth()
    {
        while (true)
        {
            yield return waitForFiveSeconds;
            if(health > 0)
            {
                health -= 10;
            }
        }
    }
}