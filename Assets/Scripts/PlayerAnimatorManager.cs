using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviour
{

    [SerializeField]
    private float directionDampTime = 0.25f;
    private Animator animator;
    private playerController playerMouvement;
    private float groundAltitude;


    // Start is called before the first frame update
    void Start()
    {
        playerMouvement = GameObject.FindGameObjectsWithTag("Controller")[0].GetComponent<playerController>();
        groundAltitude = 0;
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


            // deal with jump
            /*if (Input.GetKeyDown("space") && playerMouvement.grounded)
            {
                animator.SetBool("IsJumping", true);
            }
            else if (playerMouvement.grounded)
            {
                groundAltitude = transform.position.y;
                animator.SetFloat("Altitude", 0);
                animator.SetBool("IsJumping", false);
            }
            else if (!playerMouvement.grounded)
            {
                animator.SetFloat("Altitude", transform.position.y - groundAltitude);
                Debug.Log("IsJumping");
            }
            Debug.Log("Altitude : " + animator.GetFloat("Altitude"));
            */

            if (Input.GetKeyDown("m")){
                animator.SetBool("IsWaving", true);
            }
            else
            {
                animator.SetBool("IsWaving", false);
            }

            // set the Animator Parameters
                animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", v, directionDampTime, Time.deltaTime);
        }
    }

}
