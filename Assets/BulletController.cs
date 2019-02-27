using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletSpeed = 10f;
    private int _piercingStrength = 1;

    public int PiercingStrength
    {
        set { _piercingStrength = value; }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }

        if(collision.CompareTag("Enemy"))
        {
            _piercingStrength--;
            if (_piercingStrength <= 0)
                Destroy(gameObject);
        }
    }

    void UpdatePosition()
    {
        float x = transform.position.x;
        float y = transform.position.y + bulletSpeed  * Time.deltaTime;
        float z = transform.position.z;
        transform.position = new Vector3(x, y, z);
    }
}
