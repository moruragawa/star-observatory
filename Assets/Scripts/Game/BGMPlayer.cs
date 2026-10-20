using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgmClip;

    private void Start()
    {
        if (audioSource == null || bgmClip == null) return;

        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
