using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float direction = 1f;
    private float speed = 0.3f;
    private float distanciaBajada = 0.2f;

    private bool hasChangeDirection;

    private void Start()
    {
        hasChangeDirection = false;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (hasChangeDirection) return;

        foreach (Transform enemy in transform)
        {
            if (enemy.position.x <= (GameManager.Instance.leftBound.x + 2f) || enemy.position.x >= (GameManager.Instance.rightBound.x - 2f))
            {
                direction *= -1f;
                transform.position += Vector3.down * distanciaBajada;
                hasChangeDirection = true;
                Invoke(nameof(ResethasChangeDirection), 0.1f);
                break; // Exit the loop after changing direction
            }
        }
    }

    void ResethasChangeDirection() => hasChangeDirection = false;


}
