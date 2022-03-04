using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Coloring col;

    public bool isRocket = false;
    public float fuelTime = 8f;

    float fuelFrames = 100f;

    [HideInInspector]
    public float rocketSpeed = 1f;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public float radius = 10f;
    [HideInInspector]
    public Vector3 turretPos = Vector3.zero;

    private void Start()
    {
        if (isRocket)
        {
            if (fuelTime > 0)
                fuelFrames = fuelTime * (1 / Time.fixedDeltaTime);
        }

        if (turretPos == Vector3.zero)
            turretPos = transform.position;

        GetComponent<SpriteRenderer>().color = Color.clear;
        Invoke("Begin", 0.1f);
    }


    private void Begin()
    {
        col = GameObject.Find("Manager").GetComponent<Coloring>();
        Coloring.colorUp += ColorUp;

        ColorUp();
    }

    private void FixedUpdate()
    {
        Vector3 dest = turretPos - transform.position;

        if (dest.x > radius || dest.y > radius)
            OnCollisionEnter2D();

        if (isRocket)
        {
            if (fuelFrames > 0 || fuelTime <= 0)
            {
                var lookDirection = target.transform.position - transform.position;
                float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
                var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.rotation = targetRotation;

                gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * rocketSpeed;

                fuelFrames--;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }

    public void ColorUp()
    {
        GetComponent<SpriteRenderer>().color = col.cEnemy[col.currentIndex];
    }

    void OnCollisionEnter2D()
    {
        Coloring.colorUp -= ColorUp;
        Destroy(gameObject, 0.025f);
    }
}
