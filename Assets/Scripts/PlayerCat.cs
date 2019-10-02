using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCat : PlayerBase
{
    public new void Awake()
    {
        base.Awake();
        speed = 5;
        health = 5;
        luck = 5;
        curHealth = health;
    }

}
