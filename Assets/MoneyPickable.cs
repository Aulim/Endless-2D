using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickable : PickableController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseMoney();
            Destroy(gameObject);
        }
    }
}
