using UnityEngine;

public class Turkey : Enemy
{
    private enum State { IdleWalking, IdleStanding, Discover, Attack }
    [SerializeField] private float[] idleWalkTimeMinMax = { 0.1f, 3f };
    [SerializeField] private float[] idleStandTimeMinMax = { 0.1f, 3f };
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private TurkeyAttack attack;

    private State state = State.IdleWalking;

    private float idleWalkTime = 0f;
    private float idleStandTime = 0f;

    private int idleMoveDir = 0;
    private int lastMoveDir = -1;

    private Player targetPlayer;
    public override void OnDetect(Player player)
    {
        if (State.IdleWalking == state)
        {
            targetPlayer = player;
            LookDir = (targetPlayer.transform.position - transform.position).normalized;
            animator.SetTrigger("Discover");
            ChangeState(State.Discover);
        }
    }
    
    public void OnAttackFinished()
    {
        attack.OnAttackEnd();
    }

    private void ChangeState(State newState)
    {
        if (state == newState) return;
        state = newState;
        animator.SetInteger("State", (int)state);
    }

    private void OnIdle()
    {
        if (State.IdleWalking == state)
            IdleWalk();
        else if (State.IdleStanding == state)
        {
            IdleStand();
        }
    }

    private void IdleStand()
    {
        if(idleStandTime <= 0)
        {
            idleStandTime = Random.Range(idleStandTimeMinMax[0], idleStandTimeMinMax[1]);

        }
        idleStandTime -= Time.deltaTime;
        if (idleStandTime <= 0)
            ChangeState(State.IdleWalking);
    }

    private void IdleWalk()
    {
        if (idleWalkTime <= 0)
        {
            idleWalkTime = Random.Range(idleWalkTimeMinMax[0], idleWalkTimeMinMax[1]);
            while (idleMoveDir == lastMoveDir)
                idleMoveDir = Random.Range(0, 7);
            lastMoveDir = idleMoveDir;
        }

        Vector2 currPos = transform.position;
        currPos += Dir8[idleMoveDir] * moveSpeed * Time.deltaTime;
        transform.position = currPos;

        LookDir = Dir8[idleMoveDir];

        idleWalkTime -= Time.deltaTime;
        if (idleWalkTime <= 0)
            ChangeState(State.IdleStanding);
    }

    private void Update()
    {
        if (State.IdleWalking == state || State.IdleStanding == state)
        {
            OnIdle();
        }
        else if (State.Attack == state)
        {
            attack.UpdateAttack(targetPlayer);
        }
        SetAnimationLookDir();
    }

    private void SetAnimationLookDir()
    {
        animator.SetFloat("MoveDir.X", LookDir.x);
        animator.SetFloat("MoveDir.Y", LookDir.y);
    }
}
