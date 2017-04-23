using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buy;
    [SerializeField] private AudioClip sell;
    [SerializeField] private AudioClip fuel;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioClip crack;

    private AudioSource audio;

    [SerializeField] private AudioSource crunchLoop;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayBuy()
    {
        audio.PlayOneShot(buy, 0.3f);
    }

    public void PlaySell()
    {
        audio.PlayOneShot(sell, 0.3f);
    }
    
    public void PlayFuel()
    {
        audio.PlayOneShot(fuel);
    }

    public void PlayFail()
    {
        audio.PlayOneShot(fail, 0.7f);
    }

    public void PlayCrack()
    {
        audio.PlayOneShot(crack, 0.3f);
    }

    public void SetCrunchLoopVolume(float volume)
    {
        crunchLoop.volume = volume;
    }
}
