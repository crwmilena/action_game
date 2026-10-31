using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    private int kills = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddKill()
    {
        kills++;
        UpdateUI();
    }

    public int GetKills() //для панели смерти
    {
        return kills;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + kills;
    }
}