using UnityEngine;
using UnityEngine.InputSystem;

public class TailSwipe : MonoBehaviour
{
    private Player player;

    [SerializeField] private int power = 2;
    [SerializeField] private float radius = 0.1f;
 
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 attackDir = player.Movement.MoveDir;

        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg; // face velocity
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = player.transform.position + (Vector3)(attackDir * radius);

        player.Animator.SetTrigger("TailSwipe");
    }

    private void Attack(Enemy enemy)
    {
        enemy.OnAttacked(power);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            Debug.Assert(enemy);
            Attack(enemy);
        }
    }


}
