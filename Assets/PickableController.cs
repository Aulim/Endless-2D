using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableController : MonoBehaviour
{
    protected float _speedMod = 1;

    // Use this for initialization
    protected virtual void Start()
    {
        _speedMod = GameManager.Instance.SpeedMod;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameManager.Instance.InputEnabled)
            UpdatePosition();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
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
