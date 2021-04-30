using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BossManager : MonoBehaviour
{
   
    [SerializeField] private Player playerSc;
    [SerializeField] private GameObject fireBreathObj;
    [SerializeField] private GameObject earthquakeObj;
    [SerializeField] private GameObject groundClashObj;
    [SerializeField] private GameObject slashObj;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject children;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Transform breathPos;
    [SerializeField] public EnemyStatus enemyStatus;
    [SerializeField] private GameObject Exit;
    [SerializeField] private Text damageNum;
    [SerializeField] private AudioClip normalAttackSound;
    private EnemyDropItem enemyDropItem;
    private Transform target;
    public bool isMoving = false;
  



    private void Start()
    {
        playerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDropItem = GetComponent<EnemyDropItem>();
    }

    public void MoveBoss()
    {
        //Playerへ近づいていく
        int xdir = 0;
        int ydir = 0;

        //playerとenemyのｘ軸での距離を比べている
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            ydir = target.position.y > transform.position.y ? 1 : -1;  //playerとenemyのｙ軸を比べてplayerのほうが高い位置にいる場合はydirに１が入り低いときは-1が入る
        }
        else
        {
            xdir = target.position.x > transform.position.x ? 1 : -1;
        }

        Move(xdir, ydir);



    }

    private void Move(int horizontal, int vertical)
    {
        RaycastHit2D hit;
        if (!isMoving)
        {
            isMoving = true;
            Vector3 start = transform.position;  //playerの初期位置を得ている

            Vector3 end = start + new Vector3(horizontal, vertical, 0); //入力した値を現在地に加えることで移動後の位置を得ている

            boxCollider2D.enabled = false; //boxColliderの当たり判定を切っている

            hit = Physics2D.Linecast(start, end, blockingLayer); //移動中の線の中にあるblockingLayerの情報を取得する
            boxCollider2D.enabled = true;

            if (hit.transform == null)
            {
                transform.position = end;
                children.transform.position = start;

                StartCoroutine(Walk()); //1フレーム内での処理を途中でやめてまた次のフレームで続きの処理を行うことができる                                        
            }
            else if (hit.transform != null)
            {
                Player hitComponent = hit.transform.GetComponent<Player>();
                if (hitComponent != null)
                {

                    Boss10();

                }
            }


        }
    }

    IEnumerator Walk()
    {
        while (Mathf.Abs(children.transform.localPosition.x) > 0 || Mathf.Abs(children.transform.localPosition.y) > 0) //Epsilonとはとても小さい数という認識でいい
        {
            children.transform.localPosition = Vector3.MoveTowards(children.transform.localPosition, new Vector3(0, 0, 0), 1f / moveSpeed * Time.deltaTime);

            yield return null;
        }
        isMoving = false;
     //   GManager.instance.playerTurn = true;
    }
    private void Boss10()
    {
        int priority = Random.Range(0, 100);
        int normalAttack = 100;
        int fireBreath = 50;
        int earthquake = 20;
        int groundClash = 5;
        if (normalAttack >= priority && priority > fireBreath)
        {
            StartCoroutine(NormalAttack());
        }
        else if (fireBreath >= priority && priority > earthquake)
        {
            StartCoroutine(FireBreath());
        }
        else if (earthquake >= priority && priority > groundClash)
        {
            StartCoroutine(Earthquake());
        }
        else if (groundClash >= priority && priority > 0)
        {
            StartCoroutine(GroundClash());
        }
       
    }

    private IEnumerator NormalAttack()
    {
       
        SoundManager.instance.PlaySingle(normalAttackSound);
        Instantiate(slashObj, target.transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerSc.Enemyattack(enemyStatus.physicsAtk);
        isMoving = false;
     //   GManager.instance.playerTurn = true;

    }

    private IEnumerator FireBreath()
    {
        Instantiate(fireBreathObj, breathPos.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerSc.Enemyattack(Mathf.RoundToInt(enemyStatus.physicsAtk*1.2f));
        isMoving = false;
     //   GManager.instance.playerTurn = true;
    }

    private IEnumerator Earthquake()
    {
        Instantiate(groundClashObj, target.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerSc.Enemyattack(Mathf.RoundToInt(enemyStatus.physicsAtk * 1.5f));
        isMoving = false;
    //    GManager.instance.playerTurn = true;
    }

    private IEnumerator GroundClash()
    { 
        Instantiate(earthquakeObj, target.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        playerSc.Enemyattack(Mathf.RoundToInt(enemyStatus.physicsAtk * 1.8f));
        isMoving = false;
     //   GManager.instance.playerTurn = true;
    }

    public void PlayerAttackToBoss(int loss)
    {
        if(loss < 0)
        {
            loss = 1;
        }
        StartCoroutine(DamageNum(loss));
        if (gameObject.CompareTag("Enemy"))
        {
            enemyStatus.hp -= loss;

            if (enemyStatus.hp <= 0)
            {
                PlayerStatus.instance.LevelUp(enemyStatus.experience);
                Invoke("Death", 0.5f);
                enemyDropItem.DropIfNeeded(ItemDropPosition());
            }
        }

    }

    public void Death()
    {
        Instantiate(Exit, new Vector3(-11, 22, 0), Quaternion.identity);
        this.gameObject.SetActive(false);
    }

    private Vector3 ItemDropPosition()
    {
        RaycastHit2D hitItem;
        RaycastHit2D[] hitSurrouding;
        RaycastHit2D[] itemSurrouding;
        List<Vector3Int> transforms = new List<Vector3Int>();
        boxCollider2D.enabled = false;
        hitItem = Physics2D.BoxCast(this.transform.position, new Vector2(0.8f, 0.8f), 0, Vector2.zero, itemLayer);

        hitSurrouding = Physics2D.BoxCastAll(this.transform.position, new Vector2(2.8f, 2.8f), 0, Vector2.zero, blockingLayer);
        itemSurrouding = Physics2D.BoxCastAll(this.transform.position, new Vector2(2.8f, 2.8f), 0, Vector2.zero, itemLayer);
        boxCollider2D.enabled = true;
        if (hitItem.transform != null)
        {
            if (hitItem.transform.tag == "item" || hitItem.transform.tag == "Exit")
            {
                for (int l = 0; l < 3; l++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        transforms.Add(new Vector3Int((int)this.transform.position.x - 1 + i, (int)this.transform.position.y - 1 + l, 0));
                    }
                }


                for (int j = 0; j < transforms.Count; j++)
                {
                    for (int k = 0; k < hitSurrouding.Length; k++)
                    {
                        for (int m = 0; m < itemSurrouding.Length; m++)
                        {
                            if (transforms[j] == hitSurrouding[k].transform.position)
                            {
                                transforms.Remove(transforms[j]);

                            }
                            if (transforms[j] == itemSurrouding[m].transform.position)
                            {
                                transforms.Remove(transforms[j]);
                            }

                        }

                    }
                }
                   
                
                return transforms[Random.Range(0, transforms.Count)];
            }
        }

        return this.transform.position;

    }

    IEnumerator DamageNum(int loss)
    {
        damageNum.text = loss.ToString();
        yield return new WaitForSeconds(0.6f);
        damageNum.text = null;
    }
}
