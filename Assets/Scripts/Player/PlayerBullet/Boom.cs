using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : PlayerBullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
}
