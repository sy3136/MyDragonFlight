using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
        Bullet,
        BackGround,
        Warn,
        Item,
    }
    public enum State
    {
        Die,
        Moving,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }
}
