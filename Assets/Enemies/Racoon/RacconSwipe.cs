using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class RacconSwipe : EnemyAttack
{
    [SerializeField] private float runSpeed = 2f;
    [SerializeField] private GameObject swipeObject;
    private void Awake()
    {
        attackRange = 1f;
        attackPower = 1;
    }

    protected override void OnAttackLand(Player player)
    {
        player.OnAttacked(attackPower);
    }

    public override void OnNotInAttackRange(Player player)
    {
        if (isAttacking) return;

        Vector2 lookDir  = (player.transform.position - parentEntity.transform.position).normalized;
        
        Vector2 currPos = parentEntity.transform.position;
        currPos += lookDir * runSpeed * Time.deltaTime;
        parentEntity.transform.position = currPos;

        parentEntity.LookDir = lookDir;
    }

    public override void OnInAttackRange(Player player)
    {
        if (canAttack)
        {
            OnAttackStart(player);
        }
    }

    public override void OnAttackStart(Player player)
    {
        parentEntity.Animator.SetTrigger("Swipe");
        Vector2 lookDir = parentEntity.LookDir;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // face velocity
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        canAttack = false;
        isAttacking = true;
    }

    public override void OnAttackEnd()
    {
        canAttack = true;
        isAttacking = false;
    }
}
