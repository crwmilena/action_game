using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Camera mainCamera; //для прицеливания
    public float range = 100f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f) return; //для паузы

        if (Input.GetMouseButtonDown(0))
        {
            if (bulletPrefab != null && mainCamera != null)
            {
                Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); //луч из центра экрана
                Vector3 targetPoint = ray.GetPoint(range);

                if (Physics.Raycast(ray, out RaycastHit hit, range))
                {
                    if (!hit.collider.CompareTag("Player")) //игнорируем игрока
                    {
                        targetPoint = hit.point;
                    }
                }

                Vector3 direction = (targetPoint - firePoint.position).normalized;

                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direction)); //создание пули
                Camera.main.GetComponent<CameraFollow>().ShakeCamera(0.1f, 0.05f); //шейк

                if (newBullet.TryGetComponent(out Bullet bulletScript))
                {
                    bulletScript.SetDirection(direction); //передаем направление пуле
                }

                if (audioSource != null)
                {
                    audioSource.pitch = Random.Range(0.9f, 1.1f);
                    audioSource.Play();
                }
                FindObjectOfType<BackgroundMusic>().DuckVolume(); //приглушение
            }
        }
    }
}
