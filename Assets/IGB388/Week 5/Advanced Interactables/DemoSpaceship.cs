using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpaceship : MonoBehaviour
{
    public GameObject bomb;
    private float speed = 4.0f;

    public void MoveSpaceship(Vector2 move)
    {
        Vector3 change = Time.deltaTime * new Vector3(move.x, 0, move.y) * speed;
        transform.position += change;
        transform.LookAt(transform.position + change);
    }

    public void DropBomb()
    {
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
