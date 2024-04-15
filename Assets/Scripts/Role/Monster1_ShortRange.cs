using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1_ShortRange : RoleBase
{
    private AiLogic Ai;

    void Awake()
    {
        Ai = new AiLogic(this as RoleBase);
    }
    void Start()
    {
        Atk();
    }

    void Update()
    {
        Ai.UpdateState();
    }
    public override void Atk()
    {    
        //int temp = Random.Range(0,101);
        //print(temp);
        //if (temp < 75)
        //{
        //    Atk1();
        //}
        //else
        //{
        //    Atk2();
        //}
        Atk2();
    }

    public override void Hurt()
    {
        
    }

    protected override void Move()
    {
        
    }


    private void Atk1()  //攻击方式1 点式攻击
    {
        //animation.Play();
        Collider[] collider = Physics.OverlapBox(this.gameObject.transform.position + this.gameObject.transform.forward, new Vector3(1, 1, 1), Quaternion.identity, 1 << LayerMask.NameToLayer("Player"));
        for(int i = 0; i < collider.Length; i++)       
        {
            print(collider[i].gameObject.name);
        }   
    }

    private void Atk2()  //攻击方式2 蓄力攻击
    {
       
        ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/ExclamationMark", (obj) =>
        {
            obj.transform.position=this.transform.position+this.transform.up;
        });
        StartCoroutine("AccumulatePower");
    }

    private IEnumerator AccumulatePower()  //蓄力攻击
    {
        yield return new WaitForSeconds(1);
        ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Laser", (obj) =>
        {
            obj.transform.position = this.gameObject.transform.position - this.gameObject.transform.right;
        });
    }

}
