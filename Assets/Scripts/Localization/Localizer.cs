using System;
using UnityEngine;
using UnityEngine.UI;

public class Localizer : MonoBehaviour
{
    Langs lang = DefaultSettings.Language;

    public delegate void UpdateLang();
    public static UpdateLang updateLang;

    Text[] allTxts = new Text[0];
    
    void Awake ()
    {
        GameObject[] all = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (var g in all)
        {
            if (g.GetComponent<Text>() && g.tag != "SpecialUI" && g.layer == LayerMask.NameToLayer("UI"))
            {
                Array.Resize<Text>(ref allTxts, allTxts.Length + 1);
                allTxts[allTxts.Length - 1] = g.GetComponent<Text>();
            }
        }

        updateLang = SetLanguage;

        QueueManager.inQueue1 = SetLanguage;
    }

    public void SetLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
            PlayerPrefs.SetInt("Language", (int)lang);

        lang = (Langs)PlayerPrefs.GetInt("Language");
        
        foreach(var i in allTxts)
        {
                i.text = Localization.GetLine(i.text, lang);
        }
    }

    public void CheckLanguage()
    {
        if ((Langs)PlayerPrefs.GetInt("Language") != lang)
        {
            lang = (Langs)PlayerPrefs.GetInt("Language");
            

            if (GameObject.Find("Manager").GetComponent<Manager>().Canvas.GetComponent<mainMenu>())
            {
                GameObject.Find("Manager").GetComponent<Manager>().Canvas.GetComponent<mainMenu>().showAll(true);
            }

            updateLang.Invoke();

            if (GameObject.Find("Manager").GetComponent<Manager>().Canvas.GetComponent<mainMenu>())
            {
                GameObject.Find("Manager").GetComponent<Manager>().Canvas.GetComponent<mainMenu>().showAll(false);
            }
        }

    }
}
