using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private Transform[] laneSpawner;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private GameObject[] _pickups;
    [SerializeField] private GameObject[] _enemies;


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

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SpawnSomething();
            }
        }
    }

    public void SpawnSomething()
    {
        int obstacleCount = 0;
        int pickupCount = 0;
        for (int i = 0; i < laneSpawner.Length; i++)
        {
            //spawn for each lane
            int randomSpawn = Random.Range(0, 3);
            switch (randomSpawn)
            {
                case 0:
                    if (obstacleCount < 2)
                    {
                        Instantiate(GetRandomObjectFromArray(_obstacles), laneSpawner[i].transform.position, Quaternion.identity);
                        obstacleCount++;
                    }
                    break;
                case 1:
                    if(pickupCount < 1)
                    {
                        Instantiate(GetRandomObjectFromArray(_pickups), laneSpawner[i].transform.position, Quaternion.identity);
                        pickupCount++;
                    }
                    break;
                case 2:
                    Instantiate(GetRandomObjectFromArray(_enemies), laneSpawner[i].transform.position, Quaternion.identity);
                    break;
                case 3:
                default:
                    break;
            }
        }
    }

    private GameObject GetRandomObjectFromArray(GameObject[] arr)
    {
        if (arr.Length < 2)
            return arr[0];

        int idx = Random.Range(0, arr.Length);
        return arr[idx];
    }
}
