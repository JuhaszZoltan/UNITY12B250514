using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _jumpHeight = 230f;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    
    private float _groundCheckRadius = 0.1f; 

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _facingRight = true;
    private bool _onGround = false;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_onGround && Input.GetAxis("Jump") > 0)
        {
            _onGround = false;
            _animator.SetBool("onGround", false);
            _rigidbody.AddForce(new Vector2(0, _jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        _onGround = Physics2D.OverlapCircle(
            _groundChecker.position,
            _groundCheckRadius,
            _groundLayer);

        _animator.SetBool("onGround", _onGround);
        _animator.SetFloat("vertSpeed", _rigidbody.linearVelocity.y);


        float move = Input.GetAxis("Horizontal");

        _animator.SetFloat("speed", Mathf.Abs(move));

        _rigidbody.linearVelocity = new Vector2(
            x: move * _maxSpeed,
            y: _rigidbody.linearVelocity.y);

        if (move > 0 && !_facingRight) Flip();
        else if (move < 0 && _facingRight) Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }
}
