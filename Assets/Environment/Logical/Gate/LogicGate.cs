using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfChange : int { Lerp, ByFixedUpdate}

[RequireComponent(typeof(LogicDelegate))]
public class LogicGate : MonoBehaviour {

    [Range(0.0000001f, 1f)]
    public float speed = 0.2f;
    public TypeOfChange rotationType = TypeOfChange.Lerp;
    public TypeOfChange movementType = TypeOfChange.ByFixedUpdate;

    public LogicOperator currentOperator = LogicOperator.Or;
    public bool Active = false;

    public bool HideThis = true;

    GameObject gate;
    LogicDelegate lD;

    Vector3 startPos;
    Quaternion startRot;
    void Start()
    {
        lD = GetComponent<LogicDelegate>();
        lD.Do += Do;

        gate = transform.parent.gameObject;

        startPos = gate.transform.position;
        startRot = gate.transform.rotation;

        transform.parent = null;
        if (HideThis)
        {
            if (GetComponent<SpriteRenderer>())
                GetComponent<SpriteRenderer>().enabled = false;
            
            if (GetComponent<MeshRenderer>())
                GetComponent<MeshRenderer>().enabled = false;
        }

    }

    void Do(bool a, bool b)
    {
        Active = Logic.GetValue(currentOperator, a, b);

        if (Active)
        {
            if (rotationType == TypeOfChange.ByFixedUpdate)
                StartCoroutine(FixedRotateTo(gate.transform, transform.rotation));
            if (rotationType == TypeOfChange.Lerp)
                StartCoroutine(LerpRotateTo(gate.transform, transform.rotation));

            if (movementType == TypeOfChange.ByFixedUpdate)
                StartCoroutine(FixedMoveTo(gate.transform, transform.position));
            if (movementType == TypeOfChange.Lerp)
                StartCoroutine(LerpMoveTo(gate.transform, transform.position));
        }
        else
        {
            if (rotationType == TypeOfChange.ByFixedUpdate)
                StartCoroutine(FixedRotateTo(gate.transform, startRot));
            if (rotationType == TypeOfChange.Lerp)
                StartCoroutine(LerpRotateTo(gate.transform, startRot));

            if (movementType == TypeOfChange.ByFixedUpdate)
                StartCoroutine(FixedMoveTo(gate.transform, startPos));
            if (movementType == TypeOfChange.Lerp)
                StartCoroutine(LerpMoveTo(gate.transform, startPos));
        }
    }

    public IEnumerator FixedRotateTo(Transform obj, Quaternion target)
    {
        Quaternion from = obj.rotation;
        for (float t = 0f; t < 1f; t += speed * Time.deltaTime)
        {
            obj.rotation = Quaternion.Lerp(from, target, t);
            yield return null;
        }
        obj.rotation = target;
    }
    public IEnumerator LerpRotateTo(Transform obj, Quaternion target)
    {
        float startTime = Time.time;
        while (Time.time < startTime + speed)
        {
            transform.rotation = Quaternion.Lerp(obj.rotation, target, (Time.time - startTime) / speed);
            yield return null;
        }
        obj.rotation = target;
    }
    public IEnumerator FixedMoveTo(Transform obj, Vector3 target)
    {
        Vector3 from = obj.position;
        for (float t = 0f; t < 1f; t += speed * Time.deltaTime)
        {
            obj.position = Vector3.Lerp(from, target, t);
            yield return null;
        }
        obj.position = target;
    }
    public IEnumerator LerpMoveTo(Transform obj, Vector3 target)
    {
        float startTime = Time.time;
        while (Time.time < startTime + speed)
        {
            transform.position = Vector3.Lerp(obj.position, target, (Time.time - startTime) / speed);
            yield return null;
        }
        obj.position = target;
    }
}

