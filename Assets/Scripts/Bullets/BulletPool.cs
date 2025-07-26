using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        // Si se agotan, crear uno nuevo (opcional)
        GameObject bullet = Instantiate(bulletPrefab);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}

