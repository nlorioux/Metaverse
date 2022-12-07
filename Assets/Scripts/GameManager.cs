using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //V�rifier que le jouer est connect�
        //V�rifier que PlayerPrefab != null

        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 0.6f, -5), Quaternion.identity, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    public void OnPlayerEnterRoom(Player other)
    {
        print(other.NickName + "s'est connect� ! ");
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        print(other.NickName + "s'est d�connect� ! ");
    }

    public override void OnLeftRoom()
    {
        //SceneManager.LoadScene("LaucherScene");
    }
    
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
