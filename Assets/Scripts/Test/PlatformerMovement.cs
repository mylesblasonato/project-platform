using System;
using UnityEngine;
using UnityEngine.Events;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] string _horizontalAxis;
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] SoFloat _acceleration, _maxSpeed, _deceleration;

    Vector2 _direction;
    bool _facingRight = true;
    bool _isGrounded = false;

    private void Start()
    {
        EventManager.Instance.AddListener("OnGrounded", () => _isGrounded = true);
        EventManager.Instance.AddListener("OnJump", () => _isGrounded = false);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener("OnGrounded", () => _isGrounded = true);
        EventManager.Instance.RemoveListener("OnJump", () => _isGrounded = false);
    }

    void Update()
    {
        _direction = new Vector2(Input.GetAxis(_horizontalAxis), Input.GetAxis("Vertical"));
        _animator.SetBool("Grounded", _isGrounded);
    }

    void FixedUpdate()
    {    
        Move(_direction.x);
        ModifyPhysics();
    }

    void Move(float horizontal)
    {
        _rb.velocity +=  new Vector2(horizontal * _acceleration.Value, 0);
       
        if (horizontal > 0 && !_facingRight || horizontal < 0 && _facingRight)
            Flip();
        
        if (Mathf.Abs(_rb.velocity.x) > _maxSpeed.Value)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxSpeed.Value, _rb.velocity.y);

        if (_isGrounded)
            _animator.SetFloat("Move", Mathf.Abs(_direction.x));
    }

    void ModifyPhysics()
    {
        var changingDirections = (_direction.x > 0 && _rb.velocity.x < 0) || (_direction.x < 0 && _rb.velocity.x > 0);

        if (_rb.velocity.y == 0)
        {
            //LandAnimation         
            if (Mathf.Abs(_direction.x) < 0.4f || changingDirections)
                _rb.drag = _deceleration.Value;
            else
                _rb.drag = 0;
        }
        else
        {
            _rb.drag = _deceleration.Value * 0.5f;
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
    }

    #region HELPERS
    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(0, _groundCheckDistance));
    }
    #endregion
}
