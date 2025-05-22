using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _jumpHeight = 1000f;
    [SerializeField] private float _groundCheckRadius = 0.1f; 

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;

    private float _move = 0;
    private bool _facingRight = true;
    private bool _onGround = false;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("onGround", _onGround);
        _animator.SetFloat("vertSpeed", _rigidbody.linearVelocity.y);
        _animator.SetFloat("speed", Mathf.Abs(_move));
    }

    private void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(
            _groundChecker.position,
            _groundCheckRadius,
            _groundLayer);

        if (_onGround && Input.GetAxis("Jump") > 0)
        {
            if (_rigidbody.linearVelocity.y == 0)
                _rigidbody.AddForce(new Vector2(0, _jumpHeight));
        }

        _move = Input.GetAxis("Horizontal");
        _rigidbody.linearVelocity = new Vector2(
            x: _move * _maxSpeed,
            y: _rigidbody.linearVelocity.y);

        //Facing flip
        if ((_move > 0 && !_facingRight) || (_move < 0 && _facingRight))
        {
            _facingRight = !_facingRight;
            transform.localScale = new(
                transform.localScale.x * -1,
                transform.localScale.y);
        }
    }
}
