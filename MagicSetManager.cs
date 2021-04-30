using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;

public class MagicSetManager : MonoBehaviour, IDropHandler
{
    [SerializeField] private MagicButtonManager magicButtonManager;
    [SerializeField] private Text text;
    [SerializeField] private Magic magicSc;
    [SerializeField] private SelectRangeBlockManager selectRangeBlockManagerSc;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] private GameObject magicSet1;
    [SerializeField] private GameObject magicSet2;
    [SerializeField] private GameObject magicSet3;
    [SerializeField] private GameObject magicSet4;
    [SerializeField] private Text explanationText;
    [SerializeField] private AudioClip notEnoughSound;
    List<Transform> enemyInfo = new List<Transform>();
    public Item item = null;
    private Player playerSc;
    public bool isMagicActive;

    private void Start()
    {
        SetMagicView();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMagicActive )
        {
          MagicActive(item);        
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        if (!GManager.instance.handSc.IsHavingItem()) return;
        item = GManager.instance.handSc.GetGrabbingItem();
        Debug.Log(item.MyItemname);
        if (SaveSystem.Instance.UserData.setMagic1 == null)
        {
            SaveSystem.Instance.UserData.setMagic1 = item;
            magicSet1.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic1.MyItemImage;
            magicSet1.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet1.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic1.MyItemname;
            SaveSystem.Instance.Save();
        }
        else if(SaveSystem.Instance.UserData.setMagic1 != null&& SaveSystem.Instance.UserData.setMagic2 == null)
        {
            SaveSystem.Instance.UserData.setMagic2 = item;
            magicSet2.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic2.MyItemImage;
            magicSet2.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet2.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic2.MyItemname;
            SaveSystem.Instance.Save();
        }
        else if (SaveSystem.Instance.UserData.setMagic1 != null && SaveSystem.Instance.UserData.setMagic2 != null && SaveSystem.Instance.UserData.setMagic3 == null)
        {
            SaveSystem.Instance.UserData.setMagic3 = item;
            magicSet3.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic3.MyItemImage;
            magicSet3.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet3.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic3.MyItemname;
            SaveSystem.Instance.Save();
        }
        else if (SaveSystem.Instance.UserData.setMagic1 != null && SaveSystem.Instance.UserData.setMagic2 != null&&SaveSystem.Instance.UserData.setMagic3 != null&&SaveSystem.Instance.UserData.setMagic4 == null)
        {
            SaveSystem.Instance.UserData.setMagic4 = item;
            magicSet4.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic4.MyItemImage;
            magicSet4.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet4.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic4.MyItemname;
            SaveSystem.Instance.Save();
        }
       
        GManager.instance.handSc.SetGrabbingItem(null);
        magicButtonManager.myItem = null;
    }

    public void MagicSetButton()
    {
        playerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector3 playerPos = playerSc.transform.position;
       
        if (text.text == SaveSystem.Instance.UserData.setMagic1.MyItemname)
        {
            magicSc.SelectRange(playerPos, SaveSystem.Instance.UserData.setMagic1);
            item = SaveSystem.Instance.UserData.setMagic1;
            BlockingLayerInfo();
            isMagicActive = true;
        }
        else if (text.text == SaveSystem.Instance.UserData.setMagic2.MyItemname)
        {
            magicSc.SelectRange(playerPos, SaveSystem.Instance.UserData.setMagic2);
            item = SaveSystem.Instance.UserData.setMagic2;
            BlockingLayerInfo();
            isMagicActive = true;
        }
        else if (text.text == SaveSystem.Instance.UserData.setMagic3.MyItemname)
        {
            magicSc.SelectRange(playerPos, SaveSystem.Instance.UserData.setMagic3);
            item = SaveSystem.Instance.UserData.setMagic3;
            BlockingLayerInfo();
            isMagicActive = true;
        }
        else if (text.text == SaveSystem.Instance.UserData.setMagic4.MyItemname)
        {
            magicSc.SelectRange(playerPos, SaveSystem.Instance.UserData.setMagic4);
            item = SaveSystem.Instance.UserData.setMagic4;
            BlockingLayerInfo();
            isMagicActive = true;
        }

    }
    
    public void BlockingLayerInfo()
    {
        RaycastHit2D[] raycastHit2Ds;
        List<Transform> wallInfo = new List<Transform>();
        List<GameObject> deleteBlocks = new List<GameObject>();
        raycastHit2Ds = Physics2D.BoxCastAll(playerSc.transform.position, new Vector2(item.range+1, item.range+1), 0, Vector2.zero, blockingLayer);

        foreach(var raycastHit2D in raycastHit2Ds)
        {
            if(raycastHit2D.transform.tag == "Wall")
            {
                wallInfo.Add(raycastHit2D.transform);
            }
            if(raycastHit2D.transform.tag == "Enemy")
            {
                enemyInfo.Add(raycastHit2D.transform);
            }
        }
        for(int i = 0; i < wallInfo.Count; i++)
        {
            for(int j = 0; j < magicSc.selectRangeBlocks.Count; j++)
            {
                if(wallInfo[i].transform.position == magicSc.selectRangeBlocks[j].transform.position)
                {
                    deleteBlocks.Add(magicSc.selectRangeBlocks[j]);
                }
            }
        }
        for(int k = 0; k < deleteBlocks.Count; k++)
        {
            for(int l=0;l< magicSc.selectRangeBlocks.Count; l++)
            {
                if(deleteBlocks[k].transform.position.x == playerSc.transform.position.x && deleteBlocks[k].transform.position.y >=playerSc.transform.position.y)
                {
                    if(deleteBlocks[k].transform.position.x == magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y <= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.x == playerSc.transform.position.x && deleteBlocks[k].transform.position.y <= playerSc.transform.position.y)
                {
                    if (deleteBlocks[k].transform.position.x == magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y >= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.y == playerSc.transform.position.y && deleteBlocks[k].transform.position.x >= playerSc.transform.position.x)
                {
                    if (deleteBlocks[k].transform.position.y == magicSc.selectRangeBlocks[l].transform.position.y && deleteBlocks[k].transform.position.x <= magicSc.selectRangeBlocks[l].transform.position.x)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.y == playerSc.transform.position.y && deleteBlocks[k].transform.position.x <= playerSc.transform.position.x)
                {
                    if (deleteBlocks[k].transform.position.y == magicSc.selectRangeBlocks[l].transform.position.y && deleteBlocks[k].transform.position.x >= magicSc.selectRangeBlocks[l].transform.position.x)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if(deleteBlocks[k].transform.position.x > playerSc.transform.position.x && deleteBlocks[k].transform.position.y > playerSc.transform.position.y)
                {
                    if (deleteBlocks[k].transform.position.x <= magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y <= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.x > playerSc.transform.position.x && deleteBlocks[k].transform.position.y < playerSc.transform.position.y)
                {
                    if (deleteBlocks[k].transform.position.x <= magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y >= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.x < playerSc.transform.position.x && deleteBlocks[k].transform.position.y < playerSc.transform.position.y)
                {
                    if (deleteBlocks[k].transform.position.x >= magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y >= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
                if (deleteBlocks[k].transform.position.x < playerSc.transform.position.x && deleteBlocks[k].transform.position.y > playerSc.transform.position.y)
                {
                    if (deleteBlocks[k].transform.position.x >= magicSc.selectRangeBlocks[l].transform.position.x && deleteBlocks[k].transform.position.y <= magicSc.selectRangeBlocks[l].transform.position.y)
                    {
                        Destroy(magicSc.selectRangeBlocks[l]);
                        magicSc.selectRangeBlocks.Remove(magicSc.selectRangeBlocks[l]);
                    }
                }
            }
        }
        wallInfo.Clear();
        deleteBlocks.Clear();
      
    }

    public void MagicActive(Item setMagic)
    {

        if (setMagic.magicType == Item.MagicType.RangeAttack || setMagic.magicType == Item.MagicType.DirectAttack)
        {
            RaycastHit2D hit = Physics2D.Raycast(magicSc.worldMousePosition, Vector2.zero, 0, blockingLayer);
            if (hit.transform == null)
            {
                for (int i = 0; i < magicSc.selectRangeBlocks.Count; i++)
                {
                    Destroy(magicSc.selectRangeBlocks[i]);
                }
                magicSc.selectRangeBlocks.Clear();
                isMagicActive = false;
            }
            else if (hit.transform != null)
            {
                if (SaveSystem.Instance.UserData.playerMP < setMagic.consumedMP)
                {
                    SoundManager.instance.PlaySingle(notEnoughSound);
                    for (int i = 0; i < magicSc.selectRangeBlocks.Count; i++)
                    {
                        Destroy(magicSc.selectRangeBlocks[i]);
                    }
                    magicSc.selectRangeBlocks.Clear();
                    StartCoroutine(ExplanationText());
                    isMagicActive = false;
                    return;
                }
                else
                {
                    SoundManager.instance.PlaySingle(setMagic.se);
                    Instantiate(setMagic.effectPrefab, hit.transform);
                    PlayerStatus.instance.ChangeMP(-setMagic.consumedMP);
                    if (hit.transform.GetComponent<Enemy>() != null)
                    {
                        hit.transform.GetComponent<Enemy>().PlayerAttack(AmountDamage(setMagic, hit.transform.GetComponent<Enemy>()));
                        Debug.Log(AmountDamage(setMagic, hit.transform.GetComponent<Enemy>()));
                    }
                    else if (hit.transform.GetComponent<BossManager>() != null)
                    {
                        hit.transform.GetComponent<BossManager>().PlayerAttackToBoss(AmountDamageToBoss(setMagic, hit.transform.GetComponent<BossManager>()));
                        Debug.Log(AmountDamageToBoss(setMagic, hit.transform.GetComponent<BossManager>()));
                    }

                    StartCoroutine(Clear());
                }
            }
            isMagicActive = false;
        }
        else if (setMagic.magicType == Item.MagicType.AllAttack)
        {
            if (SaveSystem.Instance.UserData.playerMP < setMagic.consumedMP)
            {
                SoundManager.instance.PlaySingle(notEnoughSound);
                for (int i = 0; i < magicSc.selectRangeBlocks.Count; i++)
                {
                    Destroy(magicSc.selectRangeBlocks[i]);
                }
                magicSc.selectRangeBlocks.Clear();
                StartCoroutine(ExplanationText());
                isMagicActive = false;
                return;
            }
            else
            {
                SoundManager.instance.PlaySingle(setMagic.se);
                magicSc.MagicAllAttack(setMagic, playerSc.transform.position);
                PlayerStatus.instance.ChangeMP(-setMagic.consumedMP);
                foreach (Transform enemy in enemyInfo)
                {
                    if (enemy.GetComponent<Enemy>() != null)
                    {
                        enemy.GetComponent<Enemy>().PlayerAttack(AmountDamage(setMagic, enemy.GetComponent<Enemy>()));
                        Debug.Log(AmountDamage(setMagic, enemy.GetComponent<Enemy>()));
                    }
                    else if (enemy.GetComponent<BossManager>() != null)
                    {
                        enemy.GetComponent<BossManager>().PlayerAttackToBoss(AmountDamageToBoss(setMagic, enemy.GetComponent<BossManager>()));
                        Debug.Log(AmountDamageToBoss(setMagic, enemy.GetComponent<BossManager>()));
                    }
                }
                isMagicActive = false;

                StartCoroutine(Clear());
            }
        }
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < magicSc.selectRangeBlocks.Count; i++)
        {
            Destroy(magicSc.selectRangeBlocks[i]);
        }
        magicSc.selectRangeBlocks.Clear();
        GManager.instance.playerTurn = false;
    }

    IEnumerator ExplanationText()
    {
        explanationText.text = "MPがたりません";
        yield return new WaitForSeconds(0.5f);
        explanationText.text = null;
        GManager.instance.playerTurn = false;
    }

    private void SetMagicView()
    {
        if (SaveSystem.Instance.UserData.setMagic1 != null)
        {
            magicSet1.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic1.MyItemImage;
            magicSet1.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet1.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic1.MyItemname;
        }
        if (SaveSystem.Instance.UserData.setMagic2 != null)
        {
            magicSet2.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic2.MyItemImage;
            magicSet2.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet2.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic2.MyItemname;
        }
        if (SaveSystem.Instance.UserData.setMagic3 != null)
        {
            magicSet3.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic3.MyItemImage;
            magicSet3.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet3.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic3.MyItemname;
        }
        if (SaveSystem.Instance.UserData.setMagic4 != null)
        {
            magicSet4.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.setMagic4.MyItemImage;
            magicSet4.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            magicSet4.GetComponentInChildren<Text>().text = SaveSystem.Instance.UserData.setMagic4.MyItemname;
        }
    }

  private int AmountDamage(Item setMagic,Enemy enemy)
    {
        string job = SaveSystem.Instance.UserData.job;
        float randomNum = Random.Range(0, 100);
        if (setMagic.itemAttribute == Item.Attribute.Fire)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ice)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Ice)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Fire)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Wind)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ground)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Ground)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Thunder)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Thunder)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Wind)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Holy)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Dark)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Dark)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Holy)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.LastResort)
        {
            if (randomNum > PlayerStatus.instance.Critical(job) * 100)
            {
                return PlayerStatus.instance.MagAtk(job);

            }
            else
            {
                Debug.Log("呪文の暴走");
                return (int)(PlayerStatus.instance.MagAtk(job) * 1.5f);
            }
        }
        return PlayerStatus.instance.MagAtk(job);
    }

    private int AmountDamageToBoss(Item setMagic, BossManager enemy)
    {
        string job = SaveSystem.Instance.UserData.job;
        float randomNum = Random.Range(0, 100);
        if (setMagic.itemAttribute == Item.Attribute.Fire)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ice)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Ice)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Fire)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Wind)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Ground)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Ground)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Thunder)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Thunder)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Wind)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Holy)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Dark)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.Dark)
        {
            if (enemy.enemyStatus.attribute == EnemyStatus.Attribute.Holy)
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth) * 1.1f);
                    }
                }

            }
            else
            {
                if (randomNum > PlayerStatus.instance.Critical(job) * 100)
                {
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f - enemy.enemyStatus.magicDef / 4) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f - enemy.enemyStatus.magicDef / 4) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f - enemy.enemyStatus.magicDef / 4) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f - enemy.enemyStatus.magicDef / 4) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
                else
                {
                    Debug.Log("呪文の暴走");
                    if (setMagic.damage == Item.Damage.Small)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.227f) + 5.5);
                        int damageWidth = Random.Range(-2, 2);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Middle)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.305f) + 32.9);
                        int damageWidth = Random.Range(-4, 4);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.Big)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.386f) + 46);
                        int damageWidth = Random.Range(-6, 6);
                        return (int)((basicDamage + damageWidth));
                    }
                    else if (setMagic.damage == Item.Damage.SpecialBig)
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                    else
                    {
                        int basicDamage = (int)((PlayerStatus.instance.MagAtk(job) * 0.468f) + 60);
                        int damageWidth = Random.Range(-8, 8);
                        return (int)((basicDamage + damageWidth));
                    }
                }
            }
        }
        else if (setMagic.itemAttribute == Item.Attribute.LastResort)
        {
            if (randomNum > PlayerStatus.instance.Critical(job) * 100)
            {
                return PlayerStatus.instance.MagAtk(job);

            }
            else
            {
                Debug.Log("呪文の暴走");
                return (int)(PlayerStatus.instance.MagAtk(job) * 1.5f);
            }
        }
        return PlayerStatus.instance.MagAtk(job);
    }
}
