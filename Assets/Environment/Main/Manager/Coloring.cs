using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Coloring : MonoBehaviour {

    public delegate void ColorUp();
    public static ColorUp colorUp;


    public int currentIndex = 0;

    public string[] hexBackground,
        hexGround, hexPhysic, hexLogic, hexEnemy, hexWinObj, hexPlayer,
        hexOutlineGroundPhysicLogic, hexOutlineEnemy, hexOutlineWinObjPlayer,
        hexMenuTitle, hexMenuButton, hexMenuButtonText, hexMenuBackground;
    Color[] cBackground;
    [HideInInspector]
    public Color[] cGround, cPhysic, cLogic, cEnemy, cWinObj, cPlayer;
    Color[] cOutlineGroundPhysicLogic, cOutlineEnemy, cOutlineWinObjPlayer;
    Color[] cMenuTitle, cMenuButton, cMenuButtonText, cMenuBackground;

    public Color oldCBackground,
        oldCGround, oldCPhysic, oldCLogic, oldCEnemy, oldCWinObj, oldCPlayer,
        oldCOutlineGroundPhysicLogic, oldCOutlineEnemy, oldCOutlineWinObjPlayer,
        oldCMenuTitle, oldCMenuButton, oldCMenuButtonText, oldCMenuBackground;

    int oldIndex;
    Manager Manager;
    

    void Awake()
    {
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        colorUp = letsColoring;

        oldIndex = currentIndex;

        
        if (!ReadColorsFromFile())
        {
            #region HexToColors
            cBackground = new Color[hexBackground.Length];
            for (int i = 0; i < cBackground.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexBackground[i], out cBackground[i]);
            }

            cGround = new Color[hexGround.Length];
            for (int i = 0; i < cGround.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexGround[i], out cGround[i]);
            }

            cPhysic = new Color[hexPhysic.Length];
            for (int i = 0; i < cPhysic.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexPhysic[i], out cPhysic[i]);
            }

            cLogic = new Color[hexLogic.Length];
            for (int i = 0; i < cLogic.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexLogic[i], out cLogic[i]);
            }

            cEnemy = new Color[hexEnemy.Length];
            for (int i = 0; i < cEnemy.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexEnemy[i], out cEnemy[i]);
            }

            cWinObj = new Color[hexWinObj.Length];
            for (int i = 0; i < cWinObj.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexWinObj[i], out cWinObj[i]);
            }

            cPlayer = new Color[hexPlayer.Length];
            for (int i = 0; i < cPlayer.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexPlayer[i], out cPlayer[i]);
            }

            cOutlineGroundPhysicLogic = new Color[hexOutlineGroundPhysicLogic.Length];
            for (int i = 0; i < cOutlineGroundPhysicLogic.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexOutlineGroundPhysicLogic[i], out cOutlineGroundPhysicLogic[i]);
            }

            cOutlineEnemy = new Color[hexOutlineEnemy.Length];
            for (int i = 0; i < cOutlineEnemy.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexOutlineEnemy[i], out cOutlineEnemy[i]);
            }

            cOutlineWinObjPlayer = new Color[hexOutlineWinObjPlayer.Length];
            for (int i = 0; i < cOutlineWinObjPlayer.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexOutlineWinObjPlayer[i], out cOutlineWinObjPlayer[i]);
            }

            cMenuTitle = new Color[hexMenuTitle.Length];
            for (int i = 0; i < cMenuTitle.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexMenuTitle[i], out cMenuTitle[i]);
            }

            cMenuButton = new Color[hexMenuButton.Length];
            for (int i = 0; i < cMenuButton.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexMenuButton[i], out cMenuButton[i]);
            }

            cMenuButtonText = new Color[hexMenuButtonText.Length];
            for (int i = 0; i < cMenuButtonText.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexMenuButtonText[i], out cMenuButtonText[i]);
            }

            cMenuBackground = new Color[hexMenuBackground.Length];
            for (int i = 0; i < cMenuBackground.Length; i++)
            {
                ColorUtility.TryParseHtmlString(hexMenuBackground[i], out cMenuBackground[i]);
            }
            #endregion}
        }

        QueueManager.inQueue2 = letsColoring;
    }

    bool ReadColorsFromFile()
    {
        try
        {
            TextAsset asset = (TextAsset)Resources.Load("palette");
            var s = new MemoryStream(asset.bytes);
            var r = new BinaryReader(s);

            if (s.Length < 2)
                return false;

            ushort countPaletties = r.ReadUInt16();
            if (s.Length < 4 * countPaletties + 2)
                return false;

            cBackground = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cBackground[i] = ReadColor(r);
            }

            cGround = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cGround[i] = ReadColor(r);
            }

            cPhysic = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cPhysic[i] = ReadColor(r);
            }

            cLogic = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cLogic[i] = ReadColor(r);
            }

            cEnemy = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cEnemy[i] = ReadColor(r);
            }

            cWinObj = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cWinObj[i] = ReadColor(r);
            }
            cPlayer = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cPlayer[i] = ReadColor(r);
            }
            cOutlineGroundPhysicLogic = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cOutlineGroundPhysicLogic[i] = ReadColor(r);
            }

            cOutlineEnemy = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cOutlineEnemy[i] = ReadColor(r);
            }


            cOutlineWinObjPlayer = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cOutlineWinObjPlayer[i] = ReadColor(r);
            }

            cMenuTitle = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cMenuTitle[i] = ReadColor(r);
            }

            cMenuButton = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cMenuButton[i] = ReadColor(r);
            }

            cMenuButtonText = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cMenuButtonText[i] = ReadColor(r);
            }

            cMenuBackground = new Color[countPaletties];
            for (ushort i = 0; i < countPaletties; i++)
            {
                cMenuBackground[i] = ReadColor(r);
            }

            r.Close();
            s.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    Color ReadColor(BinaryReader r)
    {
        byte A = r.ReadByte(), R = r.ReadByte(), G = r.ReadByte(), B = r.ReadByte();
        return new Color(R/255f, G/255f, B/255f, A/255f);
    }
    
    void FixedUpdate ()
    {
        if (oldIndex != currentIndex && currentIndex >= 0)
            colorUp.DynamicInvoke();
    }

    public void letsColoring()
    {
        oldIndex = currentIndex;

        GameObject[]
            oGround = GameObject.FindGameObjectsWithTag("Ground"),
            oPhysic = GameObject.FindGameObjectsWithTag("Physic"),
            oLogic = GameObject.FindGameObjectsWithTag("Logic"),
            oEnemy = GameObject.FindGameObjectsWithTag("Enemy"),

            oMenuTitle = GameObject.FindGameObjectsWithTag("MenuTitle"),
            oMenuButton = GameObject.FindGameObjectsWithTag("MenuButton"),
            oMenuButtonText = GameObject.FindGameObjectsWithTag("MenuButtonText"),
            oMenuBackground = GameObject.FindGameObjectsWithTag("MenuBackground")
            ;
        GameObject
            oBackground = GameObject.FindGameObjectWithTag("Background"),
            oWinObj = Manager.WinObj,
            oPlayer = Manager.Player;
        OutlineEffect OlEf = Camera.main.GetComponent<OutlineEffect>();
        
        #region LevelColoring
        if (oldCGround != cGround[currentIndex] && oGround.Length != 0)
        {
            for (int i = 0; i < oGround.Length; i++)
            {
                oGround[i].GetComponent<SpriteRenderer>().color = cGround[currentIndex];
            }
            oldCGround = cGround[currentIndex];
        }

        if (oldCPhysic != cPhysic[currentIndex] && oPhysic.Length != 0)
        {
            for (int i = 0; i < oPhysic.Length; i++)
            {
                oPhysic[i].GetComponent<SpriteRenderer>().color = cPhysic[currentIndex];
            }
            oldCPhysic = cPhysic[currentIndex];
        }

        if (oldCLogic != cLogic[currentIndex] && oLogic.Length != 0)
        {
            for (int i = 0; i < oLogic.Length; i++)
            {
                if (oLogic[i].GetComponent<SpriteRenderer>())
                    oLogic[i].GetComponent<SpriteRenderer>().color = cLogic[currentIndex];
            }
            oldCLogic = cLogic[currentIndex];
        }

        if (oldCEnemy != cEnemy[currentIndex] && oEnemy.Length != 0)
        {
            for (int i = 0; i < oEnemy.Length; i++)
            {
                oEnemy[i].GetComponent<SpriteRenderer>().color = cEnemy[currentIndex];
            }
            oldCEnemy = cEnemy[currentIndex];
        }

        if (oldCWinObj != cWinObj[currentIndex] && oWinObj)
        {
            //oWinObj = oWinObj.transform.parent.gameObject;// idk but w/o it not working

            oWinObj.GetComponent<WinObjAnim>().p1.GetComponent<SpriteRenderer>().color = cWinObj[currentIndex];
            oWinObj.GetComponent<WinObjAnim>().p2.GetComponent<SpriteRenderer>().color = cWinObj[currentIndex];
            oWinObj.GetComponent<WinObjAnim>().p3.GetComponent<SpriteRenderer>().color = cWinObj[currentIndex];

            oldCWinObj = cWinObj[currentIndex];
        }

        if (oldCPlayer != cPlayer[currentIndex] && oPlayer)
        {
            oPlayer.GetComponent<SpriteRenderer>().color = cPlayer[currentIndex];
            oldCPlayer = cPlayer[currentIndex];
        }

        if (oldCBackground != cBackground[currentIndex] && oBackground)
        {
            oBackground.GetComponent<Image>().color = cBackground[currentIndex];
            oldCBackground = cBackground[currentIndex];
        }
        #endregion
        #region OutlineColoring
        if (oldCOutlineGroundPhysicLogic != cOutlineGroundPhysicLogic[currentIndex] && OlEf)
        {
            OlEf.lineColor0 = cOutlineGroundPhysicLogic[currentIndex];
            oldCOutlineGroundPhysicLogic = cOutlineGroundPhysicLogic[currentIndex];
        }

        if (oldCOutlineEnemy != cOutlineEnemy[currentIndex] && OlEf)
        {
            OlEf.lineColor1 = cOutlineEnemy[currentIndex];
            oldCOutlineEnemy = cOutlineEnemy[currentIndex];
        }

        if (oldCOutlineWinObjPlayer != cOutlineWinObjPlayer[currentIndex] && OlEf)
        {
            OlEf.lineColor2 = cOutlineWinObjPlayer[currentIndex];
            oldCOutlineWinObjPlayer = cOutlineWinObjPlayer[currentIndex];
        }
        #endregion
        #region MenuColoring
        if (oldCMenuTitle != cMenuTitle[currentIndex] && oMenuTitle.Length != 0)
        {
            for (int i = 0; i < oMenuTitle.Length; i++)
            {
                oMenuTitle[i].GetComponent<Text>().color = cMenuTitle[currentIndex];
            }
            oldCMenuTitle = cMenuTitle[currentIndex];
        }

        if (oldCMenuButton != cMenuButton[currentIndex] && oMenuButton.Length != 0)
        {
            for (int i = 0; i < oMenuButton.Length; i++)
            {
                oMenuButton[i].GetComponent<Image>().color = cMenuButton[currentIndex];
            }
            oldCMenuButton = cMenuButton[currentIndex];
        }

        if (oldCMenuButtonText != cMenuButtonText[currentIndex] && oMenuButtonText.Length != 0)
        {
            for (int i = 0; i < oMenuButtonText.Length; i++)
            {
                oMenuButtonText[i].GetComponent<Text>().color = cMenuButtonText[currentIndex];
            }
            oldCMenuButtonText = cMenuButtonText[currentIndex];
        }

        if (oldCMenuBackground != cMenuBackground[currentIndex] && oMenuBackground.Length != 0)
        {
            for (int i = 0; i < oMenuBackground.Length; i++)
            {
                oMenuBackground[i].GetComponent<Image>().color = cMenuBackground[currentIndex];
            }
            oldCMenuBackground = cMenuBackground[currentIndex];
        }

        #endregion
    }
}
