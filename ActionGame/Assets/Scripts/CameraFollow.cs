using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //игрок
    public float distance = 5f;
    public float height = 2f;
    public float mouseSensitivity = 3f;

    private float yaw = 0f;
    private float pitch = 20f;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //блок мышки
        Cursor.visible = false;
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    void LateUpdate()
    {
        if (target == null || Time.timeScale == 0f) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -30f, 70f); //ограничение угла наклона камеры

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance) + (Vector3.up * height); //позиция камеры

        if (shakeDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude; //сфера
            desiredPosition += shakeOffset;
            shakeDuration -= Time.deltaTime;
        }

        transform.position = desiredPosition;
        transform.rotation = rotation;
    }
}