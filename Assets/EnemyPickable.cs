using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPickable : PickableController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.ModifyLife(-1);
            Destroy(gameObject);
        }
        
        //if(collision.CompareTag("Bullet") || collision.CompareTag("Shield"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
