using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StatusScene : MonoBehaviour
{
    [SerializeField] private Text statusTitle;
    [SerializeField] private Text levelText;
    [SerializeField] private Text hpText;
    [SerializeField] private Text mpText;
    [SerializeField] private Text phyAtkText;
    [SerializeField] private Text phyDefText;
    [SerializeField] private Text magAtkText;
    [SerializeField] private Text magDefText;
    [SerializeField] private Text luckText;
    [SerializeField] private Text evasionText;
    [SerializeField] private Text nextLvExp;
   
    public void TextChange()
    {
        statusTitle.text = "ステータス(" + SaveSystem.Instance.UserData.job + ")";
        levelText.text = "Lv : " + PlayerStatus.instance.Level();
        hpText.text = "HP : " + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        mpText.text = "MP : " + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        phyAtkText.text = "物理攻撃力 : " + PlayerStatus.instance.PhyAtk(SaveSystem.Instance.UserData.job);
        phyDefText.text = "物理防御力 : " + PlayerStatus.instance.PhyDef(SaveSystem.Instance.UserData.job);
        magAtkText.text = "魔法攻撃力 : " + PlayerStatus.instance.MagAtk(SaveSystem.Instance.UserData.job);
        magDefText.text = "魔法防御力 : " + PlayerStatus.instance.MagDef(SaveSystem.Instance.UserData.job);
        luckText.text = "運 : " + PlayerStatus.instance.Luck(SaveSystem.Instance.UserData.job);
        evasionText.text = "回避 : " + PlayerStatus.instance.Evasion(SaveSystem.Instance.UserData.job);
        nextLvExp.text = SaveSystem.Instance.UserData.GetExp().ToString();
    }
}
