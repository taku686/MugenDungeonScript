using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveTime = 0.1f;
    public bool isMoving = false;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private GameObject children;
    [SerializeField] private Text damageNum;
    [SerializeField] private AudioClip movesound1;
    [SerializeField] private AudioClip movesound2;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip stairssound;
    private Effect effectSc;



    void Start()
    {
       
        boxCollider = GetComponent<BoxCollider2D>();
        effectSc = GetComponent<Effect>();
        children = GameObject.Find("PlayerAnimation");
        animator = children.GetComponent<Animator>();
    }

    void Update()
    {

        if (!GManager.instance.playerTurn)
        {
            return;
        }
       
        int x = 0;
        int y = 0;
       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1;
            ChangeAnimation(x, y);
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1;
            ChangeAnimation(x, y);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y = 1;
            ChangeAnimation(x, y);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -1;
            ChangeAnimation(x, y);
        }

        if (x == 1 || x == -1 || y == 1 || y == -1)
        {
            Move(x, y);
        }
       
    }

    private void ChangeAnimation(float x,float y)
    {
        animator.SetFloat("x", x);
        animator.SetFloat("y", y);
    }


  

    public void Move(int horizontal, int vertical)
    {
        RaycastHit2D hit;
        if (!isMoving)
        {
            isMoving = true;
            Vector3 start = transform.position;  //playerの初期位置を得ている

            Vector3 end = start + new Vector3(horizontal, vertical, 0); //入力した値を現在地に加えることで移動後の位置を得ている
           
            boxCollider.enabled = false; //boxColliderの当たり判定を切っている

            hit = Physics2D.Linecast(start, end, blockingLayer); //移動中の線の中にあるblockingLayerの情報を取得する
            boxCollider.enabled = true;
          
            if (hit.transform == null)//移動していないとき かつ　移動中の経路にhitで取得した位置情報が何もないとき
            {
                transform.position = end;
                children.transform.position = start;
                SoundManager.instance.RandomSE(movesound1, movesound2);
                StartCoroutine(AnimationMove()); //1フレーム内での処理を途中でやめてまた次のフレームで続きの処理を行うことができる
                                                 //    GManager.instance.playerTurn = false;

            }
            else if (hit.transform != null)
            {
                Enemy hitComponent = hit.transform.GetComponent<Enemy>();
                BossManager bossComponent = hit.transform.GetComponent<BossManager>();
                if(hitComponent != null || bossComponent != null)
                {

                    StartCoroutine(OncantMove(hitComponent,bossComponent));
                   
                }
                else if (hit.transform.CompareTag("Wall"))
                {
                    isMoving = false;
                    GManager.instance.playerTurn = false;
                    return;
                }
                isMoving = false;
                GManager.instance.playerTurn = false;
                return;

            }
           

        }
       
           
        
    }

    IEnumerator AnimationMove()
    {
        while (Mathf.Abs(children.transform.localPosition.x) > 0 || Mathf.Abs(children.transform.localPosition.y) > 0) //Epsilonとはとても小さい数という認識でいい
        {
            children.transform.localPosition = Vector3.MoveTowards(children.transform.localPosition, new Vector3(0, 0, 0), 1f / moveTime * Time.deltaTime);

            yield return null;
        }
        isMoving = false;
        GManager.instance.playerTurn = false;
      
    }

    IEnumerator OncantMove(Enemy hit, BossManager boss)
    {
        if (hit != null)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.PlaySingle(attackSound);
            effectSc.EffectGenerate(hit);
            yield return new WaitForSeconds(0.22f);
            hit.PlayerAttack(DamageAmount(hit));      
        }
        if (boss != null)
        {
            yield return new WaitForSeconds(0.1f);
            SoundManager.instance.PlaySingle(attackSound);
            effectSc.EffectGenerateToBoss(boss);
            yield return new WaitForSeconds(0.22f);
            boss.PlayerAttackToBoss(DamageAmountToBoss(boss));
        }
        yield return new WaitForSeconds(0.78f);
        isMoving = false;
        GManager.instance.playerTurn = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "item")
        {

            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Exit")
        {
            SoundManager.instance.PlaySingle(stairssound);
            Invoke("Restart", 1f);
           

            enabled = false;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);

    }

 

    public void Enemyattack(int loss)
    {
        int evasionNum = Random.Range(0, 100);
        int justGuardNum = Random.Range(0, 100);
        string job = SaveSystem.Instance.UserData.job;
        if(loss > 0)
        {
            loss = -1;
        }
        if(evasionNum < (int)(PlayerStatus.instance.Evasion(job)/100))
        {
            loss = 0;
            StartCoroutine(Evasion());
            PlayerStatus.instance.ChangeHP(loss);          
        }
        else if (justGuardNum < (int)PlayerStatus.instance.JustGuard(job))
        {
            loss = 0;
            StartCoroutine(JustGuard());
            PlayerStatus.instance.ChangeHP(loss);
        }
        else
        {
            StartCoroutine(DamageNum(loss));
            PlayerStatus.instance.ChangeHP(loss);
        }
      
    }

    IEnumerator DamageNum(int loss)
    {
        damageNum.text = loss.ToString();
        yield return new WaitForSeconds(0.6f);
        damageNum.text = null;
    }
    IEnumerator Evasion()
    {
        damageNum.text = "evasion";
        yield return new WaitForSeconds(0.6f);
        damageNum.text = null;
    }
    IEnumerator JustGuard()
    {
        damageNum.text = "justguard";
        yield return new WaitForSeconds(0.6f);
        damageNum.text = null;
    }
    private int DamageAmount(Enemy enemy)
    {
        string job = SaveSystem.Instance.UserData.job;
        float randomNum = Random.Range(0, 100);
        if (SaveSystem.Instance.UserData.WeaponAttribute() != null)
        {
            if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Fire && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ice)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                  //  Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ice && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Fire)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Wind && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ground)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                  //  Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                  //  Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ground && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Thunder)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                  //  Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Thunder && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Wind)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                  //  Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Holy && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Dark)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Dark && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Holy)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Nothing || SaveSystem.Instance.UserData.WeaponAttribute() == null)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                 //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                 //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
        }
        else
        {
            if (randomNum > PlayerStatus.instance.Critical(job) * 100)
            {
             //   Debug.Log("通常攻撃");
                int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                int damageWidth = basicDamage / 16 + 1;
                return basicDamage + Random.Range(-damageWidth, damageWidth);
            }
            else
            {
             //   Debug.Log("会心の一撃");
                int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                int criticalWidth = criticalDamage / 16 + 1;
                return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
            }
        }
    }

    private int DamageAmountToBoss(BossManager enemy)
    {
        string job = SaveSystem.Instance.UserData.job;
        float randomNum = Random.Range(0, 100);
        if (SaveSystem.Instance.UserData.WeaponAttribute() != null)
        {
            if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Fire && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ice)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //    Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //    Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ice && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Fire)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Wind && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ground)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //  Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ground && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Thunder)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Thunder && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Wind)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Holy && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Dark)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //    Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //  Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Dark && enemy.enemyStatus.attribute == EnemyStatus.Attribute.Holy)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    //    Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Nothing || SaveSystem.Instance.UserData.WeaponAttribute() == null)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //  Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                    //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    //   Debug.Log("通常攻撃");
                    int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                    //   Debug.Log("会心の一撃");
                    int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
        }
        else
        {
            if (randomNum > PlayerStatus.instance.Critical(job) * 100)
            {
                //   Debug.Log("通常攻撃");
                int basicDamage = (PlayerStatus.instance.PhyAtk(job) / 2 - enemy.enemyStatus.physicsDef / 4);
                int damageWidth = basicDamage / 16 + 1;
                return basicDamage + Random.Range(-damageWidth, damageWidth);
            }
            else
            {
                //   Debug.Log("会心の一撃");
                int criticalDamage = PlayerStatus.instance.PhyAtk(job);
                int criticalWidth = criticalDamage / 16 + 1;
                return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
            }
        }
    }



}

