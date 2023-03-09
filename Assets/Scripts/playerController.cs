using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mousSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] bool isQuest;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;

    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Transform childTransform = cameraHolder.transform.Find("HolderToDestroy");
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                Destroy(childObject);
            }
            // I want to destroy the child of rb in this line
            // Destroy(Cam.GetComponentInChildren<Camera>());
            Destroy(rb);
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
       if (!isQuest)
        {
            Look();
            Move();
            Jump();
        }
    }

    void Move()
    {
        Vector3 movDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, movDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    } 

    void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void Look ()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mousSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mousSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void SetGroundedState (bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        if (!isQuest)
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

}
