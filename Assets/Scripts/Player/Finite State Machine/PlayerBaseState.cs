using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(BehaviourManagement _movingState);

    public abstract void UpdateState(BehaviourManagement _movingState);

    

}
