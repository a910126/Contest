using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiStateBase
{
    protected AiLogic logic;

    public AiStateBase(AiLogic logic)
    {
        this.logic = logic;
    }

    public abstract void EnterAiState();  //刚进入处理逻辑
    public abstract void UpdateAiState();  //处理逻辑
    public abstract void ExitAiState();  //退出处理逻辑
}
