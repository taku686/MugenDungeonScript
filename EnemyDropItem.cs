using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
   
    [SerializeField] [Range(0, 1)] private float dropRate;

    [SerializeField] private int number = 1; //ドロップアイテムをいくつ作るか

    [SerializeField] GameObject[] itemIconPrefabs;

  


    public void DropIfNeeded(Vector3 dropPosition)
    {

        if (Random.Range(0, 1f) >= (dropRate + (PlayerStatus.instance.Luck(SaveSystem.Instance.UserData.job)/100))) return;

        for (int i = 0; i < number; i++)
        {
            GameObject randomChoice = itemIconPrefabs[Random.Range(0, itemIconPrefabs.Length)];
            Instantiate(randomChoice, dropPosition, Quaternion.identity);
        }
    }

   

}
