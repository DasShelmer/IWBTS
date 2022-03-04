using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicDelegate : MonoBehaviour
{
    public bool CanUsedPlayer = false;

    public bool End = false;
    public bool And = false, Or = true;

    public delegate void DoS(bool sA = false, bool sB = false);
    public DoS Do;

    public void Start()
    {
        Do += Begin;
    }
    public void Begin(bool sA = false, bool sB = false)
    {
        if (And)
        {
            if (sA && sB)
                End = true;
            else
                End = false;
        }
        if (Or)
        {
            if (sA || sB)
                End = true;
            else
                End = false;
        }
    }

    private void FixedUpdate()
    {
        if (!And && !Or)
            Or = true;
        else if (And && Or)
            Or = false;
    }
}
