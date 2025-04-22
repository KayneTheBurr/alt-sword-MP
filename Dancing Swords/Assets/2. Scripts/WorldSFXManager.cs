using JetBrains.Annotations;
using UnityEngine;

public class WorldSFXManager : MonoBehaviour
{
    public static WorldSFXManager instance;

    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;
    public AudioClip smallHitSound;
    public AudioClip largeHitSound;
    public AudioClip blockSound;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
   }
    public void PlaySmallHitSound()
    {
        sfxPlayer.pitch = 1;
        sfxPlayer.clip = smallHitSound;
        sfxPlayer.PlayOneShot(smallHitSound);
    }
    public void PlayLargeHitSound()
    {
        sfxPlayer.pitch = 1;
        sfxPlayer.clip = largeHitSound;
        sfxPlayer.PlayOneShot(largeHitSound);
    }
    public void PlayBlockSound()
    {
        sfxPlayer.pitch = 2.75f;
        sfxPlayer.clip = blockSound;
       sfxPlayer.PlayOneShot(blockSound);
    }
}