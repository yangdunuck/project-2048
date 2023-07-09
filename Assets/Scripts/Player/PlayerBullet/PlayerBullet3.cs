using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet3 : PlayerBullet
{
    Transform target;
    public float speed;
    public float rotateSpeed;
    void Start()
    {
        if(GameManager.Instance.Enemys.Count > 0 && GameManager.Instance.Enemys[0] != null)
        {
            target = GameManager.Instance.Enemys[0].transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            transform.rotation = rotation;
        }
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
}
