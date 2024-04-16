using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStatePatrol : AiStateBase
{
    public AiStatePatrol(AiLogic logic) : base(logic)
    {

     }

    public override void EnterAiState()
    {
        Debug.Log("巡逻");
    }

    public override void ExitAiState()
    {
        
    }

    public override void UpdateAiState()
    {
        //logic.monster.Move(logic.monster.BornPos, logic.monster.Patrol1);
        logic.monster.Move(logic.monster.BornPos, logic.monster.Patrol1);
    }

}
