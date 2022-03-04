using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ColliderEnterSoundPlayer : MonoBehaviour {

    AudioSource aS;
    public AudioClip[] acs;

    public string[] antiPlayNames;
    public string[] antiPlaySoundTags;
    public string[] playSoundTags;

    private void Awake()
    {
        aS = GetComponent<AudioSource>();

        if (acs.Length == 0)
            Debug.LogError("Audio Clip array is empty!", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (acs.Length != 0)
        {
            foreach(var i in antiPlaySoundTags)
            {
                if (collision.gameObject.tag == i)
                    return;
            }
            
            foreach (var i in antiPlayNames)
            {
                if (collision.gameObject.name.Contains(i))
                    return;
            }

            foreach(var i in playSoundTags)
            {
                if (collision.gameObject.tag == i)
                {
                    aS.clip = acs[Random.Range(0, acs.Length)];
                    aS.Play();
                    break;
                }
            }
        }
    }
}
