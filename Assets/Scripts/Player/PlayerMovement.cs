using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    [SerializeField] private BulletPool bulletPool;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        transform.position += move;

        /*float clampedX = Mathf.Clamp(transform.position.x, GameManager.Instance.leftBound.x + 2f, GameManager.Instance.rightBound.x - 2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
}
