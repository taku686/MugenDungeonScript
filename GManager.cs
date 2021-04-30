using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    BoardManager boardManager;

    public bool playerTurn = true;
    public bool enemiesMoving = false;

    public int stage = 0;
    public bool doingSetup;
    private Text stageText;
    public GameObject stageImage;


    public int playerHP ;
    public int maxPlayerHp;
    public Slider hpSlider;
    public Text playerHPText;
    public int playerMP;
    public int maxPlayerMp;
    public Slider mpSlider;
    public Text playerMPText;

    private List<Enemy> enemies;
    private BossManager bossManagerSc;
    [SerializeField] private MagicButtonManager magicButtonManager;
    public Hand handSc;
    public bool isGameOver;
   
  


  

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


       DontDestroyOnLoad(gameObject); //シーンの切り替え時ゲームオブジェクトが壊されなくなる

        enemies = new List<Enemy>();

        boardManager = GetComponent<BoardManager>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void Call()
    {   
        SceneManager.sceneLoaded += OnSceneLoaded;  //sceneLoadedではシーンが呼び込まれるたびに呼べる関数を設定できる
    }

    static private void OnSceneLoaded(Scene next, LoadSceneMode a)
    {
        instance.InitGame();
    }

   
    
    private void Start()
    {
        Debug.Log("gamemanagerStart");

        instance.InitGame();
    }

    
    public void InitGame()
    {
        doingSetup = true;
        if (isGameOver == false)
        {
            instance.stage = SaveSystem.Instance.UserData.currentStage;
            instance.stage++;
            SaveSystem.Instance.UserData.currentStage = instance.stage;
            SaveSystem.Instance.Save();
            if (SaveSystem.Instance.UserData.stage < instance.stage)
            {
                SaveSystem.Instance.UserData.stage++;
                SaveSystem.Instance.Save();
            }
        }
        GameObject.Find("Canvas").GetComponent<Menu>().StageImageSwitch();
        playerHPText = GameObject.Find("HpText").GetComponent<Text>();
        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        playerHP = SaveSystem.Instance.UserData.playerHP;
        maxPlayerHp = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        hpSlider.value = playerHP;
        hpSlider.maxValue = maxPlayerHp;
        playerHPText.text = playerHP + "/" + maxPlayerHp;
        playerMPText = GameObject.Find("MpText").GetComponent<Text>();
        mpSlider = GameObject.Find("MpSlider").GetComponent<Slider>();
        playerMP = SaveSystem.Instance.UserData.playerMP;
        maxPlayerMp = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        mpSlider.value = playerMP;
        mpSlider.maxValue = maxPlayerMp;
        playerMPText.text = playerMP + "/" + maxPlayerMp;

        enemies.Clear();        
        magicButtonManager.FindGameObject();
        handSc = GameObject.Find("Hand").GetComponent<Hand>();
        boardManager.SetupScene();
        if (SaveSystem.Instance.UserData.currentStage % 10 == 0 && isGameOver == false)
        {
            bossManagerSc = GameObject.Find("Boss10(Clone)").GetComponent<BossManager>();
        }
        isGameOver = false;
    }



    void Update()
    {

        if (doingSetup || playerTurn || enemiesMoving)
        {
    
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemy(Enemy script)
    {
        enemies.Add(script);
    }

    public void DestroyEnemy(Enemy script)
    {
        enemies.Remove(script);
    }

    IEnumerator MoveEnemies()
    {
      
        enemiesMoving = true;


        yield return null;

        if(enemies.Count == 0)
        {
            yield return null;
            playerTurn = true;
        }

        for (int i= 0;i< enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return null;
          
        }
       
        if (bossManagerSc != null)
        {
            bossManagerSc.MoveBoss();
            
        }
        yield return new WaitForSeconds(0.1f);
        playerTurn = true;
        enemiesMoving = false;


    }

    public void Gameover()
    {
        isGameOver = true;
        stageImage = GameObject.Find("Canvas").GetComponent<Menu>().StageImage;
        stageText = stageImage.GetComponentInChildren<Text>();
        stageImage.SetActive(true);
        stageText.text = "GameOver";
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        StartCoroutine(ToTitle());
    }

    IEnumerator ToTitle()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}

