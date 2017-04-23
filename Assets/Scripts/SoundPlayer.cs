using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buy;
    [SerializeField] private AudioClip sell;
    [SerializeField] private AudioClip fuel;
    [SerializeField] private AudioClip fail;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayBuy()
    {
        audio.PlayOneShot(buy);
    }

    public void PlaySell()
    {
        audio.PlayOneShot(sell);
    }
    
    public void PlayFuel()
    {
        audio.PlayOneShot(fuel);
    }

    public void PlayFail()
    {
        audio.PlayOneShot(fail);
    }
}
