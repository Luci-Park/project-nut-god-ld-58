using UnityEngine;

public class EnemyDetectCollider : MonoBehaviour
{
    protected Enemy parentEntity;
    
    protected virtual void Awake()
    {
        parentEntity = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            Debug.Assert(player && parentEntity);
            parentEntity.OnDetect(player);
        }
    }

}
