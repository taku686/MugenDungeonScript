using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class UserData 
{
    public string userName = "Default Name";
    public string job = "戦士";
    public int soldierLevel = 1;
    public int warriorLevel = 1;
    public int wizardLevel = 1;
    public int monkLevel = 1;
    public int playboyLevel = 1;
    public int thiefLevel = 1;
    public int soldierCurrentExperience = 1;
    public int warriorCurrentExperience = 1;
    public int wizardCurrentExperience = 1;
    public int monkCurrentExperience = 1;
    public int playboyCurrentExperience = 1;
    public int thiefCurrentExperience = 1;
    public int playerHP;
    public int playerMP;
    public int stage = 62;
    public int currentStage = 0;
    public List<Item> allItems = new List<Item>();
    public List<Item> equipmentItems = new List<Item>();
    public Item setMagic1;
    public Item setMagic2;
    public Item setMagic3;
    public Item setMagic4;


    public Item[] GetEquipmentedItem()
    {
        List<Item> items = new List<Item>();
        foreach(Item item in allItems.Where(it => it.isEquipented == true))
        {          
            items.Add(item);          
        }
        return items.ToArray();
    }

    public Item WeaponAttribute()
    {
        Item getItem = null;

        if (equipmentItems != null)
        {
            foreach (Item item in equipmentItems.Where(it => it.itemClass == Item.ItemClass.weapon))
            {
                getItem = item;
            }
        }
        return getItem;
    }

    public int GetExp()
    {
        if (SaveSystem.Instance.UserData.job == "戦士")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.soldierLevel)- SaveSystem.Instance.UserData.soldierCurrentExperience);
        }
        else if (SaveSystem.Instance.UserData.job == "武闘家")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.warriorLevel) - SaveSystem.Instance.UserData.warriorCurrentExperience);
        }
        else if (SaveSystem.Instance.UserData.job == "僧侶")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.monkLevel) - SaveSystem.Instance.UserData.monkCurrentExperience);
        }
        else if (SaveSystem.Instance.UserData.job == "魔法使い")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.wizardLevel) - SaveSystem.Instance.UserData.wizardCurrentExperience);
        }
        else if (SaveSystem.Instance.UserData.job == "盗賊")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.thiefLevel) - SaveSystem.Instance.UserData.thiefCurrentExperience);
        }
        else if (SaveSystem.Instance.UserData.job == "遊び人")
        {
            return (int)(100 * Mathf.Pow(1.1f, SaveSystem.Instance.UserData.playboyLevel) - SaveSystem.Instance.UserData.playboyCurrentExperience);
        }
        return 0;
    }
 }
