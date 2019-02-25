using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    [SerializeField] private Transform[] laneSpawner;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private GameObject[] _pickups;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float initialSpawnTimer;
    [SerializeField] private float initialSpawnInterval;
    [SerializeField] private float _speedMod;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Text _endGameScoreText;
    [SerializeField] private GameObject _pausedPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _moneyText;
    [SerializeField] private Text _lifeText;

    private GameObject spawnedPlayer;
    private bool tapStarted;
    private float startX;
    private float endX;
    private float spawnTimer;
    private float score;
    private int obstacleCountdown = 4;
    private int currentPlayMoney = 0;
    private int currentLife = 3;
    static private GameManager _instance;

    static public GameManager Instance
    {
        get { return _instance; }
    }

    public bool InputEnabled;

    public float SpeedMod
    {
        get { return _speedMod; }
    }

    private void Awake()
    {
        _instance = this;
        spawnedPlayer = Instantiate(player);

        _scoreText.text = string.Format("{0:D9}", (int)score);
        _moneyText.text = string.Format("{0:D9}", currentPlayMoney);
        _lifeText.text = string.Format("{0:D9}", currentLife);
    }

    // Use this for initialization
    void Start()
    {
        spawnedPlayer.GetComponent<PlayerController>().Lanes = lanes;
        spawnedPlayer.GetComponent<PlayerController>().Initialize(lanes.Length / 2);

        spawnTimer = initialSpawnInterval;
        tapStarted = false;

        InputEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Tap start" + Input.mousePosition);
                startX = Input.mousePosition.x;
                tapStarted = true;
            }

            if (Input.GetMouseButtonUp(0) && tapStarted)
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

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                spawnedPlayer.GetComponent<PlayerController>().MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                spawnedPlayer.GetComponent<PlayerController>().MoveRight();
            }

            //Spawner start after 3 secs
            if (initialSpawnTimer > 0)
            {
                initialSpawnTimer -= Time.deltaTime;
            }
            else
            {
                if (spawnTimer > 0)
                {
                    spawnTimer -= Time.deltaTime;
                }
                else
                {
                    if (obstacleCountdown == 0)
                    {
                        SpawnSomething();
                        obstacleCountdown = 4;
                    }
                    else
                    {
                        SpawnSomething(false);
                        obstacleCountdown--;
                    }
                    spawnTimer = initialSpawnInterval;
                }
            }

            score += Time.deltaTime * SpeedMod;
            _scoreText.text = string.Format("{0:D9}", (int)score);
        }
        else
        {
            if (_pausedPanel.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    _pausedPanel.SetActive(false);
                    InputEnabled = true;
                }
            }
        }
    }

    public void SpawnSomething(bool includeObstacle = true)
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
                    if (includeObstacle)
                    {
                        if (obstacleCount < 2)
                        {
                            Instantiate(GetRandomObjectFromArray(_obstacles), laneSpawner[i].transform.position, Quaternion.identity);
                            obstacleCount++;
                        }
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
                    break;
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

    public void PauseGame()
    {
        InputEnabled = false;
        _pausedPanel.SetActive(true);
    }

    public void EndGame()
    {
        _endGameScoreText.text = string.Format("Your score is {0}", score);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ModifyLife(int value)
    {
        currentLife += value;
        _lifeText.text = string.Format("{0:D9}", currentLife);
    }

    public void IncreaseMoney()
    {
        currentPlayMoney += 10;
        _moneyText.text = string.Format("{0:D9}", currentPlayMoney);
    }
}
