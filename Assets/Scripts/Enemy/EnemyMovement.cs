using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float direction = 1f;
    private float speed = 0.3f;
    private float downDistance = 0.2f;
    private bool hasChangeDirection;

    private Vector3 parentPosition;

    private void Start()
    {
        GameManager.Instance.OnLevelIncreased += OnLevelIncreased;
        hasChangeDirection = false;
        parentPosition = transform.position;
    }

    private void OnLevelIncreased(object sender, EventArgs e)
    {
        transform.position = parentPosition; // Reset position to parent
        speed += 0.2f; // Increase speed with each level
        foreach (Transform enemy in transform)
        {
            enemy.gameObject.SetActive(true); // Reactivate enemies
        }
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
            return;

        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (hasChangeDirection) return;

        foreach (Transform enemy in transform)
        {
            if (enemy.position.x <= (GameManager.Instance.leftBound.x + 2f) || enemy.position.x >= (GameManager.Instance.rightBound.x - 2f))
            {
                direction *= -1f;
                transform.position += Vector3.down * downDistance;
                hasChangeDirection = true;
                Invoke(nameof(ResethasChangeDirection), 0.1f);
                break; // Exit the loop after changing direction
            }
        }
    }

    void ResethasChangeDirection() => hasChangeDirection = false;


}
