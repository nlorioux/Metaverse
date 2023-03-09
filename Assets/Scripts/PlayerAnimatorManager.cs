using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviour
{

    [SerializeField]
    public float jumpSpeed = 5;
    public float rotationSpeed;

    private float directionDampTime = 0.25f;
    private Animator animator;
    private playerController playerMovement;
    private float ySpeed;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectsWithTag("Controller")[0].GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // fail if missing Animator component on GameObject
        if (!animator && gameObject.transform.childCount > 2)
        {
            animator = gameObject.transform.GetChild(2).GetComponent<Animator>();
        }
        else if (animator && gameObject.transform.childCount > 2)
        {
            // deal with movement
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            ySpeed += Physics.gravity.y * Time.deltaTime;

            
            //check if we move with left or right arrow
            if (Input.GetKey("left")){
                animator.SetBool("SideMoveLeft", true);
            }
            else
            {
                animator.SetBool("SideMoveLeft", false);
            }

            if (Input.GetKey("right"))
            {
                animator.SetBool("SideMoveRight", true);
            }
            else
            {
                animator.SetBool("SideMoveRight", false);
            }


            // set the Animator Parameters
            float Speed = h * h + v * v;
            animator.SetFloat("Speed", Speed);
            animator.SetFloat("Direction", v, directionDampTime, Time.deltaTime);

            Vector3 velocity = (h * h + v * v) * new Vector3(h, ySpeed, v);

            // deal with jump

            if (Input.GetKeyDown("space") && playerMovement.grounded)
            {
                animator.SetBool("IsJumping", true);
            }
            else if (!playerMovement.grounded)
            {
                animator.SetBool("IsFalling", true);
                animator.SetBool("IsGrounded", false);
            }
            else if (playerMovement.grounded)
            {
                animator.SetBool("IsGrounded", true);
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
            }

            //playerMovement.transform.position.y = ySpeed * Time.deltaTime;
            

            if (Input.GetKeyDown("m")){
                animator.SetBool("IsWaving", true);
            }
            else
            {
                animator.SetBool("IsWaving", false);
            }
        }
    }

}
