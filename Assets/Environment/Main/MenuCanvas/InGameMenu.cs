using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
#pragma warning disable 0618
    public GameObject
        MenuPause, MenuWin, MenuLose,
        DeadReasonText, MenuBackground;

    string deadReasonPattern = "You died by ";

    Rigidbody2D rbPlayer;
    Vector2 waitVel = Vector2.zero;

    void Awake()
    {
        rbPlayer = GameObject.Find("Manager").GetComponent<Manager>().Player.GetComponent<Rigidbody2D>();

        if (!MenuPause)
            MenuPause = GameObject.Find(gameObject.name + "/Pause");

        if (!MenuWin)
            MenuWin = GameObject.Find(gameObject.name + "/Win");

        if (!MenuLose)
            MenuLose = GameObject.Find(gameObject.name + "/Dead");

        if (!MenuBackground)
            MenuBackground = GameObject.Find(gameObject.name + "/MenuBackground");

        DeadReasonText = GameObject.Find(gameObject.name + "/" + MenuLose.name + "/DeadReasonText");
        
        QueueManager.inQueue3 = Resume;
    }

    public void Resume()
    {
        MenuBackground.active = false;
        MenuWin.active = false;
        MenuPause.active = false;
        MenuLose.active = false;
        PauseGame(1);
    }

    public void Dead(string reason)
    {
        MenuBackground.active = true;
        MenuWin.active = false;
        MenuPause.active = false;

        PauseGame(0);
        MenuLose.active = true;
        DeadReasonText.GetComponent<Text>().text = Localization.GetLine(deadReasonPattern, (Langs)PlayerPrefs.GetInt(SettingsNames.Language)) + reason;
    }

    public void Win()
    {
        MenuBackground.active = true;
        MenuPause.active = false;
        MenuLose.active = false;

        PauseGame(0);
        MenuWin.active = true;

        if (PlayerPrefs.HasKey("LevelSavedIndex") && PlayerPrefs.GetInt("LevelSavedIndex") < SceneManager.GetActiveScene().buildIndex + 1)
            PlayerPrefs.SetInt("LevelSavedIndex", SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Pause()
    {
        if (!(MenuWin.active || MenuLose.active))
        {
            PauseGame(0);

            MenuBackground.active = true;
            MenuWin.active = false;
            MenuLose.active = false;

            MenuPause.active = true;
        }
    }

    public void Quit()
    {
        PauseGame(1);
        Application.Quit();
    }

    public void ToMainMenu()
    {
        PauseGame(1);
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        PauseGame(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        PauseGame(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Устанавливет TimeScale на 0
    /// </summary>
    /// <param name="TimeScale">Установка TimeScale</param>
    public void PauseGame(float TimeScale = 0f, bool isPause = false)
    {
        if (isPause)
            Time.timeScale = TimeScale;
        else
        {
            if (TimeScale > 0)
            {
                rbPlayer.simulated = true;
                rbPlayer.velocity = waitVel;
            }
            else
            {
                waitVel = rbPlayer.velocity;
                rbPlayer.simulated = false;
            }
        }
    }
#pragma warning restore 0618
}
