using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerControllerPhone : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movespeed;

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _movespeed, _rigidbody.velocity.y, _joystick.Vertical * _movespeed);
       
        Debug.Log("the velocity " + _rigidbody.velocity);
        Debug.Log(" the vertical " + _joystick.Vertical);
        Debug.Log(" the horizontal " + _joystick.Horizontal);


        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.Play("Walking");
        }
        if (_joystick.Vertical == 0 && _joystick.Horizontal == 0)
        {
            _animator.Play("Idle");
        }
    }

}
