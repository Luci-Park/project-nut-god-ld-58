using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Feather : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float moveSpeed;
    private Vector2 moveDir;
    public void Initialize(float speed, Vector2 direction, Vector2 position)
    {
        if(!spriteRenderer) spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        moveSpeed = speed;
        moveDir = direction;
        transform.position = position;

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg; // face velocity
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            Vector2 pos = transform.position;
            pos += moveDir * moveSpeed * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            Despawn();
        }
    }
    public void Despawn()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            Debug.Assert(player);
            player.OnAttacked(1);
            Despawn();
        }
    }
}
