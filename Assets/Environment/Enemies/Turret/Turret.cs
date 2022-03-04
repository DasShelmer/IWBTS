using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float timeBeforeFire = 0.333f;
    public float radius = 20;
    public bool fireThrowPhysicObjs = true;
    Transform Target;
    public GameObject BulletPrefab;
    public float BulletSpeed = 1;

    public Color ColorOfBullet { get; set; }
    Vector3 lookDirection;
    Quaternion targetRotation;
    void Start()
    {
        ColorOfBullet = Color.clear;

        if (!Target)
            Target = GameObject.Find("Manager").GetComponent<Manager>().Player.transform;

        /*if (BulletPrefab)
            BulletSpeed = BulletSpeed / BulletPrefab.gameObject.GetComponent<Rigidbody2D>().mass;*/

        InvokeRepeating("Shoot", timeBeforeFire, timeBeforeFire);
    }

    void FixedUpdate()
    {
        lookDirection = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = targetRotation;
    }

    void Shoot()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(
                    new Vector2(transform.position.x, transform.position.y),
                    lookDirection,
                    radius);

        if (fireThrowPhysicObjs && hit.Length > 1)
        {
            if (hit[0].transform == Target)
            {
                Fire();
            }

            if (((hit[0].transform.tag == "Physic" || hit[1].transform.tag == "Physic") || (hit[0].collider.isTrigger || hit[1].collider.isTrigger)) &&
                (hit[0].transform == Target || hit[1].transform == Target))
            {
                Fire();
            }

        }
        else if (hit.Length != 0)
        {
            if (hit[0].transform == Target)
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        var bullet = Instantiate(BulletPrefab, transform.position, transform.GetChild(0).transform.rotation);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * BulletSpeed, ForceMode2D.Impulse);
        
        var bCom = bullet.GetComponent<Bullet>();
        bCom.radius = radius;
        bCom.turretPos = transform.position;
        bCom.target = Target;
        bCom.rocketSpeed = BulletSpeed;
    }

    float Module(float f)
    {
        if (f < 0)
            return f * -1;
        else
            return f;
    }
}
