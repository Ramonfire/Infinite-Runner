using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float MinMoveSpeed = 5f;
    [SerializeField] float MaxMoveSpeed = 12f;
    Rigidbody rb;
    [SerializeField] float xClamp = 4f;
    [SerializeField] float ZClamp = 2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        DoMove();
    }

    private void DoMove()
    {
        Vector3 currentPosition = transform.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 position = currentPosition + moveDirection * (movementSpeed * Time.deltaTime);

        position.x = Mathf.Clamp(position.x, -xClamp, xClamp);
        position.z = Mathf.Clamp(position.z, -ZClamp, ZClamp);

        rb.MovePosition(position);

    }
    public void AdjustMoveSpeed(float inMoveSpeed) 
    {
        movementSpeed = Mathf.Clamp(movementSpeed + inMoveSpeed, MinMoveSpeed, MaxMoveSpeed);  
    }
    public void GetMovementInput(InputAction.CallbackContext callbackContext)
    {
        movement = callbackContext.ReadValue<Vector2>();
    }
}
