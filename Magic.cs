using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
  
    [SerializeField] GameObject selectRangeBlock;
    [SerializeField] MagicSetManager magicSetManager1;
    [SerializeField] MagicSetManager magicSetManager2;
    [SerializeField] MagicSetManager magicSetManager3;
    [SerializeField] MagicSetManager magicSetManager4;

    public List<GameObject> selectRangeBlocks = new List<GameObject>();
   public Vector3 currentMousePosition;
   public Vector3 worldMousePosition;
    private void Start()
    {
     //  SaveSystem.Instance.UserData.allItems.Clear();
     //  SaveSystem.Instance.UserData.equipmentItems.Clear();
     //  SaveSystem.Instance.Save();
     //  SaveSystem.Instance.UserData.setMagic1 = null;
     //  SaveSystem.Instance.UserData.setMagic2 = null;
     //  SaveSystem.Instance.UserData.setMagic3 = null;
     //   SaveSystem.Instance.UserData.setMagic4 = null;
     //   SaveSystem.Instance.Save();
    }

    private void Update()
    {
        if (magicSetManager1.isMagicActive||magicSetManager2.isMagicActive || magicSetManager3.isMagicActive || magicSetManager4.isMagicActive)
        {
            currentMousePosition = Input.mousePosition;
            worldMousePosition = Camera.main.ScreenToWorldPoint(currentMousePosition);
        }
    }
   
    public void MagicAllAttack(Item setMagic, Vector3 playerPos)
    {
        for (int i = 0; i < selectRangeBlocks.Count; i++)
        {
            if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 1)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 2)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 3)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 4)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 5)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 6)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
            else if (Vector3.Distance(playerPos, selectRangeBlocks[i].transform.position) < 7)
            {
                Instantiate(setMagic.effectPrefab, selectRangeBlocks[i].transform.position, Quaternion.identity);
            }
        }
    }

    public void SelectRange(Vector3 playerPos, Item setMagic)
    {
        if (setMagic == null) return;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (i + j < 1)
                {
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y + j, 0), Quaternion.identity));
                }
                if (1 <= i + j && i + j < setMagic.range && (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6) && j != 0)
                {
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y + j, 0), Quaternion.identity));
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x - i, playerPos.y + j, 0), Quaternion.identity));
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y - j, 0), Quaternion.identity));
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x - i, playerPos.y - j, 0), Quaternion.identity));
                }
                if (1 <= i + j && i + j < setMagic.range && (i == 0))
                {
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y + j, 0), Quaternion.identity));
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y - j, 0), Quaternion.identity));
                }
                if (1 <= i + j && i + j < setMagic.range && (j == 0))
                {
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x - i, playerPos.y + j, 0), Quaternion.identity));
                    selectRangeBlocks.Add(Instantiate(selectRangeBlock, new Vector3(playerPos.x + i, playerPos.y + j, 0), Quaternion.identity));
                }

            }
        }
    }
}
