using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData : MonoBehaviour
{
  
}
[System.Serializable]
public struct ItemStatus
{
    [SerializeField, Header("ＨＰ")] public int hp;
    [SerializeField, Header("ＭＰ")] public int mp;
    [SerializeField, Header("物理攻撃")] public int physicsAtk;
    [SerializeField, Header("物理防御")] public int physicsDef;
    [SerializeField, Header("魔法攻撃")] public int magicAtk;
    [SerializeField, Header("魔法防御")] public int magicDef;
    [SerializeField, Header("回避")] public float evasion;
    [SerializeField, Header("運")] public float luck;
    [SerializeField, Header("会心")] public float critical;
    [SerializeField, Header("会心ガード")] public float justGuard;
}

[System.Serializable]
public struct InitialPlayerStatus
{
    [SerializeField, Header("ＨＰ")] public int hp;
    [SerializeField, Header("ＭＰ")] public int mp;
    [SerializeField, Header("物理攻撃")] public int physicsAtk;
    [SerializeField, Header("物理防御")] public int physicsDef;
    [SerializeField, Header("魔法攻撃")] public int magicAtk;
    [SerializeField, Header("魔法防御")] public int magicDef;
    [SerializeField, Header("回避")] public float evasion;
    [SerializeField, Header("運")] public float luck;
    [SerializeField, Header("会心")] public float critical;
    [SerializeField, Header("会心ガード")] public float justGuard;
}
[System.Serializable]
public struct EnemyStatus
{
    [SerializeField, Header("ＨＰ")] public int hp;
    [SerializeField, Header("ＭＰ")] public int mp;
    [SerializeField, Header("物理攻撃")] public int physicsAtk;
    [SerializeField, Header("物理防御")] public int physicsDef;
    [SerializeField, Header("魔法攻撃")] public int magicAtk;
    [SerializeField, Header("魔法防御")] public int magicDef;
    [SerializeField, Header("回避")] public float evasion;
    [SerializeField, Header("運")] public float luck;
    [SerializeField, Header("会心")] public float critical;
    [SerializeField, Header("会心ガード")] public float justGuard;
    [SerializeField, Header("経験値")] public int experience;
    [SerializeField, Header("属性")] public Attribute attribute;
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
}