using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject diedPanel;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.OnDied += HandleDeath;
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDied -= HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Игрок умер");
        if (diedPanel != null)
            diedPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}