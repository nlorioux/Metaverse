using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateController()
    {
        
        if (UnityEngine.XR.XRSettings.isDeviceActive)
        {
            Debug.Log("Oculus Quest 2 is connected");
            Debug.Log("Instanciate Player Controller");
            //Instantiate our player controoller
            PhotonNetwork.Instantiate(Path.Combine("OculusPrefabs", "PlayerController(Android)"), new Vector3(0,0,2), Quaternion.identity);
        }
        else
        {
            Debug.Log("Running on Computer");
            Debug.Log("Instanciate Player Controller");
            //Instantiate our player controoller
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        }
       
       
    }

}
