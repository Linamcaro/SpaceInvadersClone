using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 11;
    public GameObject[] enemyPrefabs;

    [SerializeField] private float xEnemySpace = 0.7f;
    [SerializeField] private float yEnemySpace = 0.5f;

    private void Start()
    {
        spawnEnemy();
    }

    private void spawnEnemy()
    {
        int prefabIndex = 0;

        /* Calculate the centerof the grid based on the number of rows and columns
           and the spacing between enemies*/
        float width = xEnemySpace * (columns - 1);
        float height = yEnemySpace * (rows - 1);
        Vector2 center = new Vector2(-width / 2, -height / 2);

        for (int i = 0; i < rows; i++)
        {
            if (i <= 1)
                prefabIndex = 0; // Use the first prefab for the first two rows
            else if (i <= 3)
                prefabIndex = 1; // Use the second prefab for the next two rows
            else
                prefabIndex = 2; // Use the third prefab for the remaining rows


            for (int j = 0; j < columns; j++)
            {
                // Calculate the position for each enemy based on its row and column
                Vector3 position = new Vector3(j * xEnemySpace, i * yEnemySpace, 0f);
                position += (Vector3)center;

                //Instantiate the enemy prefab at the spawner's position
                GameObject enemy = Instantiate(enemyPrefabs[prefabIndex], transform);
                // Set the local position of the enemy
                enemy.transform.localPosition = position;
            }
        }
    }


}
