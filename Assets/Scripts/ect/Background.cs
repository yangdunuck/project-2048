using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] sprites;
    public Transform target;
    public float Range;
    public float speed;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[GameManager.Instance.stage];
    }
    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if(transform.position.y < -Range)
        {
            transform.position = target.transform.position + Vector3.up * Range;
        }
    }
}
