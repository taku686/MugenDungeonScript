using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Menu : MonoBehaviour
{
   
    [SerializeField] private GameObject pausePanel;   
    [SerializeField] private ItemMenu selectButton;
    [SerializeField] private ItemMenu itemUseOrDestroy;
    [SerializeField] public ItemMenu statusScene;
    [SerializeField] private ItemMenu itemScene;
    [SerializeField] private ItemMenu armorScene;
    [SerializeField] private ItemMenu weapon1Scene;
    [SerializeField] private ItemMenu weapon2Scene;
    [SerializeField] private ItemMenu magicScene;
    [SerializeField] private GameObject selectButtonObj;
    [SerializeField] private GameObject itemUseOrDestroyObj;
    [SerializeField] private GameObject statusSceneObj;
    [SerializeField] private GameObject itemSceneObj;
    [SerializeField] private GameObject aromorSceneObj;
    [SerializeField] private GameObject weapon1SceneObj;
    [SerializeField] private GameObject weapon2SceneObj;
    [SerializeField] private GameObject magicSceneObj;
    [SerializeField] private GameObject magicSet;
    [SerializeField] private ItemGrid foodItemGrid;
    [SerializeField] private ItemGrid armorItemGrid;
    [SerializeField] private ItemGrid weapon1ItemGrid;
    [SerializeField] private ItemGrid weapon2ItemGrid;
    [SerializeField] private ItemGrid magicItemGrid;
    [SerializeField] public GameObject StatusRightHand;
    [SerializeField] public GameObject StatusLeftHand;
    [SerializeField] public GameObject StatusHead;
    [SerializeField] public GameObject StatusUpperBody;
    [SerializeField] public GameObject StatusLowerBody;
    [SerializeField] public GameObject StatusArm;
    [SerializeField] public GameObject StatusAccessories;
    [SerializeField] public GameObject StageImage;
    [SerializeField] public GameObject selectFloorImage;
    [SerializeField] public GameObject selectFloorContent;
    [SerializeField] private Image getItemImage;
    [SerializeField] private AudioClip decisionSound;
    [SerializeField] private AudioClip cursorSound;
    [SerializeField] private AudioClip cancelSound;
    [SerializeField] private AudioClip removeSound;
    [SerializeField] private ItemMenu ArmorOrderbyImage;
  //  [SerializeField] private ItemMenu Weapon1OrderbyImage;
  //  [SerializeField] private ItemMenu Weapon2OrderbyImage;
  //  [SerializeField] private ItemMenu ItemOrderbyImage;
    public bool isUse;
    public bool isDestroy;
    public bool isHandActive;
    private bool isAccessories;
    private bool isArm;
    private bool isBoomerang;
    private bool isBow;
    private bool isClub;
    private bool isFan;
    private bool isFood;
    private bool isHammer;
    private bool isHead;
    private bool isKnife;
    private bool isLargeSword;
    private bool isLowerBody;
    private bool isNail;
    private bool isRod;
    private bool isShield;
    private bool isSpear;
    private bool isSword;
    private bool isUpperBody;
    private bool isWhip;



    public void Pause()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

   
    public void MenuButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        selectButton.Toggle();
        magicSet.SetActive(false);
    }

    public void StatusButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        statusScene.Toggle();
        selectButton.Toggle();
        statusScene.GetComponent<StatusScene>().TextChange();
        DisplayEquipmentedItem();
    }
    
    public void MagicButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        magicScene.Toggle();
        selectButton.Toggle();
        magicItemGrid.MagicFireDisplay();
        magicSet.SetActive(true);
        isHandActive = true;

    }


    public void ItemButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        itemUseOrDestroy.Toggle();
        selectButton.Toggle();
    }

    public void UseButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        itemScene.Toggle();
        itemUseOrDestroy.Toggle();
        foodItemGrid.FoodDisplay();
        isUse = true;
    }

    public void DestroyButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        itemScene.Toggle();
        itemUseOrDestroy.Toggle();
        foodItemGrid.FoodDisplay();
        isDestroy = true;
    }

    public void ItemSceneArmorButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        itemScene.Toggle();
        armorScene.Toggle();
        armorItemGrid.HeadDisplay();
    }
    public void ItemSceneWeaponButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        itemScene.Toggle();
        weapon1Scene.Toggle();
        weapon1ItemGrid.SwordDisplay();
    }
    public void RightHandButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1Scene.Toggle();
        statusScene.Toggle();
        weapon1ItemGrid.SwordDisplay();
        
    }

    public void LeftHandButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1Scene.Toggle();
        statusScene.Toggle();
        weapon1ItemGrid.SwordDisplay();
       
    }

    public void HeadButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        armorItemGrid.HeadDisplay();
        
    }

    public void UpperBodyButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        armorItemGrid.UpperBodyDisplay();
      
    }
    public void LowerBodyButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        armorItemGrid.LowerBodyDisplay();
       
    }
    public void ArmButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        armorItemGrid.ArmDisplay();
       
    }
    public void AccessoriesButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        armorItemGrid.AccessoriesDisplay();
      
    }

    public void ArmorRightArrowButton()
    {
        SoundManager.instance.PlaySingle(cursorSound);
        weapon1Scene.Toggle();
        armorScene.Toggle();
        weapon1ItemGrid.SwordDisplay();
    }

    public void ArmorLeftArrowButton()
    {
        SoundManager.instance.PlaySingle(cursorSound);
        weapon2Scene.Toggle();
        armorScene.Toggle();
        weapon2ItemGrid.WhipDisplay();
    }

    public void ArmorHeadButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorItemGrid.HeadDisplay();
    }
    public void ArmorUpperBodyButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorItemGrid.UpperBodyDisplay();
    }
    public void ArmorLowerBodyButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorItemGrid.LowerBodyDisplay();
    }
    public void ArmorArmButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorItemGrid.ArmDisplay();
    }
    public void ArmorAccessoriesButton()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        armorItemGrid.AccessoriesDisplay();
    }

    public void Weapon1LargeSword()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.LargeSwordDisplay();
    }
    public void Weapon1Sword()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.SwordDisplay();
    }
    public void Weapon1Spear()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.SpearDisplay();
    }
    public void Weapon1Axe()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.AxeDisplay();
    }
    public void Weapon1Hammer()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.HammerDisplay();
    }
    public void Weapon1Knife()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.KnifeDisplay();
    }
    public void Weapon1Rod()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1ItemGrid.RodDisplay();
    }
    public void Weapon1RightArrowButton()
    {
        SoundManager.instance.PlaySingle(cursorSound);
        weapon1Scene.Toggle();
        weapon2Scene.Toggle();
        weapon2ItemGrid.WhipDisplay();
    }
    public void Weapon1LeftArrowButton()
    {
        SoundManager.instance.PlaySingle(cursorSound);
        weapon1Scene.Toggle();
        armorScene.Toggle();
        armorItemGrid.HeadDisplay();
    }

    public void Weapon2Whip()
    {
        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.WhipDisplay();
    }
    public void Weapon2Nail()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.NailDisplay();
    }
    public void Weapon2Bow()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.BowDisplay();
    }
    public void Weapon2Club()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.ClubDisplay();
    }
    public void Weapon2Fan()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.FanDisplay();
    }
    public void Weapon2Boomerang()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.BoomerangDisplay();
    }
    public void Weapon2Shield()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        weapon2ItemGrid.ShieldDisplay();
    }

    public void MagicFire()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicFireDisplay();
    }
    public void MagicIce()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicIceDisplay();
    }
    public void MagicWind()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicWindDisplay();
    }
    public void MagicThunder()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicThunderDisplay();
    }
    public void MagicHoly()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicHolyDisplay();
    }
    public void MagicDark()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicDarkDisplay();
    }
    public void MagicRecovery()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicRecoveryDisplay();
    }
    public void MagicLastResort()
    {

        SoundManager.instance.PlaySingle(decisionSound);
        magicItemGrid.MagicLastResortDisplay();
    }
    public void Weapon2LeftArrowButton()
    {

        SoundManager.instance.PlaySingle(cursorSound);
        weapon2Scene.Toggle();
        weapon1Scene.Toggle();
        weapon1ItemGrid.SwordDisplay();
    }
    public void Weapon2RightArrowButton()
    {
        SoundManager.instance.PlaySingle(cursorSound);
        weapon2Scene.Toggle();
        armorScene.Toggle();
        armorItemGrid.HeadDisplay();
    }

    public void ArmorSceneBackButton()
    {
       
        if (isUse || isDestroy) return;
        SoundManager.instance.PlaySingle(decisionSound);
        armorScene.Toggle();
        statusScene.Toggle();
        DisplayEquipmentedItem();
        statusScene.GetComponent<StatusScene>().TextChange();
    }
    public void Weapon1SceneBackButton()
    {
        if (isUse || isDestroy) return;
        SoundManager.instance.PlaySingle(decisionSound);
        weapon1Scene.Toggle();
        statusScene.Toggle();
        DisplayEquipmentedItem();
        statusScene.GetComponent<StatusScene>().TextChange();
    }
    public void Weapon2SceneBackButton()
    {
        if (isUse || isDestroy) return;
        SoundManager.instance.PlaySingle(decisionSound);
        weapon2Scene.Toggle();
        statusScene.Toggle();
        DisplayEquipmentedItem();
        statusScene.GetComponent<StatusScene>().TextChange();
    }

    public void ArmorOrderbyButton()
    {
        ArmorOrderbyImage.Toggle();
    }
    public void Weapon1OrderbyButton()
    {
    //    Weapon1OrderbyImage.Toggle();
    }
    public void Weapon2OrderbyButton()
    {
    //   Weapon2OrderbyImage.Toggle();
    }
    public void ItemOrderbyButton()
    {
   //     ItemOrderbyImage.Toggle();
    }

    public void PhyDefOrderbyButton()
    {
        SaveSystem.Instance.UserData.allItems.OrderBy(ai => ai.itemStatus.physicsDef);
    }

    public void MagDefOrderbyButton()
    {
        SaveSystem.Instance.UserData.allItems.OrderBy(ai => ai.itemStatus.magicDef);
    }

    public void JusGuaOrderbyButton()
    {
        SaveSystem.Instance.UserData.allItems.OrderBy(ai => ai.itemStatus.justGuard);
    }

    public void WordOrderOrderbyButton()
    {
        SaveSystem.Instance.UserData.allItems.OrderBy(ai => ai.MyItemname);
    }

    public void CloseButton()
    {
        SoundManager.instance.PlaySingle(cancelSound);
        selectButtonObj.SetActive(false);
        itemUseOrDestroyObj.SetActive(false);
        statusSceneObj.SetActive(false);
        itemSceneObj.SetActive(false);
        aromorSceneObj.SetActive(false);
        weapon1SceneObj.SetActive(false);
        weapon2SceneObj.SetActive(false);
        magicSceneObj.SetActive(false);
        selectFloorImage.SetActive(false);
        magicSet.SetActive(true);
        isUse = false;
        isDestroy = false;
        isHandActive = false;

    }

    public void RightHandRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.ItemClass.weapon == SaveSystem.Instance.UserData.equipmentItems[i].itemClass)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                 
                }
            }
        }
    }
    public void LeftHandRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.Shield == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                   
                }
            }
        }
    }
    public void HeadRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.Head == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                   
                }
            }
        }
    }
    public void UpperBodyRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.UpperBody == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                 
                }
            }
        }
    }
    public void LowerBodyRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.LowerBody == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                   
                }
            }
        }
    }
    public void ArmRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.Arm == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                   
                }
            }
        }
    }
    public void AccessoriesRemoveButton()
    {
        if (SaveSystem.Instance.UserData.equipmentItems != null)
        {
            for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
            {
                if (Item.Type.Accessories == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
                {
                    SoundManager.instance.PlaySingle(removeSound);
                    SaveSystem.Instance.UserData.equipmentItems.Remove(SaveSystem.Instance.UserData.equipmentItems[i]);
                    SaveSystem.Instance.Save();
                    StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                    StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().sprite = null;
                    statusScene.GetComponent<StatusScene>().TextChange();
                  
                }
            }
        }
    }
    public void DisplayEquipmentedItem()
    {
        for (int i = 0; i < SaveSystem.Instance.UserData.equipmentItems.Count; i++)
        {
            if (Item.ItemClass.weapon == SaveSystem.Instance.UserData.equipmentItems[i].itemClass )
            {
                StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusRightHand.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;            
            }
            else if(Item.Type.Shield == SaveSystem.Instance.UserData.equipmentItems[i].itemType )
            {
                StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusLeftHand.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
            else if(Item.Type.Head == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
            {
                StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusHead.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
            else if (Item.Type.UpperBody == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
            {
                StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusUpperBody.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
            else if (Item.Type.LowerBody == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
            {
                StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusLowerBody.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
            else if (Item.Type.Arm == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
            {
                StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusArm.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
            else if (Item.Type.Accessories == SaveSystem.Instance.UserData.equipmentItems[i].itemType)
            {
                StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().sprite = SaveSystem.Instance.UserData.equipmentItems[i].MyItemImage;
                StatusAccessories.GetComponentInChildren<Button>().GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void StageImageSwitch()
    {
        if (GManager.instance.isGameOver == false)
        {
            StageImage.SetActive(true);
            StageImage.GetComponentInChildren<Text>().text = "Stage" + GManager.instance.stage;
        }
        else if(GManager.instance.isGameOver == true)
        {
            StageImage.SetActive(true);
            StageImage.GetComponentInChildren<Text>().text = "はじまりの階";
        }
        Invoke("HideLevelImage", 2f);
    }

    private void HideLevelImage()
    {
        StageImage.SetActive(false);
        GManager.instance.doingSetup = false;
    }

    public void ShowGetItem(Item choiceItem)
    {
        getItemImage.sprite = choiceItem.MyItemImage;
        getItemImage.color = Color.white;
        StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {
        yield return new WaitForSeconds(1);
        getItemImage.sprite = null;
        getItemImage.color = new Color(0, 0, 0, 0.5f);
    }

}
