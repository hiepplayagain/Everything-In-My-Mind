using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{

    //All activities will be do when entering this state
    public abstract void EnterState(BehaviourManagement _movingState);

    //Conditions to change other states
    public abstract void UpdateState(BehaviourManagement _movingState);

    

}
