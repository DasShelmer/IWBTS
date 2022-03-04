using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicInvisActivator : MonoBehaviour {
    
    public bool ActiveOnEnterOrExit = true;

    public bool ExitVarIsA = true;
    public bool ExitVarIsB = true;
    public GameObject Exit;

    public string[] antiActivationNames = { "Bullet" };
    public string[] ActivationTags = { "Player_" };
    
    public void Do()
    {
        var lD = Exit.GetComponent<LogicDelegate>();
        lD.Do.DynamicInvoke(ExitVarIsA, ExitVarIsB);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ActiveOnEnterOrExit)
        {
            bool tagOk = false;

            foreach (var i in ActivationTags)
            {
                if (collision.gameObject.tag == i)
                {
                    tagOk = true;
                    break;
                }
            }

            foreach (var a in antiActivationNames)
            {
                if (collision.gameObject.name.Contains(a))
                {
                    tagOk = false;
                    break;
                }
            }

            if (tagOk)
            {
                Do();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!ActiveOnEnterOrExit)
        {
            bool tagOk = false;

            foreach (var i in ActivationTags)
            {
                if (collision.gameObject.tag == i)
                {
                    tagOk = true;
                    break;
                }
            }

            foreach (var a in antiActivationNames)
            {
                if (collision.gameObject.name.Contains(a))
                {
                    tagOk = false;
                    break;
                }
            }

            if (tagOk)
            {
                Do();
            }
        }
    }
}
