using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logic
{
    public static bool GetValue(LogicOperator currentOperator, bool entA = false, bool entB = false, bool InvertExitValue = false)
    {
        if (currentOperator == LogicOperator.And && (entA && entB))
        {
            return !InvertExitValue;
        }

        if (currentOperator == LogicOperator.Or && (entA || entB))
        {
            return !InvertExitValue;
        }

        if (currentOperator == LogicOperator.Xor && (entA != entB))
        {
            return !InvertExitValue;
        }

        if (currentOperator == LogicOperator.Equivalency && (entA == entB))
        {
            return !InvertExitValue;
        }

        return !InvertExitValue;
    }
}
