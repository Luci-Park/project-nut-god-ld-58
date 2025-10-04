using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash / Roll")]
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.6f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 0.5f;

    private Player player;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastNonZeroDir = Vector2.right;

    private bool isDashing = false;
    private bool canDash = true;

    Color[] c = { Color.red, Color.green, Color.blue, Color.white };
    public SpriteRenderer testSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (moveInput.sqrMagnitude > 0.0001f)
            lastNonZeroDir = moveInput.normalized;

        Vector2 dir = isDashing
            ? rb.linearVelocity            // if you use linearVelocity, keep that
            : moveInput;

        SetColorByDirection(dir);
    }

    private void FixedUpdate()
    {
        if (isDashing) return;
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (!context.performed || !canDash) return;

        Vector2 dir = moveInput.sqrMagnitude > 0.0001f ? moveInput.normalized : lastNonZeroDir;
        StartCoroutine(DashRoutine(dir));
    }

    private IEnumerator DashRoutine(Vector2 dir)
    {
        canDash = false;
        isDashing = true;

        // Make dash crisp (prevent damping)
        float originalDrag = rb.linearDamping;
        rb.linearDamping = 0f;

        // Optional: clear momentum, then set instant dash velocity
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = dir * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.linearDamping = originalDrag;
        rb.linearVelocity = Vector2.zero; // uncomment if you want a hard stop after dash

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void SetColorByDirection(Vector2 dir)
    {
        if (!testSprite) return;
        dir.Normalize();

        // 4-way bucket (cardinals). Swap colors however you like.
        // Using c[0]=Down(red), c[1]=Up(green), c[2]=Left(blue), c[3]=Right(white)
        Color chosen;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            chosen = dir.x >= 0f ? c[3] : c[2];  // right vs left
        else
            chosen = dir.y >= 0f ? c[1] : c[0];  // up vs down

        testSprite.color = chosen;
    }
}
