using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rb.simulated = false;

        if (_animator != null)
        {
            _animator.enabled = false;
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        if (!_rb.simulated)
        {
            _rb.simulated = true;
        }

        if (_animator != null && !_animator.enabled)
        {
            _animator.enabled = true;
        }

        if (WasFlyPressed())
        {
            _rb.linearVelocity = Vector2.up * _velocity;
        }
    }

    private bool WasFlyPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(0, 0, _rb.linearVelocity.y * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        GameManager.Instance.GameOver();
        _rb.linearVelocity = Vector2.zero;
        _rb.simulated = false;

        if (_animator != null)
        {
            _animator.enabled = false;
        }
    }
}
