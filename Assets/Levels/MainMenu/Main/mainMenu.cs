using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Runtime.Serialization;

public class mainMenu : MonoBehaviour
{
    public GameObject Main, ChooseLevel, Settings;

    private void Awake()
    {
        Coloring.colorUp += mBack;
    }
#pragma warning disable 0618
#pragma warning disable 1717
    public void mHTP()
    {
        Main.active = false;
        Settings.active = true;
    }

    public void mCL()
    {
        Main.active = false;
        ChooseLevel.active = true;
    }

    public void mBack()
    {
        try
        {
            Coloring.colorUp -= mBack;
        }
        catch { }
        ChooseLevel.active = false;
        Settings.active = false;
        Main.active = true;
    }

    public void mmQuit()
    {
        Application.Quit();
    }

    bool cL = false, set = false, main = false;
    public void showAll (bool show = false)
    {
        if (show)
        {
            cL = ChooseLevel.active;
            set = Settings.active;
            main = Main.active;

            ChooseLevel.active = true;
            Settings.active = true;
            Main.active = true;
        }
        else
        {
            ChooseLevel.active = cL;
            set = Settings.active = set;
            main = Main.active = main;
        }
    }
#pragma warning restore 0618
#pragma warning restore 1717
}
