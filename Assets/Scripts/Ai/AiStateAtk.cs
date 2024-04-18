using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateAtk : AiStateBase
{
    private float AtkTime=2;  //攻击间隔时间
    private float NextAtk = 0;  //下次攻击时间

    public AiStateAtk(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        Debug.Log("进入攻击状态");
    }

    public override void ExitAiState()
    {
        
    }

    public override void UpdateAiState()
    {
            AtkForward();
            
    }

    private void AtkForward()  //攻击间隔
    {
        if (Time.time >= NextAtk)
        {
            logic.monster.Atk();
            NextAtk = Time.time + AtkTime;
        }
        OutOfRange();
    }

    private void OutOfRange()  //超出攻击范围
    {
        float temp = Vector3.Distance(logic.monster.transform.position, logic.monster.PlayerPos);
        if (temp >= logic.monster.DisToAtk)
            MonoMgr.GetInstance().StartCoroutine(WaitTime());
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        logic.ChangeState(E_State.MOVE);
    }

}
