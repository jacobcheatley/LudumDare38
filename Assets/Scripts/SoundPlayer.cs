using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip buy;
    [SerializeField] private AudioClip sell;
    [SerializeField] private AudioClip fuel;
    [SerializeField] private AudioClip fail;
    [SerializeField] private AudioClip crack;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip bump;
    [SerializeField] private AudioClip[] aboveGroundClips;
    [SerializeField] private AudioClip[] belowGroundClips;

    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource crunchLoop;
    [SerializeField] private AudioSource ambience;

    public void PlayBuy()
    {
        sfx.PlayOneShot(buy, 0.3f);
    }

    public void PlaySell()
    {
        sfx.PlayOneShot(sell, 0.3f);
    }
    
    public void PlayFuel()
    {
        sfx.PlayOneShot(fuel);
    }

    public void PlayFail()
    {
        sfx.PlayOneShot(fail, 0.7f);
    }

    public void PlayCrack()
    {
        sfx.PlayOneShot(crack, 0.3f);
    }

    public void PlayPop()
    {
        sfx.PlayOneShot(pop);
    }

    public void PlayBump()
    {
        sfx.PlayOneShot(bump);
    }

    public void PlayAboveGround(float volume)
    {
        ambience.PlayOneShot(aboveGroundClips[Random.Range(0, aboveGroundClips.Length - 1)], volume);
    }

    public void PlayBelowGround(float volume)
    {
        ambience.PlayOneShot(belowGroundClips[Random.Range(0, aboveGroundClips.Length - 1)], volume);
    }

    public void SetCrunchLoopVolume(float volume)
    {
        crunchLoop.volume = volume;
    }
}
