using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Node // Define Sequence class that inherits from Node class
    {
        public Sequence() : base() { } // Constructor with no arguments that calls base constructor
        public Sequence(List<Node> children) : base(children) { } // Constructor that takes a list of child nodes and calls base constructor with that list

        public override NodeState Evaluate() // Override Evaluate method from Node class
        {
            bool anyChildIsRunning = false; // Flag to keep track if any child node is still running

            foreach (Node node in children) // Loop through all child nodes
            {
                switch (node.Evaluate()) // Evaluate the current child node and switch based on its return value
                {
                    case NodeState.FAILURE: // If child fails, return failure
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS: // If child succeeds, move on to next child
                        continue;
                    case NodeState.RUNNING: // If child is still running, set flag to true and move on to next child
                        anyChildIsRunning = true;
                        continue;
                    default: // If child returns an unknown state, return success
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            // If loop finishes without returning, that means all child nodes have been evaluated
            // If any child is still running, return running; otherwise, return success
            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }
    }
}