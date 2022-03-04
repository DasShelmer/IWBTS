using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueCam : MonoBehaviour {

    public float zoomValue = 3000;

    private void Start()
    {
        InvokeRepeating("Check", 0, 0.5f);
    }

    private void Check()
    {
        if (Screen.width < Screen.height)
        { 
            Camera.main.orthographicSize = Screen.width / (2 * zoomValue / Screen.dpi);
        }
        else
        {
            Camera.main.orthographicSize = Screen.height / (2 * zoomValue / Screen.dpi);
        }
    }
}
