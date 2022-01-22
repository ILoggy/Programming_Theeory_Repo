using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public PlayerController player;

    //SPAWN MANAGER
    public GameObject enemy;                    //enemy game object  
    public GameObject packageGoalPrefab;

    public GameObject[] npcPrefabs;              //array of npc prefabs
    public GameObject[] packagePrefabs;          //array of package prefabs
    public GameObject[] powerUpPrefabs;          //array of powerup prefabs    

    [HideInInspector] public GameObject[] enemys;             //array of all enemys on scene
    [HideInInspector] public GameObject[] npcs;               //array of all npcs on scene
    [HideInInspector] public GameObject[] packages;           //array of all packages on scene
    [HideInInspector] public GameObject[] powerUps;           //array of all powerUps on scene
    [HideInInspector] public GameObject[] npcGoals;           //array of npc goals as game object
    [HideInInspector] public GameObject[] packageGoals;       //package goals as game object;

    private float bounds = 230.0f;              //bounds for spawn zone
    private float delay = 2.0f;                 //delay for first spawn(enemy, powerup, npc, package)

    private float repeatEnemyTime = 20.0f;              //spawn rate (in seconds) for enemy
    private float repeatPowerUpTime = 15.0f;             //spawn rate (in seconds) for powerup
    private float repeatNPCTime = 0.2f;                 //spawn rate (in seconds) for npc
    private float repeatPackageTime = 3.0f;                //spawn rate (in seconds) for package
    private float repeatPackageGoalTime = 3.0f;                //spawn rate (in seconds) for package goal

    [Range(90.0f, 240.0f)]
    [HideInInspector] public float range;
    [Range(-60.0f, 60.0f)]
    [HideInInspector] public float rangeG5;

    //LIMIT
    [HideInInspector] public bool enemyLimitReach;                //to check is an amount of enemy is above limit
    [HideInInspector] public bool npcLimitReach;                  //to check is an amount of npc is above limit
    [HideInInspector] public bool powerUpLimitReach;              //to check is an amount of powerup is above limit
    [HideInInspector] public bool packageLimitReach;              //to check is an amount of package is above limit
    [HideInInspector] public bool packageGoalLimitReach;          //to check is an amount of package goals is above limit

    public int enemyLeftCounter;
    public int npcLeftCounter;
    public int packageLeftCounter;

    [HideInInspector]public bool gateOpen = true;

    //UI LEFT CORNER
    public GameObject LUCornerPanel;

    public float points;
    private float timer;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemyLeftText;
    public TextMeshProUGUI timeInGame;
    private float timeInGameCounter;

    private bool isGameActive = false;

    private bool firstAwake = true;

    //UI MENU
    //PAUSE MENU
    public Button.ButtonClickedEvent onClick;
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button exitPauseButton;
    public Button backButton;
    public Button keyboardButton;
    public Button gamepadButton;

    //CONTROL UI
    public GameObject startDisplay;
    public TextMeshProUGUI toPlayText;
    public GameObject keyboardPanel;
    public GameObject gamepadPanel;
    public GameObject controleMenu;
    public GameObject toPlayKeyboardPanel;
    public GameObject toPlayJoystickPanel;
    public bool canStart;
    public GameObject needToDoPanel;
    public bool controlChosed;


    // Start is called before the first frame update
    void Start()
    {
        npcGoals = GameObject.FindGameObjectsWithTag("NPC Goal");       //declare npcGoals as objects in Hierarchy with tag "NPC Goal"
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Awake()
    {
        if (isGameActive && firstAwake && controlChosed)
        {
            

            //InvokeRepeating("EnemyRandomSpawn", delay, repeatEnemyTime);         //invoke "method_name" with rate "repeatTime", and delay for the first activate
            //InvokeRepeating("PowerUpRandomSpawn", delay, repeatPowerUpTime);        //invoke "method_name" with rate "repeatTime", and delay for the first activate
            //InvokeRepeating("NPCRandomSpawn", delay, repeatNPCTime);                //invoke "method_name" with rate "repeatTime", and delay for the first activate
            //InvokeRepeating("PackageRandomSpawn", delay, repeatPackageTime);        //invoke "method_name" with rate "repeatTime", and delay for the first activate
            //InvokeRepeating("PackageGoalRandomSpawn", delay, repeatPackageGoalTime);
            //InvokeRepeating("Score", 0.5f, 0.5f);
            //InvokeRepeating("EnemyLeftCounter", 0.5f, 0.5f);
            //InvokeRepeating("TimeInGame", 0.5f, 1);
            //firstAwake = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && firstAwake)
        {
            Awake();
        }
        enemys = GameObject.FindGameObjectsWithTag("Enemy");   //declare to enemys all objects with "Enemy" tag
        npcs = GameObject.FindGameObjectsWithTag("NPC");       //declare to npcs all objects with "NPC" tag
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");   //declare to powerUps all objects with "PowerUp" tag
        packages = GameObject.FindGameObjectsWithTag("Package");   //declare to packages all objects with "Package" tag
        packageGoals = GameObject.FindGameObjectsWithTag("Package Goal");

        if (player.playKeyboard)
        {
            isGameActive = true;
        }
        else
        {
            isGameActive = true;
        }
        

        if (isGameActive == true)
        {
            points += 2 * Time.deltaTime;

            if (enemys.Length >= 6)              //check is amount of enemys above the limit
            {
                enemyLimitReach = true;
            }
            else if (enemys.Length < 6)
            {
                enemyLimitReach = false;
            }

            if (npcs.Length >= 100)             //check is amount of npcs above the limit
            {
                npcLimitReach = true;
            }
            else if (npcs.Length < 100)
            {
                npcLimitReach = false;
            }

            if (powerUps.Length >= 10)           //check is amount of powerUps above the limit
            {
                powerUpLimitReach = true;
            }
            else if (powerUps.Length < 10)
            {
                powerUpLimitReach = false;
            }

            if (packages.Length >= 3)        //check is amount of packages above the limit
            {
                packageLimitReach = true;
            }
            else if (packages.Length < 3)
            {
                packageLimitReach = false;
            }

            if (packageGoals.Length >= 3)        //check is amount of package goals above the limit
            {
                packageGoalLimitReach = true;
            }
            else if (packageGoals.Length < 3)
            {
                packageGoalLimitReach = false;
            }
        }

        //PAUSE MENU
        resumeButton.onClick.AddListener(OnResumeBottonClick);
        exitPauseButton.onClick.AddListener(OnPauseMenuExit);
        backButton.onClick.AddListener(BackToMainMenu);

        if (player.playKeyboard)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnGamePause();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                OnGamePause();
            }
        }
        

        //START MENU
        keyboardButton.onClick.AddListener(SetToKeyboard);
        gamepadButton.onClick.AddListener(SetToJoystick);
        if (player.playKeyboard && canStart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                controleMenu.gameObject.SetActive(false);
                keyboardPanel.gameObject.SetActive(false);
                toPlayKeyboardPanel.gameObject.SetActive(false);
                StartCoroutine("StartGameTimer");
            }
        }
        else if (player.playKeyboard == false && canStart)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                controleMenu.gameObject.SetActive(false);
                keyboardPanel.gameObject.SetActive(false);
                toPlayJoystickPanel.gameObject.SetActive(false);
                StartCoroutine("StartGameTimer");
            }
        }
        
    }

    IEnumerator StartGameTimer()
    {
        Time.timeScale = 1.0f;
        needToDoPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        needToDoPanel.gameObject.SetActive(false);
        LUCornerPanel.gameObject.SetActive(true);
        isGameActive = true;
        controlChosed = true;
    }

    IEnumerator Counter()
    {
        gateOpen = false;
        npcLeftCounter = GameObject.FindGameObjectsWithTag("NPC").Length;
        packageLeftCounter = GameObject.FindGameObjectsWithTag("Package").Length;
        Debug.Log(" Enemy left: " + enemyLeftCounter + " NPC left: " + npcLeftCounter + " Packages left: " + packageLeftCounter);
        yield return new WaitForSeconds(1);
        gateOpen = true;
    }

    private void Score()
    {
        scoreText.text = "Score : " + Mathf.Round(points);
    }

    private void EnemyLeftCounter()
    {
        enemyLeftCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyLeftText.text = "Enemy left : " + enemyLeftCounter;
    }
    //PAUSE MENU
    private void OnResumeBottonClick()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnGamePause()
    { 
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void OnPauseMenuExit()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void SetToKeyboard()
    {
        Time.timeScale = 0.0f;
        startDisplay.gameObject.SetActive(false);
        controleMenu.gameObject.SetActive(true);
        keyboardPanel.gameObject.SetActive(true);
        toPlayKeyboardPanel.gameObject.SetActive(true);
        LUCornerPanel.gameObject.SetActive(false);
        player.playKeyboard = true;
        canStart = true;

    }

    private void SetToJoystick()
    {
        Time.timeScale = 0.0f;
        startDisplay.gameObject.SetActive(false);
        controleMenu.gameObject.SetActive(true);
        gamepadPanel.gameObject.SetActive(true);
        toPlayJoystickPanel.gameObject.SetActive(true);
        LUCornerPanel.gameObject.SetActive(false);
        player.playKeyboard = false;
        canStart = true;
    }

    //SPAWN MANAGER
    private Vector3 RandomSpawnPosInBounds()          //method in return gives random position in bounds
    {
        float randomx = Random.Range(-bounds, bounds);
        float randomz = Random.Range(-bounds, bounds);
        float yPos = 0.5f;
        
        Vector3 g1 = new Vector3(-range, yPos, -range);
        Vector3 g2 = new Vector3(-range, yPos, range);
        Vector3 g3 = new Vector3(range, yPos, -range);
        Vector3 g4 = new Vector3(range, yPos, range);
        Vector3 g5 = new Vector3(rangeG5, yPos, rangeG5);

        Vector3 pos = new Vector3(randomx, yPos, randomz);
        do
        {
            pos = new Vector3(randomx, yPos, randomz); ;
        } while (pos == g1||
                 pos == g2||
                 pos == g3||
                 pos == g4||
                 pos == g5);
        
        return new Vector3(randomx, yPos, randomz);
    }

    private void EnemyRandomSpawn()         //method which spawn enemy in bounds with rotation
    {
        if (enemyLimitReach == false)       //chech is enemy above limit
        {
            Instantiate(enemy, RandomSpawnPosInBounds(), enemy.transform.rotation);
        }
    }

    private void PowerUpRandomSpawn()       //method which spawn powerup in bounds with rotation
    {
        if (powerUpLimitReach == false)     //chech is powerup above limit
        {
            int index = Random.Range(0, powerUpPrefabs.Length);
            Vector3 spawnPos = RandomSpawnPosInBounds();
            spawnPos.y = 3.0f;
            Instantiate(powerUpPrefabs[index], spawnPos, powerUpPrefabs[index].transform.rotation);
        }
    }

    private void NPCRandomSpawn()       //method which spawn npc in bounds with rotation
    {
        if (npcLimitReach == false)     //chech is npc above limit
        {
            int index = Random.Range(0, npcPrefabs.Length);
            Instantiate(npcPrefabs[index], RandomSpawnPosInBounds(), npcPrefabs[index].transform.rotation);
        }
    }

    private void PackageRandomSpawn()       //method which spawn package in bounds with rotation
    {
        if (packageLimitReach == false)     //chech is package above limit
        {
            int index = Random.Range(0, packagePrefabs.Length);
            Instantiate(packagePrefabs[index], RandomSpawnPosInBounds(), packagePrefabs[index].transform.rotation);
        }
    }

    private void PackageGoalRandomSpawn()
    {
        if (packageGoalLimitReach == false)
        {
            Vector3 spawnPos = RandomSpawnPosInBounds();
            spawnPos.y = 3.0f;
            Instantiate(packageGoalPrefab, spawnPos, packageGoalPrefab.transform.rotation);
        }
    }

    //POWERUP USE
    public void UseGem()
    {
        Debug.Log("Gem_PowerUp used");

        foreach (GameObject p in packages)
        {
            Destroy(p);
            points += 100;
        }

        foreach (GameObject pg in packageGoals)
        {
            Destroy(pg);
        }
    }

    public void UseRadioactive()
    {
        Debug.Log("Radioactive_PowerUp used");
        points += 10;

        foreach (GameObject e in enemys)
        {
            Destroy(e);
        }

        foreach (GameObject n in npcs)
        {
            Destroy(n);
        }
    }

    public void UsePower()
    {
        points += 10;
        Debug.Log("Power_PowerUp used");
        StartCoroutine("PowerTimer");
    }

    public void UseGoldCuplcon()
    {
        points += 1500;
    }

    IEnumerator PowerTimer()
    {
        player.velocity *= 2;
        yield return new WaitForSeconds(3);
        player.velocity /= 2;
    }

    private void TimeInGame()
    {
        timeInGameCounter += 1.0f;
        timeInGame.text = "Time In Game: " + timeInGameCounter + " second";
    }
}
