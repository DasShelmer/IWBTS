using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAnim : MonoBehaviour {

    public GameObject newO;

    [HideInInspector]
    public Vector3 defaultPos, newPos;
    [HideInInspector]
    public Quaternion defaultRot, newRot;
    public float tickPerSec = 60f;

    public bool isActive = true;
    [HideInInspector]
    public bool useLerp = true;
    public float Accuracy = 0.05f;
    public float delta = 0.05f;
    public bool direction = true; // Если направление true , то движемся на новую точку, сответственно обратно

    Vector3 deltaPos;
    float framesLeft, framesCount;

    private void Awake()
    {
        if (Accuracy >= delta)
        {
            Accuracy = delta;
            Accuracy -= delta / 100;
        }

        defaultPos = transform.position;
        defaultRot = transform.rotation;

        newPos = newO.transform.position;
        newRot = newO.transform.rotation;
        

        framesCount = tickPerSec * delta;
        framesLeft = framesCount;
        deltaPos = (newPos - defaultPos) / framesCount;

        InvokeRepeating("ToPoint", 0, 1 / tickPerSec);
    }

    void ToPoint()
    {
        if (!isActive)
            return;

        if (useLerp)
        {
            if (direction)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, delta);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, delta);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, defaultPos, delta);
                transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, delta);
            }
        }
        else if (framesLeft > 0)
        {
            if (direction)
            {
                transform.position += deltaPos;
                //transform.rotation = Quaternion.Lerp(transform.rotation, newRot, delta);
                transform.rotation = new Quaternion(transform.rotation.x - newRot.x, transform.rotation.y - newRot.y, transform.rotation.z - newRot.z, transform.rotation.w - newRot.w);
            }
            else
            {
                transform.position -= deltaPos;
                transform.rotation = Quaternion.Lerp(transform.rotation, defaultRot, delta);
            }
            framesLeft--;
        }

        CheckTrueDir();
    }

    void CheckTrueDir()
    {
        float a = Accuracy * Vector3.Distance(defaultPos, newPos);
        if (Vector3.Distance(transform.position, newPos) <= a || Vector3.Distance(transform.position, defaultPos) <= a)
        {
            if (/*useLerp*/false)
            {
                if (direction)
                {
                    transform.position = newPos;
                    transform.rotation = newRot;
                }
                else
                {
                    transform.position = defaultPos;
                    transform.rotation = defaultRot;
                }
            }

            direction = !direction;
        }
    }
    /*
    float SmallestValue(Vector3 v)
    {
        if (v.x <= v.y && v.x <= v.z)
            return v.x;

        if (v.y <= v.x && v.y <= v.z)
            return v.y;

        if (v.z <= v.x && v.y <= v.x)
            return v.z;

        return v.x;
    }

    bool BetweenVectors(Vector3 v, Vector3 v1, Vector3 v2)
    {
        byte val = 0;

        if (v1.x > v2.x && v1.x > v.x && v.x > v2.x)
            val++;

        if (v1.x < v2.x && v1.x < v.x && v.x < v2.x)
            val++;


        if (v1.y > v2.y && v1.y > v.y && v.y > v2.y)
            val++;

        if (v1.y < v2.y && v1.y < v.y && v.y < v2.y)
            val++;


        if (v1.z > v2.z && v1.z > v.z && v.z > v2.z)
            val++;

        if (v1.z < v2.z && v1.z < v.z && v.z < v2.z)
            val++;

        if (val >= 3)
            return true;
        else
            return false;
    }*/
}


