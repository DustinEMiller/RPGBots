using System;
using UnityEngine;
public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float _turnSpeed = 1000f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Animator _animator;
    private float _mouseMovement;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    } 

    void Update()
    {
        _mouseMovement += Input.GetAxis("Mouse X");
    }

    private void FixedUpdate()
    {
        if (ToggleablePanel.IsVisible == false)
        {
            transform.Rotate(0, _mouseMovement * Time.deltaTime * _turnSpeed, 0);
        }
        
        _mouseMovement = 0;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2f;
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;
        _rigidBody.MovePosition(transform.position + offset);
        
        _animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
        _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
    }
}
