using UnityEngine;

public class Raccoon : Enemy
{
    private enum State { Idle, Discover, Attack }

    private State state = State.Idle;
    private Animator animator;

    [SerializeField] private float[] idleWalkTimeMinMax = { 1f, 5f};
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float attackRange = 1f;

    private static Vector2[] Dir8 = 
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
    private float idleWalkTime = 0f;
    private int idleMoveDir = 0;
    private int lastMoveDir = -1;
    private Vector2 lookDir;

    private Player targetPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void ChangeState(State newState)
    {
        if (state == newState) return;
        state = newState;
    }

    private void OnIdle()
    {
        if(idleWalkTime <= 0)
        {
            idleWalkTime = Random.Range(idleWalkTimeMinMax[0], idleWalkTimeMinMax[1]);
            while(idleMoveDir == lastMoveDir)
                idleMoveDir = Random.Range(0, 7);
            lastMoveDir = idleMoveDir;
        }

        Vector2 currPos = transform.position;
        currPos += Dir8[idleMoveDir] * moveSpeed * Time.deltaTime;
        transform.position = currPos;

        lookDir = Dir8[idleMoveDir];

        idleWalkTime -= Time.deltaTime;
    }

    private void OnDiscover(Player player)
    {
        targetPlayer = player;
        animator.SetTrigger("Discover");
        ChangeState(State.Discover);
    }

    private void WhileAttacking()
    {
        if (Vector2.Distance(targetPlayer.transform.position, transform.position) > attackRange)
            MoveToAttack();
        else
        {
            //TriggerAttack;
        }
    }

    private void MoveToAttack()
    {
        lookDir = (targetPlayer.transform.position - transform.position).normalized;

        Vector2 currPos = transform.position;
        currPos += lookDir * moveSpeed * Time.deltaTime;
        transform.position = currPos;
    }

    private void Update()
    {
        if (State.Idle == state)
        {
            OnIdle();
        }
        else if(State.Attack == state)
        {
            WhileAttacking();
        }
        SetAnimationLookDir();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (state == State.Idle)
            {
                OnDiscover(other.GetComponent<Player>());
            }
        }
    }

    private void SetAnimationLookDir()
    {
        animator.SetFloat("MoveDir.X", lookDir.x);
        animator.SetFloat("MoveDir.Y", lookDir.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
