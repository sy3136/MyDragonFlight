using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lobby_UIManager : MonoBehaviour
{
    public Image weapon;
    public GameObject _coin;
    int level = 1;
    int coin = 0;
    int[] price = {10, 20, 30, 40};

    private void Awake() {
        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
         if (PlayerPrefs.HasKey("Coin"))
            coin = PlayerPrefs.GetInt("Coin");
        _coin.GetComponent<TMP_Text>().text = coin.ToString();
        weapon.sprite = Managers.Resource.Load<Sprite>("Images/Weapons/bullet_"+level.ToString("00"));
    }

    public void UpgradeWeapon()
    {
        if (level<5)
        {
            coin -= price[level-1];
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("Level", ++level);
            _coin.GetComponent<TMP_Text>().text = coin.ToString();
            weapon.sprite = Managers.Resource.Load<Sprite>("Images/Weapons/bullet_"+level.ToString("00"));
        }
    }

    public void UndoUpgrade()
    {
        if (level>1)
        {
            coin += price[level-2];
            PlayerPrefs.SetInt("Coin", coin);
            PlayerPrefs.SetInt("Level", --level);
            _coin.GetComponent<TMP_Text>().text = coin.ToString();
            weapon.sprite = Managers.Resource.Load<Sprite>("Images/Weapons/bullet_"+level.ToString("00"));
        }
    }

    public void GameStart()
    {
        Managers.Sound.Play("ui_button");
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
