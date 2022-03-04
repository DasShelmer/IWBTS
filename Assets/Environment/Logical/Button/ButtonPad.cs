using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPad : MonoBehaviour
{
    public bool Active = false;

    public string[] antiActivationNames = { "Bullet" };
    public string[] ActivationTags = { "Player_", "Enemy", "Physic" };

    public float timeBeforeChangeValue = 0.5f;
    [Range(0.0f, 1.0f)]
    public float percentBlackout = 0.2f;

    float maxSize, minSize, framesBeforeChange, sizePerFrame, remainingFrames;
    Color maxBlackout, minBlackout;

    public List<GameObject> inCollider;

    LogicButton lB;
    SpriteRenderer sR;
    private void Start()
    {
        lB = transform.parent.gameObject.GetComponent<LogicButton>();
        sR = GetComponent<SpriteRenderer>();

        maxSize = transform.localScale.x;
        minSize = maxSize * 0.7f;

        minBlackout = sR.color;
        maxBlackout = new Color(sR.color.r - percentBlackout, sR.color.g - percentBlackout, sR.color.b - percentBlackout, sR.color.a);

        framesBeforeChange = timeBeforeChangeValue / Time.fixedDeltaTime;
        sizePerFrame = (maxSize - minSize) / framesBeforeChange;

        InvokeRepeating("CheckListValidate", 0, 2);
    }

    void CheckListValidate()
    {
        foreach (GameObject g in inCollider.ToArray())
        {
            if (!g)
            {
                inCollider.Remove(g);
            }
        }
    }

    private void FixedUpdate()
    {
        if (remainingFrames > 0)
        {
            if (Active && transform.localScale.x >= minSize)
            {
                transform.localScale -= Vector3.right * sizePerFrame;
            }
            else if (transform.localScale.x <= maxSize)
            {
                transform.localScale += Vector3.right * sizePerFrame;
            }
            remainingFrames--;
        }
    }

    void OnChangeValue(bool value = false)
    {
        Active = value;
        remainingFrames = framesBeforeChange;

        if (value)
            sR.color = maxBlackout;
        else
            sR.color = minBlackout;

        if (lB.Exit)
            lB.Do();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            inCollider.Add(collision.gameObject);

            OnChangeValue(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject g in inCollider.ToArray())
        {
            if (collision.gameObject == g)
            {
                inCollider.Remove(g);

                if (inCollider.Count == 0)
                {
                    OnChangeValue(false);
                }
            }
        }
    }
}
