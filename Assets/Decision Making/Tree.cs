using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null; // The root node of the behavior tree

        protected void Start() // Unity callback function that is called when the behavior tree is started
        {
            _root = SetupTree(); // Call the abstract SetupTree() method to set up the behavior tree
        }

        private void Update() // Unity callback function that is called once per frame
        {
            if (_root != null) // Check if the root node is not null
                _root.Evaluate(); // Evaluate the root node
        }

        protected abstract Node SetupTree(); // Abstract method that must be implemented by subclasses to set up the behavior tree
    }
}