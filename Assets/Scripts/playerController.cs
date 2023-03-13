using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using ReadyPlayerMe;

public class playerController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mousSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] bool isQuest;
    string avatarLink = "https://models.readyplayer.me/640f12e15ff9a2cd66c48c70.glb";
    public Material mat;
    private GameObject avatar;

    float verticalLookRotation;
    public bool grounded;
    public float lastGroundedTime;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;

    PhotonView PV;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    void LoadAvatar1(string link)
    {
        mat.color = Color.red;
    }
    
    void sendAvatarLink()
    {
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("avatarLink", avatarLink);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
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
           
        } else
        {
            sendAvatarLink();
            Debug.Log("Fucking link is sent");
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

    private void Jump ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
            //playerMovement.transform.position.y = ySpeed * Time.deltaTime;
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
        lastGroundedTime = Time.time;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if ( targetPlayer == PV.Owner)
        {
            LoadAvatar((string)changedProps["avatarLink"]);
            
        }
    }
    private void LoadAvatar(string link)
    {
        //LoadAvatar();

        //ApplicationData.Log();
        //avatarPUNPrefab = GameObject.FindGameObjectWithTag("Controller");
        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (_, args) =>
        {
            avatar = args.Avatar;

            avatar.transform.parent = gameObject.transform;
            avatar.transform.position = avatar.transform.parent.position - new Vector3(0, 1f, 0);
            avatar.transform.rotation = Quaternion.LookRotation(gameObject.transform.forward);
            avatar.GetComponent<Animator>().applyRootMotion = false;
            //gameObject.GetComponent<Rigidbody>().mass = 1.5f;
            AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);
        };
        string avatarURL = "https://models.readyplayer.me/640f12e15ff9a2cd66c48c70.glb";
        avatarLoader.LoadAvatar(link);
    }


    private void OnDestroy()
    {
        if (avatar != null) Destroy(avatar);
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        if (!isQuest)
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }



}
