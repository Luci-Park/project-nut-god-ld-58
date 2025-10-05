using UnityEngine;

public class Enemy : MonoBehaviour
{

    public void OnAttacked(int power)
    {
        Debug.Log($"{gameObject.name} Attacked at the power of {power}");
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
