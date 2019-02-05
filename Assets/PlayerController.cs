using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Transform[] lanes;
    private int currentLane;

    public Transform[] Lanes
    {
        get { return lanes; }
        set { lanes = value; }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
