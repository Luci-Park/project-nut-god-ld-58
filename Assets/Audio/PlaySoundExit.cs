using UnityEngine;

public class PlaySoundExit : StateMachineBehaviour
{
    [SerializeField] private AudioType audio;
    [SerializeField, Range(0, 1)] private float volume = 1;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.PlaySound(audio, volume);
    }
}
