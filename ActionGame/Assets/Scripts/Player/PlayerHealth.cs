using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDied;

    private bool isDead = false;

    [Header("Настройки урона")]
    [SerializeField] private float invulnerabilityTime = 1f;
    private float lastDamageTime = -999f;

    private void Start()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            TryTakeDamage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            TryTakeDamage();
        }
    }

    private void TryTakeDamage()
    {
        if (Time.time >= lastDamageTime + invulnerabilityTime)
        {
            TakeDamage(20f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        lastDamageTime = Time.time;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        OnDied?.Invoke();
    }
}