using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Boss") 
        {
            if(collision.gameObject.name != "Boom")
            {
                if(collision.gameObject.name != "BulletDestory")
                {
                    if (collision.CompareTag("Enemy")) { collision.GetComponent<Enemy>().useEffect = false; }
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
