using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SelectJob : MonoBehaviour
{
    public void Job1()
    {
        SaveSystem.Instance.UserData.job = "戦士";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }

    public void Job2()
    {
        SaveSystem.Instance.UserData.job = "武闘家";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }
    public void Job3()
    {
        SaveSystem.Instance.UserData.job = "魔法使い";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }
    public void Job4()
    {
        SaveSystem.Instance.UserData.job = "僧侶";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }
    public void Job5()
    {
        SaveSystem.Instance.UserData.job = "遊び人";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }
    public void Job6()
    {
        SaveSystem.Instance.UserData.job = "盗賊";
        SaveSystem.Instance.Save();
        SaveSystem.Instance.UserData.playerHP = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.UserData.playerMP = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        SaveSystem.Instance.Save();
        GManager.instance.hpSlider.value = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.hpSlider.maxValue = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.maxValue = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerHPText.text = PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.HP(SaveSystem.Instance.UserData.job);
        GManager.instance.mpSlider.value = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);
        GManager.instance.playerMPText.text = PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job) + "/" + PlayerStatus.instance.MP(SaveSystem.Instance.UserData.job);

    }
}
