using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    public float damage = 100f;
    public float lifeTime = 3f;

    public GameObject hitEffectPrefab;

    private Vector3 moveDirection;
    private float spawnTime;

    public void SetDirection(Vector3 dir) //из скрипта оружия
    {
        moveDirection = dir;
        transform.forward = dir;
    }

    void Start()
    {
        spawnTime = Time.time; //чтобы пуля удалилась
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (Time.time - spawnTime < 0.1f) return;
        if (other.CompareTag("Player")) return; //игнор игрока

        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }

        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f); //удаляем через 2 сек
        }
        Destroy(gameObject);
    }
}