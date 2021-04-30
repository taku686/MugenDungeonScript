using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
public class PlayerSlot : Slot
{
    private PlayerStatus playerstatus;

    public PlayerStatus MyPlayerstatus { get => playerstatus;private set => playerstatus = value; }


    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();  //継承元のstart関数を実行しろという意味
        MyPlayerstatus = FindObjectOfType<PlayerStatus>();
        if (SaveSystem.Instance.UserData.equipmentItems[0] != null)
        {
            SetItem(SaveSystem.Instance.UserData.equipmentItems[0]);
        }
    }



    public override void SetItem(Item item)
    {
        PlayerStatus.instance.RemoveItem(MyItem);
        PlayerStatus.instance.SetItem(item);

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

    public override void OnBeginDrag(PointerEventData eventData)
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

       
    }

    public override void OnDrop(PointerEventData eventData)
    {
        //仲介人がアイテムを持っていなかったら早期return
        if (!hand.IsHavingItem()) return;

        //仲介人からアイテムを受け取る
        Item gotItem = hand.GetGrabbingItem();

        //もともと持っていたアイテムを仲介人に渡す
        hand.SetGrabbingItem(MyItem);

        SetItem(gotItem);

       
    }
}
*/