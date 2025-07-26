using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float horizontalInput;

    private void Awake()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        transform.position += move;

        float clampedX = Mathf.Clamp(transform.position.x, GameManager.Instance.leftBound.x + 2f, GameManager.Instance.rightBound.x - 2f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
