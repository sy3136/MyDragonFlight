using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayerHp : UI_Base
{
    GameObject _hpUI;
    PlayerStat _playerStat;
    string spriteText = "<sprite=0>";
    // Start is called before the first frame update
    public override void Init()
    {
        _hpUI = GameObject.Find("HpUI");
        _playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        for (int i = 0; i < _playerStat.MaxHp; i++)
            _hpUI.GetComponent<TMP_Text>().text += spriteText;

    }

    // Update is called once per frame
    private void Update()
    {
        _hpUI.GetComponent<TMP_Text>().text = "";

        for (int i = 0; i < _playerStat.Hp; i++)
            _hpUI.GetComponent<TMP_Text>().text += spriteText;

    }

}
