using UnityEngine;

public class Raccoon : Enemy
{
    private enum State { Idle, Attack }
    [SerializeField] private float[] idleWalkTimeMinMax = { 0.1f, 3f};
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private RacconSwipe swipe;
        
    private State state = State.Idle;

    private float idleWalkTime = 0f;
    private int idleMoveDir = 0;
    private int lastMoveDir = -1;

    private Player targetPlayer;

    private void ChangeState(State newState)
    {
        if (state == newState) return;
        state = newState;
    }

    public override void OnDetect(Player player)
    {
        targetPlayer = player;
        LookDir = (targetPlayer.transform.position - transform.position).normalized;
        animator.SetTrigger("Discover");
        ChangeState(State.Attack);
    }
    /// <summary>
    /// for animation purposes
    /// dirty, I know
    /// </summary>
    public void OnAttackFinished()
    {
        swipe.OnAttackEnd();
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

        LookDir = Dir8[idleMoveDir];

        idleWalkTime -= Time.deltaTime;
    }

    private void Update()
    {
        if (State.Idle == state)
        {
            OnIdle();
        }
        else if(State.Attack == state)
        {
            swipe.UpdateAttack(targetPlayer);
        }
        SetAnimationLookDir();
    }

    private void SetAnimationLookDir()
    {
        animator.SetFloat("MoveDir.X", LookDir.x);
        animator.SetFloat("MoveDir.Y", LookDir.y);
    }
}
