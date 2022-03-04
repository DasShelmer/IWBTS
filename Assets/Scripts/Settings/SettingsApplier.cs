using UnityEngine;

public class SettingsApplier : MonoBehaviour {
    public bool showFPS;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(SettingsNames.AntiAliasing))
            PlayerPrefs.SetInt(SettingsNames.AntiAliasing, DefaultSettings.AnA);

        if (!PlayerPrefs.HasKey(SettingsNames.VSync))
            PlayerPrefs.SetInt(SettingsNames.VSync, DefaultSettings.ConvertBooleanToInt(DefaultSettings.VSync));

        if (!PlayerPrefs.HasKey(SettingsNames.ShowFPS))
            PlayerPrefs.SetInt(SettingsNames.ShowFPS, DefaultSettings.ConvertBooleanToInt(DefaultSettings.ShowFPS));

        if (!PlayerPrefs.HasKey(SettingsNames.Outline))
            PlayerPrefs.SetInt(SettingsNames.Outline, DefaultSettings.ConvertBooleanToInt(DefaultSettings.Outline));

        if (!PlayerPrefs.HasKey(SettingsNames.Language))
            PlayerPrefs.SetInt(SettingsNames.Language, (int)DefaultSettings.Language);

        try { Camera.main.gameObject.GetComponent<cakeslice.OutlineEffect>().enabled = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.Outline)); } catch (System.Exception ex) { Debug.Log(ex); }

        InvokeRepeating("CheckShowFPS", 1, 5);
    }
    public void Start ()
    {

        int AnA = PlayerPrefs.GetInt(SettingsNames.AntiAliasing);
        int VSync = PlayerPrefs.GetInt(SettingsNames.VSync);


        showFPS = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.ShowFPS));

        QualitySettings.antiAliasing = AnA;
        QualitySettings.vSyncCount = VSync;

    }

    void CheckShowFPS()
    {
        showFPS = DefaultSettings.ConvertIntToBoolean(PlayerPrefs.GetInt(SettingsNames.ShowFPS));
    }
}
