using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : BulletBase
{
    protected override void Awake()
    {
        base.Awake();
        this.Speed = 15f; 
        this.Damage = 20;
        this.DestoryTime = 5;
}
}
