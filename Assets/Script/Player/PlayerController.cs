using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    public Action inventory;
    private Rigidbody _rigidbody;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public bool isRun;//쉬프트를 누른 상태
    public bool isTired = false;//지쳤는가

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 5f;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        /*if (isRunning && IsRun())
        {
            Running();
        }*/
        //쉬프트를 누르면서 
        if(isRun && IsRun() && isTired == false)
        {
            moveSpeed = 8f;
            Running();
        }
        else
        {
            moveSpeed = 5f;
        }


        if (canLook)
            Look();
    }
    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }
    void Look()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
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
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isRun = true;
        }
        else// if(context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
        }
    }

    bool IsRun()
    {
        if (CharacterManager.Instance.Player.conditions.uiCondition.stamina.curValue > 0)
            return true;
        else
        {
            isTired = true;
            Invoke("TiredEnd", 5f);
            return false;
        }
    }

    void TiredEnd()
    {
        isTired = false;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.5f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.5f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            //Debug.DrawRay(rays[i].origin, rays[i].direction * 1.3f, Color.red);
            if (Physics.Raycast(rays[i], 1.3f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    void Running()
    {
        CharacterManager.Instance.Player.conditions.uiCondition.stamina.Subtract(CharacterManager.Instance.Player.conditions.uiCondition.stamina.passiveValue * 7 * Time.deltaTime);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            bool toggle = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
            canLook = !toggle;
        }
    }
}
