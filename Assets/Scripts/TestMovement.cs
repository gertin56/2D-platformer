using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _gravityModifier = 1f;

    private Rigidbody2D _rigidBody2D;
    private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private bool _grounded;
    private Vector2 _groundNormal;
    private float _minGroundNormalY = .65f;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    private void OnEnable()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _rigidBody2D.gravityScale = 0;
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && _grounded)
            _velocity.y = 11;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x * _speed;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rigidBody2D.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                ChangeVelocity(_hitBufferList[i].normal, yMovement);

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidBody2D.position = _rigidBody2D.position + move.normalized * distance;
    }

    private void ChangeVelocity(Vector2 normal, bool yMovement)
    {
        if (normal.y > _minGroundNormalY)
        {
            _grounded = true;
            if (yMovement)
            {
                _groundNormal = normal;
                normal.x = 0;
            }
        }

        float projection = Vector2.Dot(_velocity, normal);
        if (projection < 0)
        {
            _velocity = _velocity - projection * normal;
        }
    }
}
