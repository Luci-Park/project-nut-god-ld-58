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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Player player = collider.GetComponent<Player>();
            Debug.Assert(player);
            Attack(player);
        }
    }
}
