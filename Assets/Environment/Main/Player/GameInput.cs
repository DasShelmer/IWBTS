using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
#pragma warning disable 0618
    public float updatePerSec = 60f;

    public bool canContol = true;

    Movement Movemt;
    Manager Manager;

    bool isWindows = false;

    bool isJumping = false;

    Vector2 halfScreen;

    void Start()
    {
        Manager = GameObject.Find("Manager").GetComponent<Manager>();

        string OS = SystemInfo.operatingSystem.Remove(SystemInfo.operatingSystem.IndexOf(' '));
        if (OS == "Windows")
            isWindows = true;
        Movemt = Manager.Player.GetComponent<Movement>();
        halfScreen = new Vector2(Screen.width, Screen.height) / 2;

        InvokeRepeating("SpecialUpdate", 0, 1 / updatePerSec);
    }
    

    void SpecialUpdate()
    {
        if (canContol == Manager.Canvas.GetComponent<InGameMenu>().MenuPause.active)
            canContol = !Manager.Canvas.GetComponent<InGameMenu>().MenuPause.active;

        if (isWindows)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Manager.Canvas.GetComponent<InGameMenu>().MenuPause.active)
                {
                    Manager.Canvas.GetComponent<InGameMenu>().Resume();
                }
                else
                {
                    Manager.Canvas.GetComponent<InGameMenu>().Pause();
                }
            }

            if (canContol)
            {
                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    Movemt.Motion(Direction.Left);
                }

                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    Movemt.Motion(Direction.Right);
                }

                if (Input.GetAxisRaw("Vertical") == 1 && !isJumping)
                {
                    isJumping = true;
                    Movemt.Motion(Direction.Up);
                }else if (Input.GetAxisRaw("Vertical") == 0)
                {
                    isJumping = false;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (Manager.Canvas.GetComponent<InGameMenu>().MenuPause.active)
                {
                    Manager.Canvas.GetComponent<InGameMenu>().Resume();
                }
                else
                {
                    Manager.Canvas.GetComponent<InGameMenu>().Pause();
                }
            }

            if (canContol)
            {
                Vector2[] touchs = new Vector2[3] { Vector2.zero, Vector2.zero, Vector2.zero};

                try { touchs[0] = Input.GetTouch(0).position; } catch { }
                try { touchs[1] = Input.GetTouch(1).position; } catch { }
                try { touchs[2] = Input.GetTouch(2).position; } catch { }

                Movemt.Motion(DirByTouch(touchs));
            }
        }
    }

    public Direction DirByTouch(Vector2[] touchs)
    {
        for (int i = 0; i < touchs.Length; i++)
        {
            if (touchs[i] != Vector2.zero)
            {
                if (touchs[i].y > halfScreen.y && !isJumping)
                {
                    isJumping = true;
                    return Direction.Up;
                }
                else
                {
                    isJumping = false;

                    if (touchs[i].x < halfScreen.x)
                    {
                        return Direction.Left;
                    }
                    else if (touchs[i].x > halfScreen.x)
                    {
                        return Direction.Right;
                    }
                }

                return Direction.Null;
            }
        }

        return Direction.Null; // Чтобы не было ошибки

        /*if (Input.GetTouch(1).position.y > halfScreen.y && !(Input.GetTouch(0).position.y > halfScreen.y))
        {
            MS.Motion(2);
        }
        else
        {
            if (Input.GetTouch(1).position.x < halfScreen.x && !(Input.GetTouch(0).position.x < halfScreen.x))
            {
                MS.Motion(0);
            }

            if (Input.GetTouch(1).position.x > halfScreen.x && !(Input.GetTouch(0).position.x > halfScreen.x))
            {
                MS.Motion(1);
            }
        }*/
    }
#pragma warning restore 0618
}
