using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    public GameObject firstView;
    public GameObject thirdView;
  public void First()
    {
        firstView.SetActive(true);
        thirdView.SetActive(false); 
    }

    public void Third()
    {
        thirdView.SetActive(true);
        firstView.SetActive(false);
    }
}
