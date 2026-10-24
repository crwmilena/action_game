using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float Acceleration = 20f;
    public CharacterController controller;
    public Transform firstCamera;
    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private float horizontal;
    private float vertical;
    private Vector3 currentVelocity;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (firstCamera != null)
        {
            float cameraYaw = firstCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cameraYaw, ref turnSmoothVelocity, turnSmoothTime, Mathf.Infinity, Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        Vector3 targetVelocity = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + firstCamera.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            targetVelocity = moveDir.normalized * Speed;
        }

        currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, Acceleration * Time.fixedDeltaTime);

        controller.Move(currentVelocity * Time.fixedDeltaTime);
    }
}