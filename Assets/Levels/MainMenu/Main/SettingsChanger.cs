using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsChanger : MonoBehaviour {

    public GameObject VSync, ShowFPS, OutlineToggle, Language, AA;
    SettingsApplier sa;

    private void Awake()
    {
        sa = gameObject.GetComponent<SettingsApplier>();

        if (!PlayerPrefs.HasKey(SettingsNames.ShowFPS))
            PlayerPrefs.SetInt(SettingsNames.ShowFPS, DefaultSettings.ConvertBooleanToInt(DefaultSettings.ShowFPS));
    }

    void Start()
    {

        VSync.GetComponent<Toggle>().isOn = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.VSync));
        ShowFPS.GetComponent<Toggle>().isOn = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.ShowFPS));
        OutlineToggle.GetComponent<Toggle>().isOn = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.Outline));
        Language.GetComponent<Dropdown>().value = PlayerPrefs.GetInt(SettingsNames.Language);


        Invoke("SetAA", 0.5f);
    }

    void SetAA()
    {
        int trueVal = 0, value = PlayerPrefs.GetInt(SettingsNames.AntiAliasing);

        if (value == 0)
            trueVal = 0;
        else if (value == 2)
            trueVal = 1;
        else if (value == 4)
            trueVal = 2;
        else if (value == 8)
            trueVal = 3;

        AA.GetComponent<Dropdown>().value = trueVal;
    }

    public void ApplyVSync()
    {
        bool value = VSync.GetComponent<Toggle>().isOn;
        int trueVal;

        if ((bool)value)
            trueVal = 1;
        else
            trueVal = 0;

        PlayerPrefs.SetInt(SettingsNames.VSync, trueVal);

        sa.Start();
    }

    public void ApplyShowFPS()
    {
        bool value = ShowFPS.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt(SettingsNames.ShowFPS, DefaultSettings.ConvertBooleanToInt(value));
        sa.Start();
    }

    public void ApplyOutline()
    {
        bool value = OutlineToggle.GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt(SettingsNames.Outline, DefaultSettings.ConvertBooleanToInt(value));
        sa.Start();
    }

    public void ApplyLanguage()
    {
        Debug.Log("ApplyLanguage()");
        int value = Language.GetComponent<Dropdown>().value;

        PlayerPrefs.SetInt(SettingsNames.Language, (int)value);

        Localizer.updateLang.Invoke();
    }

    public void ApplyAA()
    {
        int value = AA.GetComponent<Dropdown>().value;
        int trueVal = 0;

        if ((int)value == 0)
            trueVal = 0;
        else if ((int)value == 1)
            trueVal = 2;
        else if ((int)value == 2)
            trueVal = 4;
        else if ((int)value == 3)
            trueVal = 8;

        PlayerPrefs.SetInt(SettingsNames.AntiAliasing, trueVal);
        sa.Start();
    }
}
