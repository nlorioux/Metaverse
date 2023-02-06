using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public override void OnEnable()
    {

        Debug.Log("INSTANTIATED");
        base.OnEnable();
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }

    void OnSceneLoaded (Scene current, Scene next)
    {
        if (current.buildIndex ==1 || next.buildIndex==1) // we're in the game scene
        {
            Debug.Log("INSTANTIATED");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
}
