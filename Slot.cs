using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    private Item item;

    [SerializeField]protected Image itemImage;

    protected GameObject draggingObj;

    [SerializeField]protected GameObject itemImageObj;

    protected Transform CanvasTransform;

    protected Hand hand;

    private SlotGrid slotGrid;

    public Item MyItem { get => item; protected set => item = value; }

    protected virtual void Start()
    {
        CanvasTransform = FindObjectOfType<Canvas>().transform;

        slotGrid = GameObject.Find("Slotgrid").GetComponent<SlotGrid>();

        hand = FindObjectOfType<Hand>();

        if (MyItem == null) itemImage.color = new Color(0, 0, 0, 0);
    }

    //Slotにアイテムをセットする
    public virtual void SetItem(Item item)
    {
        MyItem = item;
        if (item != null)
        {
            itemImage.color = new Color(1, 1, 1, 1);
            itemImage.sprite = item.MyItemImage;
        }
        else
        {
            itemImage.color = new Color(0, 0, 0, 0);
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (MyItem == null) return;

        //アイテムのイメージを複製する
        draggingObj = Instantiate(itemImageObj, CanvasTransform);

        //複製を最前面に配置
        draggingObj.transform.SetAsLastSibling();

        //複製元の色を暗くする
        itemImage.color = Color.gray;

        //複製がレイキャストをブロックしないようにする =>インスペクターで完了

        //仲介人にアイテムを渡す
        hand.SetGrabbingItem(MyItem);

        slotGrid.allItems.Remove(MyItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (MyItem == null) return;

        //複製がポインターを追従するようにする
        draggingObj.transform.position = hand.transform.position;
    }



    //相手スロット側の操作
    public virtual void OnDrop(PointerEventData eventData)
    {
        //仲介人がアイテムを持っていなかったら早期return
        if (!hand.IsHavingItem()) return;

        //仲介人からアイテムを受け取る
        Item gotItem = hand.GetGrabbingItem();

        //もともと持っていたアイテムを仲介人に渡す
        hand.SetGrabbingItem(MyItem);

        SetItem(gotItem);

        slotGrid.allItems.Add(gotItem);
    }

    //OnDropが先に呼ばれるためSlot以外の場所で呼ばれるとアイテムをそのまま返却する
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggingObj);

        //仲介人からアイテムを受け取る
        Item gotItem = hand.GetGrabbingItem();
        SetItem(gotItem);
        if (gotItem == null) return;
        slotGrid.allItems.Add(gotItem);
    }
}
*/