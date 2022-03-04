using UnityEngine;
public static class DefaultSettings
{ 
    public static int AnA = 0;
    //int TxQ = 1;
    public static bool VSync = false;
    public static int LevelSavedIndex = 1;
    public static Langs Language = Langs.EN;
    public static bool ShowFPS = false;
    public static bool Outline = false;

    public static bool ConvertIntToBoolean(int i)
    {
        if (i > 0)
            return true;
        else if (i <= 0)
            return false;

        return false;
    }

    public static int ConvertBooleanToInt(bool i)
    {
        if (i)
            return 1;
        else
            return 0;
    }
}

