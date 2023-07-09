using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy
{
    void CreateMeteorPiece()
    {
        if (!useEffect) { return; }
        for(int i = 0; i < 25; i++)
        {
            GameObject piece = Instantiate(ObjectManager.Instance.meteorPiece);
            piece.transform.position = transform.position;
            piece.transform.rotation = transform.rotation;
            piece.GetComponent<Rigidbody2D>().AddForce(piece.transform.up * 7.5f * Random.Range(0.8f,1.2f), ForceMode2D.Impulse);
            transform.Rotate(Vector3.forward * (360 / 25));
        }
    }
    private void OnDestroy()
    {
        try
        {
            CreateMeteorPiece();
            ItemDrop();
        }
        catch { }
        try
        {
            GameManager.Instance.Enemys.Remove(gameObject);
        }
        catch { }
    }
}
