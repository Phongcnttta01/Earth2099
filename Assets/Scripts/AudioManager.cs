using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource baiscAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip energyClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip cutClip;
    [SerializeField] private AudioClip reCutClip;

    public void PlayShootSound()
    {
        effectAudioSource.PlayOneShot(shootClip);
    }

    public void PlayReloadSound()
    {
        effectAudioSource.PlayOneShot(reloadClip);
    }

    public void PlayEnergySound()
    {
        effectAudioSource.PlayOneShot(energyClip);
    }

    public void PlayExplosionSound()
    {
        effectAudioSource.PlayOneShot(explosionClip);
    }

    public void CutSound()
    {
        effectAudioSource.PlayOneShot(cutClip);
    }

    public void ReCutSound()
    {
        effectAudioSource.PlayOneShot(reCutClip);
    }
    public void PlayBossSound()
    {
        bossAudioSource.Play();
        baiscAudioSource.Stop();
    }

    public void PlayBasisSound()
    {
        bossAudioSource.Stop();
        baiscAudioSource.Play();
    }

    public void StopAudio()
    {
        effectAudioSource.Stop();
        bossAudioSource.Stop();
        baiscAudioSource.Stop();
    }
}
