using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TailSwipe : MonoBehaviour
{
    private Player player;

    [SerializeField] private GameObject hitboxObject;
    [SerializeField] private int power = 2;

    [SerializeField] private float attackDuration = 0.3f;

    [SerializeField] private bool canSwipe = true;
    void Awake()
    {
        player = GetComponentInParent<Player>();
        hitboxObject.SetActive(false);
    }
    void OnEnable()
    {
        canSwipe = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || !canSwipe) return;

        StartCoroutine(AttackRoutine());
    }

    private void Attack(Enemy enemy)
    {
        enemy.OnAttacked(power);
    }

    private IEnumerator AttackRoutine()
    {
        canSwipe = false;
        Vector2 attackDir = player.Movement.MoveDir;

        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg; // face velocity
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = player.transform.position + (Vector3)(attackDir * radius);

        player.Animator.SetTrigger("TailSwipe");

        hitboxObject.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        hitboxObject.SetActive(false);
        canSwipe = true;

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

    public bool GetCanSwipe()
    {
        return this.canSwipe;
    }
    public void SetCanSwipe(bool swipeable)
    {
        this.canSwipe = false;
    }
}
