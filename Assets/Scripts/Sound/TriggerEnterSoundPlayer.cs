using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TriggerEnterSoundPlayer : MonoBehaviour {

    AudioSource aS;
    public AudioClip[] acs;

    public string[] antiPlaySoundTags;
    public string[] playSoundTags;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();

        if (acs.Length == 0)
            Debug.LogError("Audio Clip array is empty!", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (acs.Length != 0)
        {
            bool tagOk = false;

            for (int k = 0; k < antiPlaySoundTags.Length; k++)
            {
                if (collision.gameObject.tag == antiPlaySoundTags[k])
                    return;
            }

            for (int k = 0; k < playSoundTags.Length; k++)
            {
                if (collision.gameObject.tag == playSoundTags[k])
                {
                    tagOk = true;
                    break;
                }
            }

            if (tagOk)
            {
                aS.clip = acs[Random.Range(0, acs.Length)];
                aS.Play();
            }
        }
    }
}
