using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField]    TextMeshProUGUI healthText;

    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] private DrawManager drawManager;

    [SerializeField] private FishManager fishManager;
    void Start()
    {
        expText.text = "EXP: " + drawManager.currentExp.ToString();
        healthText.text = "Health: " + drawManager.Health.ToString();
        drawManager.onExpChange += (int exp) => expText.text = "EXP: " + exp.ToString();
        drawManager.onHealthChange += (int health) => healthText.text = "Health: " + health.ToString();
        fishManager.onWaveChange += () =>
        {
            waveText.text = "Wave: " + fishManager.GetWaveNumber() + "/" + fishManager.GetWaveCount();
            waveText.gameObject.SetActive(true);
            StartCoroutine(Utils.timer(2, () => waveText.gameObject.SetActive(false)));
        };
    }

}
