using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 100f;

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("Враг получил урон! Осталось HP: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Враг убит!");
        Destroy(gameObject);
    }
}