using JetBrains.Annotations;
using UnityEngine;

public class WorldSFXManager : MonoBehaviour
{
    public static WorldSFXManager instance;

    public AudioSource sfxPlayer;
    public AudioClip hitSound;
    public AudioClip blockSound;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
   }
    public void PlayHitSound()
    {
        sfxPlayer.clip = hitSound;
        sfxPlayer.PlayOneShot(hitSound);
    }
    public void PlayBlockSound()
    {
        sfxPlayer.clip = blockSound;
       sfxPlayer.PlayOneShot(blockSound);
    }
}