using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerControllerPhone : MonoBehaviour
{
    [SerializeField] private GameObject rightHandIndex2;
    [SerializeField] private GrabSmartphone grabSmartphone;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movespeed;
    [SerializeField] private GameObject grabButton;
    [SerializeField] private GameObject throwButton;
    private bool wave = false;
    private Transform lastGrabbedParent;

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
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _movespeed, _rigidbody.velocity.y, _joystick.Vertical * _movespeed);
        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
           // _animator.Play("Walking");
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

    public void Grab()
    {
        grabSmartphone.lastSelectedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabSmartphone.lastSelectedObject.GetComponent<Rigidbody>().detectCollisions = false;
        lastGrabbedParent = grabSmartphone.lastSelectedObject.transform;
        grabSmartphone.lastSelectedObject.transform.parent = rightHandIndex2.transform;
        grabSmartphone.lastSelectedObject.transform.localPosition = new Vector3(0.0284f, -0.0064f, 0.0353f);
        grabSmartphone.lastSelectedObject.GetComponent<Renderer>().material.color = grabSmartphone.lastColor;
        throwButton.SetActive(true);
        grabButton.SetActive(false);

    }

    public void Throw()
    {
        grabSmartphone.lastSelectedObject.transform.parent = null;
        grabSmartphone.lastSelectedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabSmartphone.lastSelectedObject.GetComponent<Rigidbody>().detectCollisions = true;
        grabSmartphone.lastSelectedObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward);
        throwButton.SetActive(false);
    }

}
