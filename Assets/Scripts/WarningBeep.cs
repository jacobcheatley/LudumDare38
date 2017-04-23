using UnityEngine;

public class WarningBeep : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip clip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBeep()
    {
        source.PlayOneShot(clip, 0.8f);
    }
}
