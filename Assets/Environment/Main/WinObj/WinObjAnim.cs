using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjAnim : MonoBehaviour {

    public GameObject p1, p2, p3;
    public float speedP1, speedP2, speedP3;
    
    private void FixedUpdate()
    {
        if (p1)
            p1.transform.Rotate(0, 0, speedP1);

        if (p2)
            p2.transform.Rotate(0, 0, speedP2);

        if (p3)
            p3.transform.Rotate(0, 0, speedP3);
    }
}
