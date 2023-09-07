using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private float health = 100;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
            playerController.IncrementScore();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}