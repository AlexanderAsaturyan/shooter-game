using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GunController gunController;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI bulletCount;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image gameOverPopup;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        healthText.text = $"Health: {playerController.Health}";
        bulletCount.text = $"Bullets: {gunController.BulletCount}";
        scoreText.text = $"Score: {playerController.Score}";

        if(playerController.Health == 0)
        {
            gameOverPopup.gameObject.SetActive(true);
        }
    }
}
