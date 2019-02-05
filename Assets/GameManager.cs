using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private GameObject player;

    private GameObject spawnedPlayer;
    public bool inputEnabled;
    private bool tapStarted;
    private float startX;
    private float endX;

    private void Awake()
    {
        spawnedPlayer = Instantiate(player);
    }

    // Use this for initialization
    void Start()
    {
        spawnedPlayer.GetComponent<PlayerController>().Lanes = lanes;
        spawnedPlayer.GetComponent<PlayerController>().Initialize(lanes.Length / 2);

        tapStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Tap start" + Input.mousePosition);
                startX = Input.mousePosition.x;
                tapStarted = true;
            }

            if(Input.GetMouseButtonUp(0) && tapStarted)
            {
                //Debug.Log("Tap end" + Input.mousePosition);
                endX = Input.mousePosition.x;
                endX -= startX;
                if (endX < 0)
                    spawnedPlayer.GetComponent<PlayerController>().MoveLeft();
                else if (endX > 0)
                    spawnedPlayer.GetComponent<PlayerController>().MoveRight();
                tapStarted = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                spawnedPlayer.GetComponent<PlayerController>().MoveLeft();
            }
            
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                spawnedPlayer.GetComponent<PlayerController>().MoveRight();
            }
        }
    }
}
