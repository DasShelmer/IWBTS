using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {
    
    public delegate void inQueue();
    public static inQueue inQueue1;
    public static inQueue inQueue2;
    public static inQueue inQueue3;
    public static inQueue inQueue4;
    public static inQueue inQueue5;

    void Start()
    {
        if (inQueue1 != null)
            inQueue1.DynamicInvoke();

        if (inQueue2 != null)
            inQueue2.DynamicInvoke();

        if (inQueue3 != null)
            inQueue3.DynamicInvoke();

        if (inQueue4 != null)
            inQueue4.DynamicInvoke();

        if (inQueue5 != null)
            inQueue5.DynamicInvoke();
    }
	
}
