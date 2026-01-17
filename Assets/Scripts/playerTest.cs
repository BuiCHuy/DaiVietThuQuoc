using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class playerTest : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool lockDiagonalMovement = true;
    [SerializeField] private float inputDeadZone = 0.01f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Animator Params (optional)")]
    [SerializeField] private string dirXParam = "dirX";
    [SerializeField] private string dirYParam = "dirY";
    [SerializeField] private string speedParam = "";

    private Vector2 moveInput;
    private Vector2 facingDir = Vector2.down;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        Vector2 rawInput = ReadKeyboardInput();
        float x = rawInput.x;
        float y = rawInput.y;

        if (lockDiagonalMovement)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                y = 0f;
                x = x > 0f ? 1f : (x < 0f ? -1f : 0f);
            }
            else
            {
                x = 0f;
                y = y > 0f ? 1f : (y < 0f ? -1f : 0f);
            }
        }

        moveInput = new Vector2(x, y);

        if (moveInput.sqrMagnitude > inputDeadZone * inputDeadZone)
        {
            facingDir = moveInput;
        }

        UpdateVisuals();
    }

    private Vector2 ReadKeyboardInput()
    {
        float x = 0f;
        float y = 0f;

#if ENABLE_INPUT_SYSTEM
        Keyboard kb = Keyboard.current;
        if (kb != null)
        {
            bool left = kb.aKey.isPressed || kb.leftArrowKey.isPressed;
            bool right = kb.dKey.isPressed || kb.rightArrowKey.isPressed;
            bool up = kb.wKey.isPressed || kb.upArrowKey.isPressed;
            bool down = kb.sKey.isPressed || kb.downArrowKey.isPressed;

            if (left ^ right)
            {
                x = right ? 1f : -1f;
            }

            if (up ^ down)
            {
                y = up ? 1f : -1f;
            }

            return new Vector2(x, y);
        }
#endif

#if ENABLE_LEGACY_INPUT_MANAGER
        bool leftLegacy = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightLegacy = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool upLegacy = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool downLegacy = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (leftLegacy ^ rightLegacy)
        {
            x = rightLegacy ? 1f : -1f;
        }

        if (upLegacy ^ downLegacy)
        {
            y = upLegacy ? 1f : -1f;
        }

        return new Vector2(x, y);
#else
        return Vector2.zero;
#endif
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            transform.position += (Vector3)(moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnDisable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void UpdateVisuals()
    {
        // if (spriteRenderer != null && Mathf.Abs(facingDir.x) > 0.01f)
        // {
        //     spriteRenderer.flipX = facingDir.x < 0f;
        // }

        if (animator == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(dirXParam))
        {
            animator.SetFloat(dirXParam, facingDir.x);
        }

        if (!string.IsNullOrEmpty(dirYParam))
        {
            animator.SetFloat(dirYParam, facingDir.y);
        }

        if (!string.IsNullOrEmpty(speedParam))
        {
            animator.SetFloat(speedParam, moveInput.magnitude);
        }
    }
}
