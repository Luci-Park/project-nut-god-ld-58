using UnityEngine;
using UnityEngine.InputSystem;

public class TailSwipe : MonoBehaviour
{
    private Player player;

    [SerializeField] private float radius = 1f;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 attackDir = player.Movement.MoveDir;

        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg; // face velocity
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
