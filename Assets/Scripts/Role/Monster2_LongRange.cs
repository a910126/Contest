using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2_LongRange : RoleBase
{
    protected override void Awake()
    {
        this.Movespeed = 10;
        this.MonsterRange = 10;
        this.DisToAtk = 8;
        this.DisToBack = 20;
        this.AtkTime = 1.5f;
        this.BornPos = new Vector3(0, 1, 0);
        base.Awake();
        Ai = new AiLogic(this);
    }
    public override void Atk()
    {
        CreatePrefab("Monster2_Bullet", this.gameObject.transform.position+Vector3.left,this.transform.rotation);
    }

    public override void Hurt()
    {
        
    }
}
