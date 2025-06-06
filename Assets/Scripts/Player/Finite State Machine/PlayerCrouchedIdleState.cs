using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchedIdleState : PlayerBaseState
{
    public override void EnterState(BehaviourManagement _movingState)
    {
        _movingState._anim.SetBool("isCrouching", true);
        _movingState._currentSpeed = 0f;
    }

    public override void UpdateState(BehaviourManagement _movingState)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) ExitState(_movingState, _movingState._playerRunState);
        else if (Input.GetKeyUp(KeyCode.LeftControl)) ExitState(_movingState, _movingState._playerWalkState);
        //else if (_movingState._inputMagnitude > 0.1f) ExitState(_movingState,_movingState._playerCrouchedWalkState);
        
    }

    void ExitState(BehaviourManagement _movingState, PlayerBaseState _state)
    {
        _movingState._anim.SetBool("isCrouching", false);
        _movingState.SwitchState(_state);
    }

    
}
