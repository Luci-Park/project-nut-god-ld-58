using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Vector2 LookDir { get; set; }
    public Animator Animator => animator;

    protected static Vector2[] Dir8 =
    {
        new Vector2( 0,  1),
        new Vector2( 1,  1),
        new Vector2( 1,  0),
        new Vector2( 1, -1),
        new Vector2( 0, -1),
        new Vector2(-1, -1),
        new Vector2(-1,  0),
        new Vector2(-1,  1),
    };

    protected Animator animator;
    public void OnAttacked(int power)
    {
        Debug.Log($"{gameObject.name} Attacked at the power of {power}");
    }

    public abstract void OnDetect(Player player);

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Attack(Player player)
    {
        player.OnAttacked(3);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Debug.Assert(player);
            Attack(player);
        }
    }


}
