using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using UnityEngine.EventSystems;


public class SelectRangeBlockManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    RaycastHit2D hit;
    [SerializeField] LayerMask selectRangeLayer;
    [SerializeField] LayerMask blockingLayer;
    Vector3 playerPos;
    Item setMagic;
    Magic magicSc;
    public bool isAttack;

    private void Start()
    {
        magicSc = GameObject.Find("MagicSet").GetComponent<Magic>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform.position;
        foreach(var equipmentitem in SaveSystem.Instance.UserData.equipmentItems.Where(ei => ei.isEquipented== true&&ei.itemClass == Item.ItemClass.magic))
        {
            setMagic = equipmentitem;
        }
    }

    private void Update()
    {
        if (!isAttack)
        {
            ColorChange();
        }
        if (Input.GetMouseButtonDown(0))
        {
            isAttack = true;
        }
        
    }

    public void ColorChange()
    {
        hit = Physics2D.BoxCast(magicSc.worldMousePosition, new Vector2(0.1f, 0.1f), 0, Vector2.zero, selectRangeLayer);
    }

    private void OnMouseOver()
    {
        if (hit.transform != null)
        {
            if (hit.transform.tag == "SelectRangeButton")
            {
                hit.transform.GetComponent<SpriteRenderer>().color = new Color(0, 5, 255, 0.5f);
            }
        }
    }

    private void OnMouseExit()
    {
        if (hit.transform != null)
        {
            if (hit.transform.tag == "SelectRangeButton")
            {
                hit.transform.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
            }
        }
    }

  




}
