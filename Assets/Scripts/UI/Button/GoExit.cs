using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GoExit : MonoBehaviour
{
    public void GoToLobby()
    {
        Managers.Sound.Play("ui_button");
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
    
}
