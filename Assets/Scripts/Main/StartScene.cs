using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
    }
    public override void Clear()
    {
    }
    private void Update() 
    {
        if (Input.anyKey)
            Managers.Scene.LoadScene(Define.Scene.Lobby); 
    }
}
