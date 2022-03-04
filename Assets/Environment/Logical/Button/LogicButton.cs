using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LogicButton : MonoBehaviour {

    public bool ExitVarIsA = true;
    public bool ExitVarIsB = true;
    public GameObject Exit;

    public void Do()
    {
        var lD = Exit.GetComponent<LogicDelegate>();
        lD.Do.DynamicInvoke(ExitVarIsA, ExitVarIsB);
    }
}
