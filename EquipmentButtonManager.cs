using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;


public class EquipmentButtonManager : MonoBehaviour
{
    private List<Item> selection = new List<Item>();
    [SerializeField] private Text text;
    [SerializeField] private AudioClip equipmetSound;
   
   
    public void ButtonClick()
    {
        foreach(var equipmentItem in SaveSystem.Instance.UserData.equipmentItems.Where(ei => ei.MyItemname == text.text))
        {
            return;
        }
        Menu menuSc = GameObject.Find("Canvas").GetComponent<Menu>();
        if(menuSc.isUse || menuSc.isDestroy)
        {
            return;
        }
        if (selection != null)
        {
            selection.Clear();
        }

        if (this.gameObject.GetComponentInChildren<Text>() != null)
        {
            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.MyItemname == text.text))
            {
                selection.Add(allitem);
            }          
            SoundManager.instance.PlaySingle(equipmetSound);
            SaveSystem.Instance.UserData.equipmentItems.Add(selection[0]);
            SaveSystem.Instance.Save();         
            menuSc.DisplayEquipmentedItem();
        }
    }

    public void ShowDetail()
    {
        GameObject detailImage = GameObject.Find("EquipmentImage");
        GameObject detailStaus = GameObject.Find("EquipmentStatus");
        Text[] statusTexts = detailStaus.GetComponentsInChildren<Text>();

        if (this.gameObject.GetComponentInChildren<Text>() != null)
        {

            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.MyItemname == text.text))
            {
                detailImage.GetComponent<Image>().sprite =allitem.MyItemImage;
                detailImage.GetComponent<Image>().color = Color.white;
                statusTexts[0].text = allitem.MyItemname;
                statusTexts[1].text = "HP : " + allitem.itemStatus.hp;
                statusTexts[2].text = "MP : " + allitem.itemStatus.mp;
                statusTexts[3].text = "物理攻撃力 : " + allitem.itemStatus.physicsAtk;
                statusTexts[4].text = "物理防御力 : " + allitem.itemStatus.physicsDef;
                statusTexts[5].text = "魔法攻撃力 : " + allitem.itemStatus.magicAtk;
                statusTexts[6].text = "魔法防御力 : " + allitem.itemStatus.magicDef;
                statusTexts[7].text = "回避 : " + allitem.itemStatus.evasion;
                statusTexts[8].text = "運 : " + allitem.itemStatus.luck;
                statusTexts[9].text = "会心率 : " + allitem.itemStatus.critical;
                statusTexts[10].text = "会心ガード率 : " + allitem.itemStatus.justGuard;
                statusTexts[11].text = "属性 : " + allitem.itemAttribute;
            }
             
           
        }
    }

    public void FoodButton()
    {
        Menu menuSc = GameObject.Find("Canvas").GetComponent<Menu>();
        PlayerStatus playerStatusSc = GameObject.Find("Canvas").GetComponent<PlayerStatus>();
        List<Item> items = new List<Item>();
        items.Clear();
        if (menuSc.isUse)
        {
            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.MyItemname == text.text && ai.itemClass == Item.ItemClass.food))
            {
                items.Add(allitem);
            }
            SoundManager.instance.PlaySingle(items[items.Count - 1].se);
            playerStatusSc.ChangeHP(items[items.Count - 1].itemStatus.hp);
            playerStatusSc.ChangeMP(items[items.Count - 1].itemStatus.mp);
            SaveSystem.Instance.UserData.allItems.Remove(items[items.Count - 1]);
            SaveSystem.Instance.Save();
            Destroy(this.gameObject);
        }
        else if (menuSc.isDestroy)
        {
            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.MyItemname == text.text && ai.itemClass == Item.ItemClass.food))
            {
                items.Add(allitem);
            }
            SaveSystem.Instance.UserData.allItems.Remove(items[0]);
            SaveSystem.Instance.Save();
            Destroy(this.gameObject);
            List<Item> deleteItems = new List<Item>();
            foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai == items[0]))
            {
                deleteItems.Add(allitem);
            }
            if (deleteItems.Count == 0)
            {
                foreach (var equipmentitem in SaveSystem.Instance.UserData.equipmentItems.Where(ei => ei == items[0]))
                {
                    SaveSystem.Instance.UserData.equipmentItems.Remove(equipmentitem);
                }
            }
            SaveSystem.Instance.Save();
            if (items[0].itemClass == Item.ItemClass.weapon)
            {
                menuSc.StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.Shield)
            {
                menuSc.StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.Head)
            {
                menuSc.StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.UpperBody)
            {
                menuSc.StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.LowerBody)
            {
                menuSc.StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.Arm)
            {
                menuSc.StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
            else if (items[0].itemType == Item.Type.Accessories)
            {
                menuSc.StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                menuSc.StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                menuSc.statusScene.GetComponent<StatusScene>().TextChange();
            }
        }
    }

}
