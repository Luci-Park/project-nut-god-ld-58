using UnityEngine;

public class PlaySoundEnter : StateMachineBehaviour
{
    [SerializeField] private AudioType audio;
    [SerializeField, Range(0,1)] private float volume = 1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.PlaySound(audio, volume);
    }
}