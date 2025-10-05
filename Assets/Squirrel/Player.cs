using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerHealth health;
    private Animator animator;
    
    public PlayerMovement Movement {  get { return movement; } }
    public PlayerHealth Health { get { return health; }}
    public Animator Animator { get { return animator; } }
    public void OnAttacked(int power)
    {
        Debug.Log($"Player attacked by the power of {power}");
    }

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }
}
