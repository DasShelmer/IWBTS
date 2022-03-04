using UnityEngine;
using System.Collections;
public class SmoothFollowCamera : MonoBehaviour
{
    public float dampTime = 0.25f;
    public bool enableDampTimeCorrection = false;

    private Vector3 velocity = Vector3.zero;

    public Transform target;
    Rigidbody2D rbPlayer;
    Transform cam;
    void Start()
    {
        if (target == null)
            target = GameObject.Find("Manager").GetComponent<Manager>().Player.transform;

        if (transform.parent.name == target.name)
            transform.SetParent(null);

        cam = Camera.main.transform;

        rbPlayer = target.GetComponent<Rigidbody2D>();

        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            cam.position = destination;
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            cam.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);


            if (enableDampTimeCorrection)
            {
                Vector2 screenPosition = Camera.main.WorldToScreenPoint(target.position);
                if (screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
                {
                    dampTime *= 0.9f;
                }
                else
                {
                    float deltaVel = (Mathf.Abs(rbPlayer.velocity.x) + Mathf.Abs(rbPlayer.velocity.y)) / 2;

                    dampTime = 1 / deltaVel;
                }
            }
        }
    }
}
