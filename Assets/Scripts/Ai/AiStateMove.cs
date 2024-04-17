using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMove : AiStateBase
{
    public AiStateMove(AiLogic logic) : base(logic)
    {
    }

    public override void EnterAiState()
    {
        Debug.Log("进入追击状态");
        
    }

    public override void ExitAiState()
    {
        logic.monster.StopMove();
    }

    public override void UpdateAiState()
    {
        logic.monster.Move(logic.monster.transform.position, logic.monster.PlayerPos);
        PrepareAtk();
        PrepareBack();
    }

    private void PrepareAtk()  //距离Player指定距离停止切换攻击
    {
        float temp = Vector3.Distance(logic.monster.transform.position, logic.monster.PlayerPos);
        if (temp <= logic.monster.DisToAtk)
        {  
            logic.ChangeState(E_State.ATK);
        }
    }
    private void PrepareBack()  //距离BornPos指定距离回到BornPos
    {
        float temp = Vector3.Distance(logic.monster.transform.position, logic.monster.BornPos);
        Debug.Log(temp + "距离");
        if (temp <= logic.monster.DisToBack)
        {
            Debug.Log("回去");
            logic.monster.Move(logic.monster.transform.position, logic.monster.BornPos);
            
        }
    }
}
