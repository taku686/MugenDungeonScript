using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class SlotGrid : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    public List<GameObject> slotPrefabs = new List<GameObject>();

    public int slotNumber = 28;

    [SerializeField] public List<Item> allItems;


    // Start is called before the first frame update
    private void Awake()
    {
        allItems = SaveSystem.Instance.UserData.allItems;
        for (int i = 0; i < slotNumber; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, this.transform);
            slotPrefabs.Add(slotObj);

            Slot slot = slotObj.GetComponent<Slot>();

            //スロットにアイテムをセットしたい
            if (i < allItems.Count)
            {
                slot.SetItem(allItems[i]);
            }
            else
            {
                slot.SetItem(null);
            }

        }
    }
 

}
*/