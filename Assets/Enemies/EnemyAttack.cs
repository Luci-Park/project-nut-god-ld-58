using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected Enemy parentEntity;

    [SerializeField] protected int attackPower;
    [SerializeField] protected float attackRange;
    protected bool canAttack = true;
    protected bool isAttacking = false;
    private float attackRangedSqr => attackRange * attackRange;

    public void UpdateAttack(Player target)
    {
        if (IsInAttackRange(target.transform.position, parentEntity.transform.position))
            OnInAttackRange(target);
        else
            OnNotInAttackRange(target);
    }


    public virtual bool IsInAttackRange(Vector2 playerPos, Vector2 enemyPos)
    {
        return (playerPos - enemyPos).sqrMagnitude <= attackRange;
    }
    public abstract void OnNotInAttackRange(Player player);
    public abstract void OnInAttackRange(Player player);
    public abstract void OnAttackStart(Player player);
    public abstract void OnAttackEnd();

    protected abstract void OnAttackLand(Player player);

    private void OnValidate()
    {
        parentEntity = GetComponentInParent<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            Debug.Assert(player && parentEntity);
            OnAttackLand(player);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(parentEntity.transform.position, attackRange);
    }
}
