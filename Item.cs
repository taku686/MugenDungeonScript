using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName ="Items",menuName ="Items/item")]
public class Item: ScriptableObject
{
    [SerializeField] private string itemname;
    [SerializeField] public bool isEquipented;
    [SerializeField] private Sprite itemImage;
    [SerializeField] public Type itemType ;
    [SerializeField] public ItemClass itemClass;
    [SerializeField] public ItemStatus itemStatus;
    [SerializeField] public Attribute itemAttribute;
    [SerializeField, Header("エフェクト")] public GameObject effectPrefab;
    [SerializeField, Header("ダメージ")] public Damage damage;
    [SerializeField, Header("攻撃範囲")] public int range;
    [SerializeField, Header("消費ＭＰ")] public int consumedMP;
    [SerializeField, Header("タイプ")] public MagicType magicType;
    [SerializeField, Header("説明")] public string explanation;
    [SerializeField, Header("SE")] public AudioClip se;
    public Sprite MyItemImage { get => itemImage; }
    public string MyItemname { get => itemname; }

    public enum ItemClass
    {
        armor,
        weapon,
        food,
        magic
    }
  
    public enum Type
    {
        Head,
        UpperBody,
        LowerBody,
        Arm,
        Shield,
        Accessories,
        LargeSword,
        Sword,
        Spear,
        Axe,
        Hammer,
        Knife,
        Rod,
        Whip,
        Nail,
        Bow,
        Club,
        Fan,
        Boomerang,
        Food,
        fireMagic,
        iceMagic,
        windMagic,
        thunderMagic,
        holyMagic,
        darkMagic,
        recoveryMagic,
        lastResort
    }

    public enum Attribute
    {
        Nothing,
        Fire,
        Ice,
        Thunder,
        Dark,
        Holy,
        Wind,
        Recovery,
        Ground,
        LastResort,
        Other
    }

    public enum MagicType
    {
        AllAttack,
        RangeAttack,
        DirectAttack,
        Recovery,
        Sumon
    }

    public enum Damage
    {
        Small,
        Middle,
        Big,
        SpecialBig,
        AbsoluteBig
    }
}
