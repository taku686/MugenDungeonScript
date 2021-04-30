using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Enemy : MonoBehaviour
{
    public float moveTime = 0.1f;
    public bool isMoving = false;
    [SerializeField] private bool skipMove = false;
    [SerializeField] public EnemyStatus enemyStatus;
    [SerializeField] private Text damageNum;
    [SerializeField] private AudioClip attackSound;

    public LayerMask blockingLayer;
    public LayerMask itemLayer;
    private BoxCollider2D boxCollider;
    private Transform target;
    private EnemyDropItem enemyDropItem;
    [SerializeField] private GameObject children;
    private new Animation animation;
    private Effect effectSc;









    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animation = GetComponentInChildren<Animation>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GManager.instance.AddEnemy(this);
        enemyDropItem = GetComponent<EnemyDropItem>();
        effectSc = GetComponent<Effect>();

    }

    public void MoveEnemy()
    {
        if (!skipMove)
        {
            skipMove = true;
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
        else
        {
            skipMove = false;
            return;
        }

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

            if (hit.transform == null)
            {
                transform.position = end;
                children.transform.position = start;
                StartCoroutine(AnimationMove()); //1フレーム内での処理を途中でやめてまた次のフレームで続きの処理を行うことができる                                        
            }
            else if (hit.transform != null)
            {
                Player hitComponent = hit.transform.GetComponent<Player>();
                if (hitComponent != null)
                {
                    StartCoroutine(OncantMove(hitComponent));
                }
                isMoving = false;

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
    

    }

    IEnumerator OncantMove(Player hit)
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.PlaySingle(attackSound);
        effectSc.EnemyEffectGenerate(hit);
        yield return new WaitForSeconds(0.22f);       
        hit.Enemyattack(DamageAmount());
       
        animation.AttackAnim();
        yield return new WaitForSeconds(0.78f);
        isMoving = false;
    }





    public void PlayerAttack(int loss)
    {
        int evasionNum = Random.Range(0, 100);
        int justGuardNum = Random.Range(0, 100);
        if(loss < 0)
        {
            loss = 1;
        }
        StartCoroutine(DamageNum(loss));
        if (gameObject.CompareTag("Enemy"))
        {
           
            if (evasionNum > (int)(enemyStatus.evasion / 100) || justGuardNum > enemyStatus.justGuard*100)
            {
                enemyStatus.hp -= loss;
            }
            else
            {
                loss = 0;
                enemyStatus.hp -= loss;
            }

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

        GManager.instance.DestroyEnemy(this);

        gameObject.SetActive(false);

    }

    private Vector3 ItemDropPosition()
    {
        RaycastHit2D hitItem;
        RaycastHit2D[] hitSurrouding;
        RaycastHit2D[] itemSurrouding;
        List<Vector3Int> transforms = new List<Vector3Int>();
        boxCollider.enabled = false;
        hitItem = Physics2D.BoxCast(this.transform.position, new Vector2(0.8f, 0.8f), 0, Vector2.zero, itemLayer);

        hitSurrouding = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.8f, 1.8f), 0, Vector2.zero, blockingLayer);
        itemSurrouding = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.8f, 1.8f), 0, Vector2.zero, itemLayer);
        boxCollider.enabled = true;
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

    private int DamageAmount()
    {
        string job = SaveSystem.Instance.UserData.job;
        float randomNum = Random.Range(0, 100);
        if (SaveSystem.Instance.UserData.WeaponAttribute() != null)
        {
            if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ice && enemyStatus.attribute == EnemyStatus.Attribute.Fire)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Fire && enemyStatus.attribute == EnemyStatus.Attribute.Ice)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Ground && enemyStatus.attribute == EnemyStatus.Attribute.Wind)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Thunder && enemyStatus.attribute == EnemyStatus.Attribute.Ground)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Wind && enemyStatus.attribute == EnemyStatus.Attribute.Thunder)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Holy && enemyStatus.attribute == EnemyStatus.Attribute.Dark)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Dark && enemyStatus.attribute == EnemyStatus.Attribute.Holy)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return (int)(basicDamage + Random.Range(-damageWidth, damageWidth) * 1.1f);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return (int)(criticalDamage + Random.Range(-criticalWidth, criticalWidth) * 1.1f);
                }
            }
            else if (SaveSystem.Instance.UserData.WeaponAttribute().itemAttribute == Item.Attribute.Nothing || SaveSystem.Instance.UserData.WeaponAttribute() == null)
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job))
                {
                    int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                    int damageWidth = basicDamage / 16 + 1;
                    return basicDamage + Random.Range(-damageWidth, damageWidth);
                }
                else
                {
                    int criticalDamage = enemyStatus.physicsAtk;
                    int criticalWidth = criticalDamage / 16 + 1;
                    return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
                }
            }
        }
        else
        {
            if (randomNum > PlayerStatus.instance.Critical(job))
            {
                int basicDamage = (enemyStatus.physicsAtk / 2) + (PlayerStatus.instance.PhyDef(job) / 4);
                int damageWidth = basicDamage / 16 + 1;
                return basicDamage + Random.Range(-damageWidth, damageWidth);
            }
            else
            {
                int criticalDamage = enemyStatus.physicsAtk;
                int criticalWidth = criticalDamage / 16 + 1;
                return criticalDamage + Random.Range(-criticalWidth, criticalWidth);
            }
        }
       




    }

    IEnumerator DamageNum(int loss)
    {
        damageNum.text = loss.ToString();
        yield return new WaitForSeconds(0.6f);
        damageNum.text = null;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }
}
