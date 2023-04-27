using BehaviorTree;
using UnityEditor.PackageManager;
using UnityEngine;

public class CheckBattery : Node
{
    bool batteryDead = false;

    public override NodeState Evaluate()
    {
        if (GameManager.Instance.DroneBattery >= 100)
        {
            batteryDead = false;
        }
        if (GameManager.Instance.DroneBattery < 5.0f)
        {
            batteryDead = true;
            state = NodeState.SUCCESS;
            return state;
        }
        else if(GameManager.Instance.DroneBattery >= 5.0f && !batteryDead)
        {
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}
