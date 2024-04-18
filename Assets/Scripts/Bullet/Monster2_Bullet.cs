using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2_Bullet : BulletBase
{
    protected override void Awake()
    {
        base.Awake();
        this.Speed = 3;
        this.DestoryTime = 2;
    }
}
