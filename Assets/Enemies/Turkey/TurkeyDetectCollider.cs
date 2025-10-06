using UnityEngine;

public class TurkeyDetectCollider : EnemyDetectCollider
{
    private CapsuleCollider2D lineOfSightCollider;
    private SpriteRenderer sp;
    

    protected override void Awake()
    {
        base.Awake();
        lineOfSightCollider = GetComponent<CapsuleCollider2D>();
        sp = parentEntity.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 lookDir = parentEntity.LookDir;
        Vector2 offset = lineOfSightCollider.offset;

        if (sp.flipX)
            offset.x = 3.5f;
        else
            offset.x = -3.5f;
        lineOfSightCollider.offset = offset;
    }
}
