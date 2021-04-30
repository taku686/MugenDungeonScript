using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Item grabbingItem; //Slotから受け取ったアイテム
    [SerializeField] Menu menuSc;

    // Update is called once per frame
    void Update()
    {
        if (menuSc.isHandActive)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    //Handのアイテムを受け渡しする関数
    public Item GetGrabbingItem()
    {
        Item oldItem = grabbingItem;
        grabbingItem = null;
        return oldItem;

    }

    //Handにアイテムを代入するための関数
    public void SetGrabbingItem(Item item)
    {
        grabbingItem = item;
    }

    //grbbingItemがnullじゃないかチェックする
    public bool IsHavingItem()
    {
        return grabbingItem != null;
    }
}
