using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicSecondDoubleJump : MonoBehaviour
{
    public float regenTime = 1.5f;

    SpriteRenderer sR;
    PolygonCollider2D pC;
    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        pC = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Break();

        var player = collision.gameObject;
        if (player == GameObject.Find("Manager").GetComponent<Manager>().Player)
        {
            var m = player.GetComponent<Movement>();
            m.jumpsLeft = m.countOfJumps;
        }

        Invoke("Regen", regenTime);
    }

    void Break()
    {
        sR.enabled = false;
        pC.enabled = false;
    }

    void Regen()
    {
        sR.enabled = true;
        pC.enabled = true;
    }
}
