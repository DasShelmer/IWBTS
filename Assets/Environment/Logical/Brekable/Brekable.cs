using UnityEngine;

[RequireComponent(typeof(LogicDelegate))]
public class Brekable : MonoBehaviour {

    public float timeBeforeDestroy = 2f;
    [Range(0f, 100.0f)]
    public float destroyOnProcient = 25;
    bool trigged = false;
    float framesBeforeDestroy = 1;
    float alphaPerFrame = 255 / 50;
    SpriteRenderer sr;
    LogicDelegate lD;

	void Start ()
    {
        lD = GetComponent<LogicDelegate>();
        lD.Do += Triggering;

        framesBeforeDestroy = timeBeforeDestroy / Time.fixedDeltaTime;

        if (GetComponent<SpriteRenderer>())
        {
            sr = GetComponent<SpriteRenderer>();
            alphaPerFrame = sr.color.a / framesBeforeDestroy;

            if (destroyOnProcient != 0)
                framesBeforeDestroy = (sr.color.a * 100 - destroyOnProcient) / (alphaPerFrame * 100);
        }
    }

    private void FixedUpdate()
    {
        if (sr && trigged)
        {
            if (framesBeforeDestroy > 0)
            {
                sr.color -= new Color(0, 0, 0, alphaPerFrame);
                framesBeforeDestroy -= 1;
            }
            else 
            {
                Destroy(gameObject, 0);
            }
        }
    }

    public void Triggering(bool A = true, bool B = false)
    {
        trigged = lD.End;
    }
}
