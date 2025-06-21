
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GravityController))]
public class MovementController : MonoBehaviour
{
    [Header("移动参数")]
    [Range(1, 15)] public float moveSpeed = 8f;
    [Range(0.1f, 1)] public float airControlFactor = 0.3f;
    [Range(5, 15)] public float jumpForce = 7f;
    
    private float horizontalInput;
    private Rigidbody2D _rb;
    private GravityController _gravityController;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityController = GetComponent<GravityController>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (_gravityController.IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        float controlFactor = _gravityController.IsGrounded ? 1f : airControlFactor;
        _rb.velocity = new Vector2(horizontalInput * moveSpeed * controlFactor, _rb.velocity.y);
    }
}