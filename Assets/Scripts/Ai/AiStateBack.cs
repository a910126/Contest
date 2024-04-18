using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateBack : AiStateBase
{
    public AiStateBack(AiLogic logic) : base(logic)
    {
    }
    public override void EnterAiState()
    {
        //logic.monster.StopMove();
        Debug.Log("进入返回状态");
        logic.monster.Move(logic.monster.transform.position,logic.monster.BornPos);
    }

    public override void ExitAiState()
    {
        logic.monster.StopMove();
    }

    public override void UpdateAiState()
    {
        IsBornPos();
    }

    private void IsBornPos()  //是否回到初始点
    {
        float temp = Vector3.Distance(logic.monster.transform.position, logic.monster.BornPos);
        //Debug.Log(temp+"距离");
        if (temp <= 0.08f)
        {
            logic.ChangeState(E_State.PATROL);
        }
    }

}
