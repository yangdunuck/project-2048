using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;

    public GameObject playerBullet1;
    public GameObject playerBullet2;
    public GameObject playerBullet3;
    public GameObject scoreUp;
    public GameObject fuelUp;
    public GameObject hpUp;
    public GameObject invincibility;
    public GameObject powerUp;
    public GameObject enemybullet1;
    public GameObject enemybullet2;
    public GameObject meteorPiece;
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    public GameObject meteor;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static ObjectManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
