using UnityEngine;
public enum Direction : int { Left , Right, Up, Null}
public class Movement : MonoBehaviour {

    public bool isGrounded = false;
    public int countOfJumps = 1;
    public float groundCheckDist = 1.001f;
    public float speed = 1, jump = 1;

    public string[] groundTags =
    {
        "Ground",
        "Physic",
        "Logic"
    };

    public string[] antiGroundTags =
    {
        "Player",
        "Enemy",
        "WinObj"
    };

    public string[] antiGroundNames =
    {
        "lBooster"
    };

    Rigidbody2D playerBody;

    [HideInInspector]
    public int jumpsLeft;

    PlaySoundOnJump psoj = null;
    void Start()
    {
        psoj = GetComponent<PlaySoundOnJump>();

        playerBody = GetComponent<Rigidbody2D>();
        jumpsLeft = countOfJumps - 1;
    }


    void FixedUpdate()
    {
        isGrounded = false;

        //if (countOfJumps < 1)
        //    return;

        RaycastHit2D[] hit = Physics2D.RaycastAll(
                    new Vector2(transform.position.x, transform.position.y),
                    -Vector2.up,
                    groundCheckDist);


        for (int i = 0; i < hit.Length; i++)
        {
            bool tagOk = false;

            for (int k = 0; k < antiGroundNames.Length; k++)
            {
                if (hit[i].transform.gameObject.name == antiGroundNames[k])
                {
                    if (hit[i].transform.gameObject.GetComponent<LogicBooster>())
                    {
                        var lB = hit[i].transform.gameObject.GetComponent<LogicBooster>();
                        if (!lB.AddDoubleJump)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            for (int k = 0; k < antiGroundTags.Length; k++)
            {
                if (hit[i].transform.gameObject.tag == antiGroundTags[k])
                    return;
            }

            for (int k = 0; k < groundTags.Length; k++)
            {
                if (hit[i].transform.gameObject.tag == groundTags[k])
                {
                    tagOk = true;
                    break;
                }
            }
            

            if (tagOk)
            {
                jumpsLeft = countOfJumps - 1;
                isGrounded = true;
                break;
            }
        }
    }

    public void Motion (Direction d)
    {
        if (d == Direction.Left)
        {
            playerBody.AddForce(-Vector2.right * speed, ForceMode2D.Force);
        }

        if (d == Direction.Right)
        {
            playerBody.AddForce(Vector2.right * speed, ForceMode2D.Force);
        }

        if (d == Direction.Up && (isGrounded || jumpsLeft > 0))
        {
            if (jumpsLeft == 0)
                isGrounded = false;

            playerBody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            try { psoj.Play(); } catch { }

            jumpsLeft--;
        }
    }
}
