using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float Acceleration = 20f;
    public CharacterController controller;
    public Transform firstCamera;
    public float turnSmoothTime = 0.1f;
    public float gravity = -25f;

    private float turnSmoothVelocity;
    private float horizontal;
    private float vertical;
    private float verticalVelocity;
    private Vector3 currentVelocity;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.fixedDeltaTime; //ускорение

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (firstCamera != null)
        {
            float cameraYaw = firstCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cameraYaw, ref turnSmoothVelocity, turnSmoothTime, Mathf.Infinity, Time.fixedDeltaTime); //угол, на который игрок должен повернуться в этом кадре
            transform.rotation = Quaternion.Euler(0f, angle, 0f); //применяем поворот к игроку
        }

        Vector3 targetVelocity = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + firstCamera.eulerAngles.y; //угол движения с учетом камеры
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; //превращает угол в вектор направления
            targetVelocity = moveDir.normalized * Speed;
        }

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, Acceleration * Time.fixedDeltaTime); //для ускорения

        Vector3 finalMove = currentVelocity;
        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.fixedDeltaTime);
    }
}