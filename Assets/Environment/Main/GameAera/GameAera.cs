using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class GameAera : MonoBehaviour {

    public string deadReason = "Dead Zone";

    InGameMenu gM;
    GameObject Player;
	void Awake ()
    {
        var manager = GameObject.Find("Manager").GetComponent<Manager>();
        gM = manager.Canvas.GetComponent<InGameMenu>();
        Player = manager.Player;

        var col = GetComponent<CircleCollider2D>();
        if (!col.isTrigger)
            col.isTrigger = true;
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
            gM.Dead(Localization.GetLine(deadReason, (Langs)PlayerPrefs.GetInt(SettingsNames.Language)));
    }
}
