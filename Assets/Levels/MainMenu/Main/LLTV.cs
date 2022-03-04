using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LLTV : MonoBehaviour
{

    public GameObject ContentL, LevelLabelPrefab;
    int CountReseveIndices = 1;
    public string templateNames = " Level ";
    float sa = 1f, sb = 0.8f, sF = 0.2f;

    Text[] allTexts = new Text[0];

    void Start()
    {
        Localizer.updateLang += SetLanguage;

        if (!PlayerPrefs.HasKey("LevelSavedIndex"))
        {
            PlayerPrefs.SetInt("LevelSavedIndex", CountReseveIndices);
        }

        GameObject ThisLabel;
        for (int i = 0; i < PlayerPrefs.GetInt("LevelSavedIndex") - CountReseveIndices + 1; i++)
        {
            ThisLabel = (GameObject)Instantiate(LevelLabelPrefab, new Vector3(ContentL.transform.position.x, ContentL.transform.position.y, ContentL.transform.position.z), ContentL.transform.rotation);
            ThisLabel.transform.SetParent(ContentL.transform);
            Array.Resize<Text>(ref allTexts, allTexts.Length + 1);
            allTexts[allTexts.Length - 1] = ThisLabel.GetComponentInChildren<Text>();
            allTexts[allTexts.Length - 1].text = templateNames + (i + 1);

            ThisLabel.GetComponent<RectTransform>().anchorMin = new Vector2(0, sb);
            ThisLabel.GetComponent<RectTransform>().anchorMax = new Vector2(1, sa);

            sa -= sF;
            sb -= sF;

            ThisLabel.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            ThisLabel.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

            int localI = i + CountReseveIndices;
            ThisLabel.GetComponent<Button>().onClick.AddListener(delegate { CLloadLevel(localI); });
        }
        SetLanguage();
        Invoke("ulang", 1);
    }
    private void ulang()
    {
        Localizer.updateLang.DynamicInvoke();
    }

    public void SetLanguage()
    {
        Langs l = (Langs)PlayerPrefs.GetInt(SettingsNames.Language);
        for (int i = 0; i < allTexts.Length; i++)
        {
            string Number = "";

            string newLine = "";

            foreach (char ch in allTexts[i].text)
            {
                if (char.IsDigit(ch))
                    Number += ch.ToString();
                else
                    newLine += ch.ToString();
            }

            allTexts[i].text = Localization.GetLine(newLine, l) + Number;
        }

        //Debug.Log(templateNames);
    }


    public void CLloadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
