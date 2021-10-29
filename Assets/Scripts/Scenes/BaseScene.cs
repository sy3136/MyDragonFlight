using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Time.timeScale = 1.0f;
        Screen.SetResolution(540, 960, false);
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
    public abstract void Clear();
}
