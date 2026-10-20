using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SoundType
    {
        Pickup,
        PlaceStar,
        UseKey,
        PlaceGear,
        CorrectCode,
        IncorrectCode,
        NumberSelect,
        Clear,
        Button
    }

    [System.Serializable]
    public class SoundEntry
    {
        public SoundType soundType;
        public AudioClip clip;
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundEntry[] sounds;

    // 効果音を再生する
    public void PlaySound(SoundType soundType)
    {
        if (audioSource == null) return;

        foreach (SoundEntry sound in sounds)
        {
            if (sound.soundType != soundType) continue;
            if (sound.clip == null) return;

            audioSource.PlayOneShot(sound.clip);
            return;
        }
    }
}
