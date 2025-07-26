using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Vector3 direction;
    private float speed = 10f;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Handle bullet hitting an enemy
            other.gameObject.SetActive(false); // Deactivate enemy
            // Return bullet to pool
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else if (other.CompareTag("Boundary"))
        {
            // Handle bullet hitting the boundary
            // Return bullet to pool
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }
}
