using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private PlayerHealth playerHealth; //ссылка на скрипт здоровья

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateBar; //если меняется здоровье, обновляем бар
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(float currentHP, float maxHP)
    {
        bar.fillAmount = currentHP / maxHP;
    }
}