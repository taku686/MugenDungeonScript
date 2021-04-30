using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UniRx;

public class ItemGrid : MonoBehaviour
{
  //  delegate void test(string s);
   
  //  test t;


    [SerializeField] private GameObject itemGrid;
    [SerializeField] private GameObject magicGrid;
    private List<GameObject> itemGrids = new List<GameObject>();
  //  private List<Item> items = new List<Item>();
  
  
    public void FoodDisplay()
    {
        /*
        t = (TEST) => { Debug.Log(TEST); };
        t += (next) => { Debug.Log(next);  Debug.Log(next); };
        t("Test");
        */
        foreach (var itemGrid in itemGrids.Where(ig => ig!=null).Select(ig=>ig.gameObject))
        {
            Destroy(itemGrid);
        }
      
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Food))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void SwordDisplay()
    {
        foreach(var itemgrid in itemGrids.Where(ig => ig!= null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
      
      
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Sword))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        } 
        
    }

    public void SpearDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Spear))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void LargeSwordDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.LargeSword))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);         
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void AxeDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Axe))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void HammerDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        } 
      
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Hammer))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void KnifeDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Knife))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void RodDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Rod))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void WhipDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Whip))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void NailDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Nail))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void BowDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Bow))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void ClubDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
        
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Club))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void FanDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Fan))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void BoomerangDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Boomerang))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void HeadDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
        
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Head))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void UpperBodyDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
   
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.UpperBody))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void LowerBodyDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
        
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.LowerBody))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void ArmDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Arm))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
    public void ShieldDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
     
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Shield))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void AccessoriesDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
      
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.Accessories))
        {
            GameObject itemGridClone = Instantiate(itemGrid, this.transform);
            itemGrids.Add(itemGridClone);
            Image itemImage = itemGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            itemImage.sprite = allitem.MyItemImage;
            itemImage.color = Color.white;
            itemGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicFireDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
      
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.fireMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;           
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }

    }

    public void MagicIceDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.iceMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicWindDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.windMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicThunderDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
        
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.thunderMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicHolyDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.holyMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicDarkDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.darkMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicRecoveryDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.recoveryMagic))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }

    public void MagicLastResortDisplay()
    {
        foreach (var itemgrid in itemGrids.Where(ig => ig != null).Select(ig => ig.gameObject))
        {
            Destroy(itemgrid);
        }
       
        foreach (var allitem in SaveSystem.Instance.UserData.allItems.Where(ai => ai.itemType == Item.Type.lastResort))
        {
            GameObject magicGridClone = Instantiate(magicGrid, this.transform);
            itemGrids.Add(magicGridClone);
            Image magicImage = magicGridClone.GetComponentInChildren<Button>().GetComponent<Image>();
            magicImage.sprite = allitem.MyItemImage;
            magicImage.color = Color.white;
            magicGridClone.GetComponentInChildren<Text>().text = allitem.MyItemname;
        }
    }
}
