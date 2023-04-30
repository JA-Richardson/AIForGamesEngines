using System.Collections.Generic;

namespace BehaviorTree
{
    // Define an enumeration for the possible states of a node.
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    // Define the Node class, which represents a node in a behavior tree.
    public class Node
    {
        // The current state of the node.
        protected NodeState state;

        // The parent of the node.
        public Node parent;

        // The children of the node.
        protected List<Node> children = new List<Node>();

        // The data context of the node, which stores key-value pairs.
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        // Constructors.
        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            // Attach each child node to this node.
            foreach (Node child in children)
                _Attach(child);
        }

        // A private method to attach a child node to this node.
        private void _Attach(Node node)
        {
            // Set the parent of the child node to this node, and add it to the children list.
            node.parent = this;
            children.Add(node);
        }

        // Evaluate method, which should be implemented by subclasses.
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        // Set data in the data context of the node.
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        // Get data from the data context of the node.
        public object GetData(string key)
        {
            object value = null;
            // Try to get the value from the data context of this node.
            if (_dataContext.TryGetValue(key, out value))
                return value;

            // If the value is not found in the data context of this node,
            // search for it in the data context of the parent nodes.
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        // Clear data from the data context of the node.
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}