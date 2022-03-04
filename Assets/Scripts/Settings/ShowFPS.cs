using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour {

    public bool checkShowFPS = false;

    Text tx;
    float FPS;

    private void Awake()
    {
        if (checkShowFPS)
            InvokeRepeating("Start", 1, 1);
    }

    public void Start()
    {
        tx = GetComponent<Text>();
        if (Camera.main.GetComponent<SettingsApplier>().showFPS)
        {
            tx.enabled = true;
            InvokeRepeating("SetText", 0.5f, 0.5f);
        }
        else
        {
            tx.enabled = false;
        }
    }

    void SetText()
    {
        tx.text = Mathf.RoundToInt(FPS).ToString();
    }

    private void Update()
    {
        FPS = 1 / Time.deltaTime;
    }
}
