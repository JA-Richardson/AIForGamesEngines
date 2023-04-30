using BehaviorTree; // Import the BehaviorTree namespace

public class CheckBattery : Node // Inherit from the Node class
{
    bool batteryDead = false; // Initialize a boolean variable to keep track of whether the battery is dead or not

    public override NodeState Evaluate() // Override the Evaluate() method from the Node class
    {
        if (GameManager.Instance.DroneBattery >= 100) // If the drone battery level is greater than or equal to 100
        {
            batteryDead = false; // Set the batteryDead flag to false
        }
        if (GameManager.Instance.DroneBattery < 5.0f) // If the drone battery level is less than 5.0
        {
            batteryDead = true; // Set the batteryDead flag to true
            state = NodeState.SUCCESS; // Set the state to SUCCESS, indicating that the battery is dead
            return state; // Return the current state
        }
        else if (GameManager.Instance.DroneBattery >= 5.0f && !batteryDead) // If the drone battery level is greater than or equal to 5.0 and the batteryDead flag is false
        {
            state = NodeState.FAILURE; // Set the state to FAILURE, indicating that the battery is not dead
            return state; // Return the current state
        }
        state = NodeState.SUCCESS; // Set the state to SUCCESS, indicating that the battery is not dead
        return state; // Return the current state
    }
}