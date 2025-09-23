using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Controls controls;
    Rigidbody2D rb2D;
    [SerializeField] float speed = 5.0f;

    [Header("Jump")]
    [SerializeField] float jumpPower = 2.0f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance = 1.0f;
    private bool IsJump;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();

        controls.Player.Jump.performed += HandleJump;
        controls.Player.Jump.canceled += HandleJumpCancled;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= HandleJump;
        controls.Player.Jump.canceled -= HandleJumpCancled;
        controls.Player.Disable();
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float dir = controls.Player.Move.ReadValue<float>();
        rb2D.linearVelocity = new Vector2(dir * 5, rb2D.linearVelocityY);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGround() && !IsJump)
        {
            rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        }
    }

    private bool IsGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
    }

    private void HandleJumpCancled(InputAction.CallbackContext context)
    {
        IsJump = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * groundCheckDistance));
    }
}
