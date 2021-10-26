using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoExit : MonoBehaviour
{
    public void GoToLobby()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }
}
