using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(BehaviourManagement _movingState)
    {
        _movingState._currentSpeed = 5f;
        
    }

    public override void UpdateState(BehaviourManagement _movingState)
    {
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) ExitState(_movingState, _movingState._playerRunState);

        else if (Input.GetKey(KeyCode.LeftControl)) ExitState(_movingState, _movingState._playerCrouchedIdleState);

        else if (_movingState._inputMagnitude < 0.1f) ExitState(_movingState, _movingState._playerIdleState);
    }

    void ExitState(BehaviourManagement _movingState, PlayerBaseState _state)
    {
        _movingState._currentSpeed = 0f;
        _movingState.SwitchState(_state);

    }
}
