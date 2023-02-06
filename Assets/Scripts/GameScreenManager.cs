using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenManager : MonoBehaviour
{
    private void Start()
    {
        if (UnityEngine.XR.XRSettings.isDeviceActive)
        {
            Debug.Log("Oculus device Detected");
            SceneManager.LoadScene(3);
        }
        else
        {

            Debug.Log("Computer device Detected");
            SceneManager.LoadScene(0);
        }
    }
}
