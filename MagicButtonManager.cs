using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MagicButtonManager : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public GameObject draggingObj;
    private Transform canvasTransform;
    public Item myItem;
    private List<Item> myItems = new List<Item>();
    private RaycastHit2D hit;
    [SerializeField] private LayerMask magicLayer;
    [SerializeField] Text text;
    private GameObject MagicSetManager1;
    private GameObject MagicSetManager2;
    private GameObject MagicSetManager3;
    private GameObject MagicSetManager4;


    private void Start()
    {
        FindGameObject();
    }

    public void FindGameObject()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        MagicSetManager1 = GameObject.Find("MagicSet1");
        MagicSetManager2 = GameObject.Find("MagicSet2");
        MagicSetManager3 = GameObject.Find("MagicSet3");
        MagicSetManager4 = GameObject.Find("MagicSet4");
    }


    public void ShowDetail()
    {
        GameObject detailImage = GameObject.Find("MagicImage");
        GameObject detailStaus = GameObject.Find("MagicStatus");
        Text[] statusTexts = detailStaus.GetComponentsInChildren<Text>();

        if (text != null)
        {
            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.MyItemname == text.text))
            {

                detailImage.GetComponent<Image>().sprite = allitem.MyItemImage;
                detailImage.GetComponent<Image>().color = Color.white;
                statusTexts[0].text = allitem.MyItemname;
                statusTexts[1].text = "消費ＭＰ : " + allitem.consumedMP;
                statusTexts[2].text = "威力 : " + allitem.damage;
                statusTexts[3].text = "範囲 : " + allitem.range;
                statusTexts[4].text = "属性 : " + allitem.itemAttribute;
                statusTexts[5].text = "タイプ : " + allitem.magicType;
                statusTexts[6].text = "詳細 : " + allitem.explanation;

            }    
        }
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(myItems != null)
        {
            myItems.Clear();
        }

        hit = Physics2D.Raycast(Input.mousePosition, new Vector3(0, 0, -1), 100, magicLayer);
        if (hit.transform != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.allItems.Count; i++)
            {
                if (hit.transform.GetComponentInChildren<Text>().text == SaveSystem.Instance.UserData.allItems[i].MyItemname)
                {
                    myItems.Add(SaveSystem.Instance.UserData.allItems[i]);
                }
            }
            myItem = myItems[0];
            Debug.Log(myItem.MyItemname);
            draggingObj = Instantiate(hit.transform.Find("MagicImageButton").gameObject, canvasTransform);
            draggingObj.GetComponentInChildren<Button>().GetComponent<Image>().sprite = myItem.MyItemImage;
            hit.transform.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.gray;

            //複製を最前面に配置
            draggingObj.transform.SetAsLastSibling();

            //複製がレイキャストをブロックしないようにする =>インスペクターで完了(Canvas Groupを使用)

            //仲介人にアイテムを渡す
           GManager.instance.handSc.SetGrabbingItem(myItem);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (myItem == null) return;

        //複製がポインターを追従するようにする
        draggingObj.transform.position = GManager.instance.handSc.transform.position;
    }

   

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GManager.instance.handSc.IsHavingItem())
        {
            hit.transform.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            Destroy(GameObject.Find("MagicImageButton(Clone)"));
        }
        Destroy(GameObject.Find("MagicImageButton(Clone)"));

    }

    public void RemoveButton()
    {
        
        this.gameObject.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
        if (SaveSystem.Instance.UserData.setMagic1!= null && this.gameObject.GetComponentInChildren<Text>().text == SaveSystem.Instance.UserData.setMagic1.MyItemname)
        {
            MagicSetManager1.GetComponentInChildren<Text>().text = null;
            MagicSetManager1.GetComponentInChildren<Image>().sprite = null;
            MagicSetManager1.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.5f);
            SaveSystem.Instance.UserData.setMagic1 = null;
        }
        else if (SaveSystem.Instance.UserData.setMagic2 != null && this.gameObject.GetComponentInChildren<Text>().text == SaveSystem.Instance.UserData.setMagic2.MyItemname)
        {
            MagicSetManager2.GetComponentInChildren<Text>().text = null;
            MagicSetManager2.GetComponentInChildren<Image>().sprite = null;
            MagicSetManager2.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.5f);
            SaveSystem.Instance.UserData.setMagic2 = null;
        }
        else if (SaveSystem.Instance.UserData.setMagic3 != null && this.gameObject.GetComponentInChildren<Text>().text == SaveSystem.Instance.UserData.setMagic3.MyItemname)
        {
            MagicSetManager3.GetComponentInChildren<Text>().text = null;
            MagicSetManager3.GetComponentInChildren<Image>().sprite = null;
            MagicSetManager3.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.5f);
            SaveSystem.Instance.UserData.setMagic3 = null;
        }
        else if (SaveSystem.Instance.UserData.setMagic4 != null && this.gameObject.GetComponentInChildren<Text>().text == SaveSystem.Instance.UserData.setMagic4.MyItemname)
        {
            MagicSetManager4.GetComponentInChildren<Text>().text = null;
            MagicSetManager4.GetComponentInChildren<Image>().sprite = null;
            MagicSetManager4.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.5f);
            SaveSystem.Instance.UserData.setMagic4 = null;
        }
        SaveSystem.Instance.Save();
        
    }

 
}
