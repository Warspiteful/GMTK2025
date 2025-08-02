using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField]    TextMeshProUGUI healthText;

    [SerializeField] private DrawManager drawManager;
    void Start()
    {
        expText.text = "EXP: " + drawManager.currentExp.ToString();
        healthText.text = "Health: " + drawManager.Health.ToString();

        drawManager.onExpChange += (int exp) => expText.text = "EXP: " + exp.ToString();
        drawManager.onHealthChange += (int health) => healthText.text = "Health: " + health.ToString();

    }
    public void Update()
    {

    }
}
