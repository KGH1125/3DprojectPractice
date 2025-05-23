using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float JumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    private bool isJumpPressed = false;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        UpdateJump();
    }

    private void LateUpdate()
    {
        if (canLook) { CameraLook(); }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigid.velocity.y;

        rigid.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) { isJumpPressed = true; }
        else if (context.canceled) { isJumpPressed = false; }
    }

    void UpdateJump()
    {
        if (isJumpPressed && IsGround() && rigid.velocity.y == 0)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0.1f, rigid.velocity.z);
            rigid.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);

            CharacterManager.Instance.player.condition.AddStamina(-30f);
        }
    }

    private bool IsGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void AddMoveSpeed(float value)
    {
        moveSpeed += value;
    }

    public void AddJumpPower(float value)
    {
        JumpPower += value;
    }
    public IEnumerator AddMoveSpeedForDuration(float value, float duration)
    {
        moveSpeed += value;
        yield return new WaitForSeconds(duration);
        moveSpeed -= value;
    }
    public IEnumerator AddJumpPowerForDuration(float value, float duration)
    {
        JumpPower += value;
        yield return new WaitForSeconds(duration);
        JumpPower -= value;
    }

}
