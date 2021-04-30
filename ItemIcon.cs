using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] private Item[] itemList;
    private Item choiceItem;
   
    
   

   

    public void Initialize()
    {

        var colliderCache = GetComponent<Collider2D>();
        colliderCache.enabled = false;

        var transformCache = transform;
        var dropPosition = transform.localPosition;
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.localScale = Vector3.zero;
        transformCache.DOScale(defaultScale, 0.5f)
        .SetEase(Ease.OutBounce)
        .OnComplete(() =>
        {
            colliderCache.enabled = true;
        });

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           

            ItemAdd();
            Destroy(this.gameObject);
          
        }

       
    }

    public void ItemAdd()
    {
       
        int randomIndex = Random.Range(0, itemList.Length);
        choiceItem = itemList[randomIndex];
        SaveSystem.Instance.UserData.allItems.Add(choiceItem);
        SaveSystem.Instance.Save();
        GameObject.Find("Canvas").GetComponent<Menu>().ShowGetItem(choiceItem);  
    }

  

  



}
