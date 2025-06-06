using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(BehaviourManagement _movingState)
    {
        _movingState._currentSpeed = 0f;
        
    }

    public override void UpdateState(BehaviourManagement _movingState)
    {
        if (_movingState._inputMagnitude > 0.1f)
        {
            _movingState.SwitchState(_movingState._playerWalkState);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) _movingState.SwitchState(_movingState._playerRunState);
        else if (Input.GetKey(KeyCode.LeftControl)) _movingState.SwitchState(_movingState._playerCrouchedIdleState);
    }

    

}
