using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerControllerPhone : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movespeed;
   // private bool wave = false;

    // check if the animation is playing or not
    bool AnimatorIsPlaying()
    {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 );
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    private void FixedUpdate()
    {
        //Walk animation
    
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _movespeed, _rigidbody.velocity.y, _joystick.Vertical * _movespeed);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _animator.SetBool("isWalking", true); 
        }
        else { _animator.SetBool("isWalking", false); }


        //Wave animation 
        if (AnimatorIsPlaying("Waving")) { _animator.SetBool("isWaving", false); }

        // Jump animation 
        if (AnimatorIsPlaying("Jumping")) { _animator.SetBool("isJumping", false); }

    }
    public void Jump()
        {
            _animator.SetBool("isJumping", true);
        }
    public void Wave()
    {
        _animator.SetBool("isWaving", true);

    }

   

}
