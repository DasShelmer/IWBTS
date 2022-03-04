using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LogicOperator : int { And, Or, Xor, Equivalency}
public class LogicalOperators : MonoBehaviour
{
    public LogicOperator currentOperator = LogicOperator.Or;

    public bool InvertExitValues = false;
    public bool A = true;
    public bool B = false;
    public GameObject Exit;

    private void Start()
    {
        if (Exit)
        {
            if (Exit.GetComponent<LogicDelegate>())
            {
                transform.parent.gameObject.GetComponent<LogicDelegate>().Do += Begin;
            }
        }
    }

    public void Begin(bool entA = false, bool entB = false)
    {
        if(Logic.GetValue(currentOperator, entA, entB))
        {
            Do();
        }
        InvertExitValues = !InvertExitValues;
        Do();
        InvertExitValues = !InvertExitValues;
    }

    void Do()
    {
        if (Exit)
        {
            if (Exit.GetComponent<LogicDelegate>())
            {
                var lD = Exit.GetComponent<LogicDelegate>();

                if (InvertExitValues)
                    lD.Do.DynamicInvoke(!A, !B);
                else
                    lD.Do.DynamicInvoke(A, B);
            }
        }
    }
}
