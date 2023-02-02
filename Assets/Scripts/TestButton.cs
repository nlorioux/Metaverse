using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestButton : MonoBehaviour
{
    public TMP_Text textefield; 
    public void SetText()
    {
        string text = "hello world";
        textefield.text = text; 
    }
}
