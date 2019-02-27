using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Transform[] lanes;
    private int currentLane;

    public GameObject Bullet;

    public GameObject Shield;

    private int shieldUsageLimit;
    private float initialShieldDuration = 4;
    private float shieldDuration;
    private float initialShieldCooldown = 10;
    private float shieldCooldown;
    private bool shieldActive;
    private bool shieldAvailable;

    private bool bulletAvailable;
    private float initialBulletCooldown = 5;
    private float bulletCooldown;

    public int ShieldUsageLimit
    {
        set { shieldUsageLimit = value; }
    }

    public float InitialShieldDuration
    {
        set { initialShieldDuration = value; }
    }

    public float InitialShieldCooldown
    {
        set { initialShieldCooldown = value; }
    }

    public float InitialBulletCooldown
    {
        set { initialBulletCooldown = value; }
    }

    public bool IsBulletAvailable
    {
        set { bulletAvailable = value; }
    }

    public bool IsShieldAvailable
    {
        set { shieldAvailable = value; }
    }

    public Transform[] Lanes
    {
        get { return lanes; }
        set { lanes = value; }
    }

    // Use this for initialization
    void Start()
    {
        if (shieldAvailable)
        {
            Shield = Instantiate(Shield, transform.position, Quaternion.identity);
            Shield.transform.SetParent(transform);
            Shield.transform.localPosition = Vector3.up/2f;
            shieldCooldown = initialShieldCooldown;
            shieldDuration = initialShieldDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldAvailable)
        {

            if (Input.GetKeyDown(KeyCode.X))
            {
                ActivateShield();
            }

            if (shieldActive)
            {
                if(shieldDuration > 0)
                {
                    shieldDuration -= Time.deltaTime;
                }
                else
                {
                    Shield.SetActive(false);
                    shieldDuration = initialShieldDuration;
                    shieldActive = false;
                }
            }

            if (shieldCooldown > 0)
            {
                shieldCooldown -= Time.deltaTime;
            }
        }

        if(bulletAvailable)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SpawnBullet();
            }

            if (bulletCooldown > 0)
            {
                bulletCooldown -= Time.deltaTime;
            }
        }
    }

    public void Initialize(int startingLane)
    {
        currentLane = startingLane;
        transform.position = lanes[currentLane].position;
    }

    public void MoveLeft()
    {
        if (currentLane < 1)
            return;
        currentLane -= 1;
        //transform.position = lanes[currentLane].position;
        //Debug.Log("Move left" + transform.position);
        transform.DOMove(lanes[currentLane].position, 0.3f);
    }

    public void MoveRight()
    {
        if (currentLane >= lanes.Length - 1)
            return;
        currentLane += 1;
        //transform.position = lanes[currentLane].position;
        //Debug.Log("Move right" + transform.position);
        transform.DOMove(lanes[currentLane].position, 0.3f);
    }

    public void SpawnBullet()
    {
        if(bulletAvailable)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            bulletCooldown = initialBulletCooldown;
        }
    }

    public void ActivateShield()
    {
        if(shieldAvailable && shieldCooldown <= 0)
        {
            print("activate shiled");
            Shield.SetActive(true);
            shieldActive = true;
            shieldCooldown = initialShieldCooldown;
        }
    }
}
