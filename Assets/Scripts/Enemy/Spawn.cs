using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : Singleton<Spawn>
{
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private TextMeshProUGUI enemyNumberText;
    [SerializeField] private Button startButton;
    [SerializeField] private int numberOfSpawn;
    [SerializeField] private float waitTime;
    [SerializeField] private float coolDown;

    private int wave;
    private int enemiesNumber;
    public int GetWave()
    {
        return wave;
    }

    private bool waveEnd;
    private float countdown;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        enemiesNumber = 0;
        countdown = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartWave();
        }

        countdown -= Time.deltaTime;

        cooldownText.text = $"Next Wave in {(int)countdown}";

        if (waveEnd) 
        { 
            CheckEnemy(); 
        }

        enemyNumberText.text = $"Enemies : {enemiesNumber} / {numberOfSpawn}";
    }

    IEnumerator SpawnWave()
    {
        wave++;

        waveText.text = $"Wave : {wave}";

        for (int i = 0; i < numberOfSpawn; i++)
        {
            SpawnEnemy();
            enemiesNumber++;

            yield return new WaitForSeconds(waitTime);
        }

        waveEnd = true;
    }

    public void StartWave()
    {
        waveEnd = false;
        StartCoroutine(SpawnWave());
        countdown = coolDown;
        startButton.gameObject.SetActive(false);
    }

    private void SpawnEnemy()
    {
        enemy = Instantiate(spawns[Random.Range(0, spawns.Length)], spawnPos.position, Quaternion.Euler(new Vector3(0,90,0)));
    }

    private void CheckEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesLeft = enemies.Length;

        if (enemiesLeft == 0)
        {
            startButton.gameObject.SetActive(true);

            enemiesNumber = 0;
        }
    }
}
