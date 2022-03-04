using cakeslice;
using UnityEngine;

public class Manager : MonoBehaviour {
    [HideInInspector]
    public GameObject Player, WinObj, Canvas;
    
	void Start ()
    {
        Player = GameObject.Find("Player");
        WinObj = GameObject.Find("WinObj");
        Canvas = GameObject.Find("Canvas");

        GameObject[] all = UnityEngine.Object.FindObjectsOfType<GameObject>();
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].GetComponent<Outline>())
            {
                var ol = all[i].GetComponent<Outline>();
                ol.enabled = false;
                ol.enabled = true;
            }
        }
    }
	
}
