using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base() { } // Constructor that calls the base class's constructor with no arguments
        public Selector(List<Node> children) : base(children) { } // Constructor that calls the base class's constructor with a list of child nodes

        public override NodeState Evaluate() // Method that evaluates the selector node
        {
            foreach (Node node in children) // Loop through all the child nodes
            {
                switch (node.Evaluate()) // Evaluate the child node and switch based on its state
                {
                    case NodeState.FAILURE: // If the child node returns failure, move on to the next child
                        continue;
                    case NodeState.SUCCESS: // If the child node returns success, return success and stop evaluating the rest of the children
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING: // If the child node returns running, return running and stop evaluating the rest of the children
                        state = NodeState.RUNNING;
                        return state;
                    default: // If the child node returns an unknown state, move on to the next child
                        continue;
                }
            }

            state = NodeState.FAILURE; // If none of the child nodes returned success or running, return failure
            return state;
        }
    }
}