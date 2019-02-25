using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    [SerializeField] private float _speedMod = 1;

    // Use this for initialization
    void Start()
    {
        _speedMod = GameManager.Instance.SpeedMod;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.InputEnabled)
            UpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("Called onTrigger");
        if(collision.CompareTag("Finish"))
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Called onCollision");
        if (collision.gameObject.CompareTag("Finish"))
            Destroy(gameObject);
    }

    private void UpdatePosition()
    {
        float x = transform.position.x;
        float y = transform.position.y - _speedMod * Time.deltaTime;
        float z = transform.position.z;
        transform.position = new Vector3(x, y,z);
    }
}
