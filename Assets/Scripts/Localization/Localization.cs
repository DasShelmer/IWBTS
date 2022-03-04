using System.Linq;
public enum Langs : int { EN, RU}
public static class Localization
{
    public static string[] EN =
    {
    #region InGameMenu
        "Pause",
        "Resume",
        "Restart",
        "To main menu",
        "Quit",
        "Next level",
        "You died by ",
        "You won!",
    #endregion
    #region MainMenu
        "I Wanna Be The Square",
        "Choose Level",
        "Settings",
        "Back",
        "Level ",
        "- Enable VSync",
        "- Enable FPS showing",
        "- Enable Outline",
        "EN",
        "RU",
        "Antialiasing Off",
    #endregion
    #region AA
        "Antialiasing",
        "Antialiasing x2",
        "Antialiasing x4",
        "Antialiasing x8",
    #endregion
    #region Enemies
        "Unknown",
        "Spike",
        "Turret",
        "Rocket Turret",
        "Dead Zone"
        #endregion
    };

    public static string[] RU =
    {
    #region InGameMenu
        "Пауза",
        "Продолжить",
        "Заново",
        "В гл.меню",
        "Выйти",
        "Следующий уровень",
        "Вы умерли от ",
        "Вы победитель!",
    #endregion
    #region MainMenu
        "I Wanna Be The Square",
        "Выбрать уровень",
        "Настройки",
        "Назад",
        "Уровень №",
        "- Включить верт.синхронизацию",
        "- Включить отображение FPS",
        "- Включить обводку",
        "EN",
        "RU",
        "Antialiasing Выкл",
        #endregion
    #region AA
        "Antialiasing",
        "Antialiasing x2",
        "Antialiasing x4",
        "Antialiasing x8",
        #endregion
    #region Enemies
        "неизвестности",
        "шипа",
        "турели",
        "ракетной турели",
        "запретной зоны"
    #endregion
    };

    public static string GetLine (string line, Langs lang)
    {
        Langs lg;

        string Number = "";
        if (line.Any(char.IsDigit))
        {
            string newLine = "";

            foreach (char ch in line)
            {
                if (char.IsDigit(ch))
                    Number += ch.ToString();
                else
                    newLine += ch.ToString();
            }
            line = newLine;

        }else if (line.All(char.IsDigit))
        {
            return line;
        }

        if (lang == Langs.EN)
            lg = Langs.RU;
        else
            lg = Langs.EN;

        int Index = GetIndex(line, lg);

        if (Index < 0)
            Index = GetIndex(line, lang);
        

        string[] Language = new string[0];

        if (lang == Langs.EN)
            Language = EN;
        else
            Language = RU;

        if (Index == -1)
        {
            return "ErrStrg:" + line;
        }

        if (Number != "")
            return Language[Index] + Number;

        return Language[Index];
    }

    static int GetIndex(string line, Langs lang)
    {
        string[] Language = new string[0];

        if (lang == Langs.EN)
            Language = EN;
        else
            Language = RU;

        for (int i = 0; i < Language.Length; i++)
        {
            if (Language[i] == line)
                return i;/*
            else if (Language[i].StartsWith(line))
            {
                Debug.Log(line);
                return i + SecondErr;
            }*/
        }

        return -1;
    }
}
