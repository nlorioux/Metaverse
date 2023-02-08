using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject canvasComputer;
    [SerializeField] GameObject canvasOculus;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.XR.XRSettings.isDeviceActive)
        {
            canvasOculus.SetActive(true);
            canvasComputer.SetActive(false);

        } else if (UnityEditor.EditorApplication.isRemoteConnected)
            {
            Debug.Log("Hellooooooooo");
            } 
       
        else
        {
            canvasComputer.SetActive(true);
            canvasOculus.SetActive(false);
        }
    }

    // Update is called once per frame
 
}
