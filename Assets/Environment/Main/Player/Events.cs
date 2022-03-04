using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour {

    Manager Manager;
    
    string[] namesOfExceptionsObjects = { "lBooster", "Button", "lSecDoubleJump" };
    private void Start()
    {
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindEvent(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindEvent(collision);
    }

    public void FindEvent (Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Manager.Canvas.GetComponent<InGameMenu>().Dead(Localization.GetLine(collision.gameObject.GetComponent<Enemy>().enemyName, (Langs)PlayerPrefs.GetInt("Language")));

        if (collision.gameObject == Manager.WinObj)
            Manager.Canvas.GetComponent<InGameMenu>().Win();

        if (collision.gameObject.tag == "Logic")
        {
            bool Ok = true;

            foreach(string s in namesOfExceptionsObjects)
            {
                if (collision.gameObject.name.Contains(s))
                {
                    Ok = false;
                }
            }

            try
            {
                if (Ok)
                    if (collision.gameObject.GetComponent<LogicDelegate>().CanUsedPlayer)
                        collision.gameObject.GetComponent<LogicDelegate>().Do.DynamicInvoke(true, true);
            }
            catch
            {
                Debug.LogError("Error on DynamicInvoke() delegate in logic object: " + collision.gameObject.name);
            }
        }
    }
}
