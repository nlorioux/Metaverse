using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviour
{

    [SerializeField]
    private float directionDampTime = 0.25f;
    private Animator animator;

    // Start is called before the first frame update
    /*void Start()
    {
        animator = gameObject.transform.GetChild(2).GetComponent<Animator>();
        Debug.Log("Child name : " + gameObject.transform.GetChild(2).name);
    }*/

    // Update is called once per frame
    void Update()
    {
        // failSafe is missing Animator component on GameObject
        if (!animator && gameObject.transform.childCount > 0)
        {
            animator = gameObject.transform.GetChild(2).GetComponent<Animator>();
        }

        if (animator != null)
        {
            // deal with movement
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // prevent negative Speed.
            if (v < 0)
            {
                v = 0;
            }

            // set the Animator Parameters
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }
    }
}
