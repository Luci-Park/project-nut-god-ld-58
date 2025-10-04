using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerHealth health;
    
    public PlayerMovement Movement {  get { return movement; } }
    public PlayerHealth Health { get { return health; }}


    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
    }
}
