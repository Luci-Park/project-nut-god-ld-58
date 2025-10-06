using UnityEngine;

public class FeatherShooter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    public Feather[] feathers;
    public SpriteRenderer turkeyEntity;
    Vector2[] directions;


    private void Awake()
    {
        foreach (var feather in feathers)
            feather.Despawn();
        directions = new Vector2[]{ Vector2.left,
            Quaternion.Euler(0, 0, 45f) * Vector2.left,
            Quaternion.Euler(0, 0, -45f) * Vector2.left,
            Vector2.right,
            Quaternion.Euler(0, 0, 45f) * Vector2.right,
            Quaternion.Euler(0, 0, -45f) * Vector2.right
        };
        ;
    }

    public void Shoot()
    {
        int dir;
        if (turkeyEntity.flipX)
        {
            transform.localPosition = new Vector2(0.5f, 0);
            dir = 3;
        }
        else
        {
            transform.localPosition = new Vector2(-0.5f, 0);
            dir = 0;
        }

        for(int i =0; i < 3; i++)
        {
            feathers[i].Initialize(moveSpeed, directions[i + dir], transform.position);
        }
    }
    
}
