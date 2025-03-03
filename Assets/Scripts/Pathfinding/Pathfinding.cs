using NUnit.Framework;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }

    [Header("Pathfinding Settings")]
    [SerializeField] private Grid _grid;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 FindPath(Vector2 startPos, Vector2 targetPos)
    {
        // Change the vector position into node position
        Node startNode = _grid.GetNodeFromWorldPoint(startPos);
        Node targetNode = _grid.GetNodeFromWorldPoint(targetPos);

        // Creates a list of elements to check (openSet) and a list of elements already check (closedSet)
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        // We start with the startNode as the first Node
        openSet.Add(startNode);

        // Loop through the openSet because it contains all node to look at
        while(openSet.Count > 0)
        {
            // Create a variable to keep track of which node we are on and it's going to be our startNode 
            // Because we set it to our first element of the list
            Node currentNode = openSet[0];

            // Start from 1 because 0 is currently our current node 
            for (int i = 1; i < openSet.Count; i++)
            {
                // Check if the fCost of the next node is lower or is equal
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost)
                {
                    // After the first check is true then we check the hCost (distance between current and target)
                    // if the hCost of the next node is lower than the current (it means the next node is closer from the target)
                    if(openSet[i].hCost < currentNode.hCost)
                    {
                        // we assign our current node to the next node in the list
                        currentNode = openSet[i];
                    }
                }
            }

            // Remove our currentNode from the list of things that have to be checked each time to add it to the list of already checked node
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);


            // If we find the target then we call a function to retrace the step in reverse (it creates the path)
            if(currentNode == targetNode)
            {
                // Go to the function to see what it does
                return RetracePath(startNode, targetNode);
            }

            // Get the neighbor of the node
            foreach (Node neighbor in _grid.GetNeighbors(currentNode))
            {
                // If we can't walk on the node or we already check it then we continue de function
                if(!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }
                
                // Calculate the next gCost by additioning the current node and the distance between the current node and the neighbor
                // The gCost (distance with the startNode) alway increase because we go further and further from it
                int newMovementCostToNeighbor = currentNode.gCost + GetDistanceNode(currentNode, neighbor);

                // If the next gCost is lower than the current gCost (it means we can go on it because it's suppose to be lower)
                // or the neighbor isn't in the openSet list because if it is then it's already currently checking it probably
                if (newMovementCostToNeighbor < currentNode.gCost || !openSet.Contains(neighbor))
                {
                    // Then the neighbor take the new g cost calculate 
                    // Is hCost is the distance between himself and the targetNode
                    // And we assign his parent to the currentNode (like this we know from who the node came, it help retrace the path)
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistanceNode(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    // If neighbor isn't in the openSet list then we add it
                    if(!openSet.Contains(neighbor))
                    {
                        // We finally add the neighbor to the openSet list which will be treated in loop
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return startPos;
    }

    // This function get the path when the algorithm is finished 
    private Vector2 RetracePath(Node startNode, Node endNode)
    {
        // We create a path list which will regroup all the node that creates the path 
        // It starts from the endNode (currentNode = endNode)
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        // And while it's not at the startNode (starting from the endNode)
        while (currentNode != startNode)
        {
            // We add the currentNode to the list 
            // And we change the node with the parent (he keeps track of which node was before him)
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse(); // we also reverse the list because we want it to go from startToEnd 
        _grid.Paths.Add(path); // this is just for the grid script to add a color to those specific node

        return path[0].worldPosition;
    }

    // Get the distance between two node 
    private int GetDistanceNode(Node nodeA, Node nodeB)
    {
        // Calculate the difference between the position X and Y in node
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        // If depending on which distance is greater we reverse substraction because we don't want negative number
        if(distanceX > distanceY)
        {
            // 10 for the number up, left, right, down way (because it's 1 * 10) 
            // Substract the greater distance by the lower distance to get to our nodeB
            return 10 * (distanceX - distanceY);
        }

        return 10 * (distanceY - distanceX);
    }

    public Vector3 GetRandomNeighbor(Vector3 position)
    {
        List<Node> neighborList = _grid.GetNeighbors(_grid.GetNodeFromWorldPoint(position));
        List<Node> walkableNeighbor = new List<Node>();

        foreach (Node node in neighborList)
        {
            if(node.isWalkable)
            {
                walkableNeighbor.Add(node);
            }
        }

        return walkableNeighbor[Random.Range(0, walkableNeighbor.Count)].worldPosition;
    }
}
