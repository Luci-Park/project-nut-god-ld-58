using UnityEngine;

public enum AudioType
{
    // Background sounds
    INTRO_BACKGROUND,
    LVL_BACKGROUND,
    GODDESS_BACKGROUND,
    BOSS_BACKGROUND,

    // Character sounds
    PLAYER_ATTACK,
    PLAYER_DASH,
    PLAYER_HURT,
    PLAYER_FOOTSTEP,

    // NPC sounds
    NPC_ATTACK,
    NPC_HURT
    // TODO: more to be added here...
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static AudioManager instance;
    private AudioSource audioSource;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioType audio, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)audio], volume);
    }
}
