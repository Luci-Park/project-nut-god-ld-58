using UnityEngine;

public class TurkeyAttack : EnemyAttack
{
    [SerializeField] private FeatherShooter featherShooter;
    private void Awake()
    {
        attackPower = 1;
    }
    public override bool IsInAttackRange(Vector2 playerPos, Vector2 enemyPos)
    {
        return true;
    }


    protected override void OnAttackLand(Player player)
    {
        player.OnAttacked(attackPower);
    }

    public override void OnNotInAttackRange(Player player)
    {
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
        canAttack = false;
        isAttacking = true;
        parentEntity.LookDir = (player.transform.position - parentEntity.transform.position).normalized;

    }

    public override void OnAttackEnd()
    {
        canAttack = true;
        isAttacking = false;
    }
    public void Shoot()
    {
        featherShooter.Shoot();
    }
}
