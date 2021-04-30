using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    private List<Item> items;


    [SerializeField] private InitialPlayerStatus initialPlayerStatus;
    private int level;




    public static PlayerStatus instance;
    
    private void Awake()

    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }
    
    public int Level()
    {

        if (SaveSystem.Instance.UserData.job == "戦士")
        {
            return level = SaveSystem.Instance.UserData.soldierLevel;
        }
        else if (SaveSystem.Instance.UserData.job == "武闘家")
        {
            return level = SaveSystem.Instance.UserData.warriorLevel;
        }
        else if (SaveSystem.Instance.UserData.job == "僧侶")
        {
            return level = SaveSystem.Instance.UserData.monkLevel;
        }
        else if (SaveSystem.Instance.UserData.job == "魔法使い")
        {
            return level = SaveSystem.Instance.UserData.wizardLevel;
        }
        else if (SaveSystem.Instance.UserData.job == "盗賊")
        {
            return level = SaveSystem.Instance.UserData.thiefLevel;
        }
        else if (SaveSystem.Instance.UserData.job == "遊び人")
        {
            return level = SaveSystem.Instance.UserData.playboyLevel;
        }
        return level = 0;
    }




    public int HP(string job)
    {
        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;

        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.hp;
                    //     Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.hp;
                    //    Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.hp;
                    //    Debug.Log("food" + foodValue);
                }
            }
        }

        if (job == "アサシン" || job == "魔法使い" || job == "僧侶" || job == "遊び人" || job == "ギャンブラー" || job == "神官" || job == "賢者" || job == "盗賊")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(20) * Level());

        }
        else if (job == "戦士" || job == "武闘家" || job == "バトルマスター" || job == "バーサーカー")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(40) * Level());
        }
        else if (job == "無し")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(70) * Level());
        }

        return initialPlayerStatus.hp + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public int MP(string job)
    {



        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;

        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.mp;
                    //   Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.mp;
                    //   Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.mp;
                    //    Debug.Log("food" + foodValue);
                }
            }
        }
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる

        if (job == "アサシン" || job == "戦士" || job == "武闘家" || job == "遊び人" || job == "バトルマスター" || job == "バーサーカー" || job == "盗賊")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(2) * Level());

        }
        else if (job == "魔法使い" || job == "僧侶" || job == "賢者" || job == "神官" || job == "ギャンブラー")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(5) * Level());
        }
        else if (job == "無し")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(10) * Level());
        }

        return initialPlayerStatus.mp + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }
    public int PhyAtk(string job)
    {



        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.physicsAtk;
                    //    Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.physicsAtk;
                    //    Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.physicsAtk;
                    //  Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "武闘家" || job == "魔法使い" || job == "僧侶" || job == "遊び人" || job == "ギャンブラー" || job == "神官" || job == "賢者")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(20) * Level());

        }
        else if (job == "戦士" || job == "盗賊" || job == "バトルマスター" || job == "アサシン")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(40) * Level());
        }
        else if (job == "バーサーカー")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(70) * Level());
        }

        return initialPlayerStatus.physicsAtk + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public int PhyDef(string job)
    {



        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.physicsDef;
                    //   Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.physicsDef;
                    //   Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.physicsDef;
                    //    Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "戦士" || job == "武闘家" || job == "魔法使い" || job == "僧侶" || job == "遊び人" || job == "盗賊" || job == "賢者" || job == "ギャンブラー" || job == "アサシン" || job == "バーサーカー")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(20) * Level());

        }
        else if (job == "バトルマスター" || job == "神官")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(40) * Level());
        }
        else if (job == "無し")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(70) * Level());
        }

        return initialPlayerStatus.physicsDef + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public int MagAtk(string job)
    {



        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.magicAtk;
                    //    Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.magicAtk;
                    //   Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.magicAtk;
                    //    Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "戦士" || job == "武闘家" || job == "僧侶" || job == "遊び人" || job == "盗賊" || job == "バトルマスター" || job == "バーサーカー" || job == "神官" || job == "ギャンブラー")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(20) * Level());

        }
        else if (job == "魔法使い" || job == "アサシン")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(40) * Level());
        }
        else if (job == "賢者")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(70) * Level());
        }

        return initialPlayerStatus.magicAtk + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public int MagDef(string job)
    {



        int weaponValue = 0;
        int armorValue = 0;
        int foodValue = 0;
        int riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.magicDef;
                    //    Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.magicDef;
                    //    Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.magicDef;
                    //    Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "戦士" || job == "武闘家" || job == "魔法使い" || job == "遊び人" || job == "盗賊" || job == "バトルマスター" || job == "バーサーカー" || job == "ギャンブラー" || job == "アサシン")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(20) * Level());

        }
        else if (job == "僧侶" || job == "賢者")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(40) * Level());
        }
        else if (job == "神官")
        {
            riseValue = Mathf.RoundToInt(Mathf.Sqrt(70) * Level());
        }

        return initialPlayerStatus.magicDef + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public float Evasion(string job)
    {



        float weaponValue = 0;
        float armorValue = 0;
        float foodValue = 0;
        float riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.evasion;
                    //   Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.evasion;
                    //    Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.evasion;
                    //   Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "戦士" || job == "魔法使い" || job == "僧侶" || job == "賢者" || job == "神官")
        {
            riseValue = (Mathf.Sqrt(0.03f) * Level() );

        }
        else if (job == "武闘家" || job == "遊び人" || job == "盗賊" || job == "バトルマスター" || job == "バーサーカー" || job == "ギャンブラー")
        {
            riseValue = (Mathf.Sqrt(0.1f) * Level() );
        }
        else if (job == "アサシン")
        {
            riseValue = (Mathf.Sqrt(0.3f) * Level() );
        }
        float s = initialPlayerStatus.evasion + riseValue + weaponValue + armorValue + foodValue;
        s = s * 10;
        s = Mathf.Floor(s) ;

        return s;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public float Luck(string job)
    {
        float weaponValue = 0;
        float armorValue = 0;
        float foodValue = 0;
        float riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.luck;
                    //   Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.luck;
                    //   Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.luck;
                    //   Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "戦士" || job == "武闘家" || job == "魔法使い" || job == "僧侶" || job == "盗賊" || job == "バトルマスター" || job == "賢者" || job == "バーサーカー" || job == "神官" || job == "アサシン")
        {
            riseValue = (Mathf.Sqrt(0.03f) * Level() );

        }
        else if (job == "遊び人")
        {
            riseValue = (Mathf.Sqrt(0.1f) * Level() );
        }
        else if (job == "ギャンブラー")
        {
            riseValue = (Mathf.Sqrt(0.3f) * Level() );
        }
        float s = initialPlayerStatus.luck + riseValue + weaponValue + armorValue + foodValue;
        s = s * 10;
        s = Mathf.Floor(s);
        return s;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public float Critical(string job)
    {



        float weaponValue = 0;
        float armorValue = 0;
        float foodValue = 0;
        float riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.critical;
                    //   Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.critical;
                    //   Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.critical;
                    //   Debug.Log("food" + foodValue);
                }
            }
        }
        if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.02f) * Level() / 1000);

        }
        else if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.05f) * Level() / 1000);
        }
        else if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.1f) * Level() / 1000);
        }

        return initialPlayerStatus.critical + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }

    public float JustGuard(string job)
    {



        float weaponValue = 0;
        float armorValue = 0;
        float foodValue = 0;
        float riseValue = 0;
        //すべてのitemに対しArmorであるか問い、ArmorであればitemAtkに値を入れる
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {

                if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.armor)
                {
                    Armor armor = SaveSystem.Instance.UserData.equipmentItems[i] as Armor;
                    armorValue += armor.itemStatus.justGuard;
                    //  Debug.Log("armor" + armorValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.weapon)
                {
                    Weapon weapon = SaveSystem.Instance.UserData.equipmentItems[i] as Weapon;
                    weaponValue += weapon.itemStatus.justGuard;
                    //    Debug.Log("weapon" + weaponValue);
                }
                else if (SaveSystem.Instance.UserData.equipmentItems[i].itemClass == Item.ItemClass.food)
                {
                    Food food = SaveSystem.Instance.UserData.equipmentItems[i] as Food;
                    foodValue += food.itemStatus.justGuard;
                    //   Debug.Log("food" + foodValue);
                }
            }
        }

        if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.01f) * Level() / 1000);

        }
        else if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.02f) * Level() / 1000);
        }
        else if (job == "無し")
        {
            riseValue = (Mathf.Sqrt(0.05f) * Level() / 1000);
        }

        return initialPlayerStatus.justGuard + riseValue + weaponValue + armorValue + foodValue;//初期ステータス＋上昇ステータス＋装備ステータス＋アイテムでの上昇

    }



    //外部からPlayerのhpを変更したいときに使う関数
    public void ChangeHP(int delta)
    {
        int PlayerHp = GManager.instance.playerHP + delta;
        if (GManager.instance.isGameOver == true) return;
        if (PlayerHp <= 0 )
        {        
            GManager.instance.Gameover();
            return;
        }
        else if (PlayerHp > HP(SaveSystem.Instance.UserData.job)) PlayerHp = HP(SaveSystem.Instance.UserData.job);
        else GManager.instance.playerHP = PlayerHp;
       

        SaveSystem.Instance.UserData.playerHP = PlayerHp;
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerHp;
        GManager.instance.playerHPText.text = PlayerHp + "/" + HP(SaveSystem.Instance.UserData.job);

        Debug.Log("現在のHpは" + GManager.instance.playerHP + "です");
    }
    public void ChangeMP(int delta)
    {
        int PlayerMp = GManager.instance.playerMP + delta;
        if (PlayerMp <= 0 )
        {
            GManager.instance.playerMP = 0;
            PlayerMp = 0;
        }
        else if (PlayerMp > MP(SaveSystem.Instance.UserData.job)) PlayerMp = MP(SaveSystem.Instance.UserData.job);
        else GManager.instance.playerMP = PlayerMp;


        SaveSystem.Instance.UserData.playerMP = PlayerMp;
        SaveSystem.Instance.Save();
        GManager.instance.mpSlider.value = PlayerMp;
        GManager.instance.playerMPText.text = PlayerMp + "/" + MP(SaveSystem.Instance.UserData.job);

        Debug.Log("現在のMpは" + GManager.instance.playerMP + "です");
    }
    public void LevelUp(int experience)
    {
        int level = 0;
        int currentExperience =0;
        int levelUpValue = 100;
        if (SaveSystem.Instance.UserData.job == "戦士")
        {
             level = SaveSystem.Instance.UserData.soldierLevel;
            currentExperience = SaveSystem.Instance.UserData.soldierCurrentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "武闘家")
        {
             level = SaveSystem.Instance.UserData.warriorLevel;
            currentExperience = SaveSystem.Instance.UserData.warriorCurrentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "僧侶")
        {
             level = SaveSystem.Instance.UserData.monkLevel;
            currentExperience = SaveSystem.Instance.UserData.monkCurrentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "魔法使い")
        {
             level = SaveSystem.Instance.UserData.wizardLevel;
            currentExperience = SaveSystem.Instance.UserData.wizardCurrentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "盗賊")
        {
             level = SaveSystem.Instance.UserData.thiefLevel;
            currentExperience = SaveSystem.Instance.UserData.thiefCurrentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "遊び人")
        {
             level = SaveSystem.Instance.UserData.playboyLevel;
            currentExperience = SaveSystem.Instance.UserData.playboyCurrentExperience;
        }
        currentExperience += experience;
        if (SaveSystem.Instance.UserData.job == "戦士")
        {
            SaveSystem.Instance.UserData.soldierCurrentExperience = currentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "武闘家")
        {
            SaveSystem.Instance.UserData.warriorCurrentExperience = currentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "僧侶")
        {
            SaveSystem.Instance.UserData.monkCurrentExperience = currentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "魔法使い")
        {
            SaveSystem.Instance.UserData.wizardCurrentExperience = currentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "盗賊")
        {
            SaveSystem.Instance.UserData.thiefCurrentExperience = currentExperience;
        }
        else if (SaveSystem.Instance.UserData.job == "遊び人")
        {
            SaveSystem.Instance.UserData.playboyCurrentExperience = currentExperience;
        }
        SaveSystem.Instance.Save();
        if (currentExperience > levelUpValue * Mathf.Pow(1.1f, level))
        {
            currentExperience = 0;
            level++;
            if (SaveSystem.Instance.UserData.job == "戦士")
            {
                SaveSystem.Instance.UserData.soldierLevel = level;
                SaveSystem.Instance.UserData.soldierCurrentExperience = currentExperience;
            }
            else if (SaveSystem.Instance.UserData.job == "武闘家")
            {
                SaveSystem.Instance.UserData.warriorLevel = level;
                SaveSystem.Instance.UserData.warriorCurrentExperience = currentExperience;
            }
            else if (SaveSystem.Instance.UserData.job == "僧侶")
            {
                SaveSystem.Instance.UserData.monkLevel = level;
                SaveSystem.Instance.UserData.monkCurrentExperience = currentExperience;
            }
            else if (SaveSystem.Instance.UserData.job == "魔法使い")
            {
                SaveSystem.Instance.UserData.wizardLevel = level;
                SaveSystem.Instance.UserData.wizardCurrentExperience = currentExperience;
            }
            else if (SaveSystem.Instance.UserData.job == "盗賊")
            {
                SaveSystem.Instance.UserData.thiefLevel = level;
                SaveSystem.Instance.UserData.thiefCurrentExperience = currentExperience;
            }
            else if (SaveSystem.Instance.UserData.job == "遊び人")
            {
                SaveSystem.Instance.UserData.playboyLevel = level;
                SaveSystem.Instance.UserData.playboyCurrentExperience = currentExperience;
            }
            SaveSystem.Instance.Save();
        }
      
    }
}

