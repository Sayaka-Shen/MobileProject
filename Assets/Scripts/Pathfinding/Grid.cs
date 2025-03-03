using System;
using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    [Header("Grid Settings")] 
    [SerializeField] private LayerMask _unwalkableMask;
    
    // Total size of the grid in which will be contain node
    [SerializeField] private Vector2 _gridWorldSize;
    [SerializeField] private float _nodeRadius;
    private Node[,] _grid;
    private float _nodeDiameter;

    // Size of each node, size of the array (array of X number of nodes)
    private int _gridSizeX, _gridSizeY;

    public List<List<Node>> Paths { get; set; }
    private List<Color> _colorsPath = new List<Color>();
 
    private void Start()
    {
        // Calculate the diameter using the radius
        _nodeDiameter = _nodeRadius * 2;

        // And calculate the number of square you can put in my grid
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

        Paths = new List<List<Node>>();
        for (int i = 0; i < 99; i++)
        {
            _colorsPath.Add(UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
        }

        CreateGrid();
    }

    private void CreateGrid()
    {
        // Creates a grid the size of X and Y depending on the value
        // Like a chess board with a 2 dimensional array of X size and Y size
        _grid = new Node[_gridSizeX, _gridSizeY];

        // Take the half of the grid in X and Y (Vector2.right * _gridWorldSize.x / 2 && Vector2.up * _gridWorldSize.y / 2) 
        // And substract it from the transform.position which will take it at the bottomLeft
        Vector2 worldBottomLeft = (Vector2)transform.position - Vector2.right * _gridWorldSize.x / 2 - Vector2.up * _gridWorldSize.y / 2;


        // Loop in the two dimensional array
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                // Get the middle x and middle y (Vector2.right * (x * _nodeDiameter + _nodeRadius) + Vector2.up * (y * _nodeDiameter + _nodeRadius)
                // Then we add to the bottomLeft to create a middle point
                Vector3 worldPoint = worldBottomLeft + Vector2.right * (x * _nodeDiameter + _nodeRadius) + Vector2.up * (y * _nodeDiameter + _nodeRadius);

                // Check from the world point in the area of the node diameter minus a little space, the collider with the unwalkableMask
                // if it's true (it has find the unwalkable mask) it will be reverse so walkable will be false 
                bool walkable = !(Physics2D.OverlapBox(worldPoint, new Vector2(_nodeDiameter - .1f, _nodeDiameter - .1f), 0, _unwalkableMask));

                // Then assign at position x and y in the array a new node with the walkable parameter and the worldPoint of the node
                _grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public void LoadGrid()
    {
        UpdateGrid();
        GameManager.Instance.MovementPlayer.OnStartMove += ResetPathsGizmos;
        GameManager.Instance.MovementPlayer.OnStartMove += UpdateGrid;
    }

    private void UpdateGrid()
    {
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                _grid[x, y].isWalkable = !(Physics2D.OverlapBox(_grid[x, y].worldPosition, new Vector2(_nodeDiameter - .1f, _nodeDiameter - .1f), 0, _unwalkableMask));
            }
        }
    }

    // This function get the neighbor of a node
    public List<Node> GetNeighbors(Node startingNode)
    {
        // First create a list to store them
        List<Node> neighbors = new List<Node>();

        // Create a 3 by 3 square around the node (between -1 and 1)
        // -1  0 -1
        //  0  0  0
        //  1  0  1
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // if it's x = 0 and y = 0 THEN it's the center and if it's not 0 it's diagonal so we can continue
                // if it's not we stop
                if((x == 0 && y == 0) || (x != 0 && y != 0))
                {
                    continue;
                }

                // CheckX and CheckY are adding to the node x and y to move the position in node to the neighors
                int checkX = startingNode.gridX + x;
                int checkY = startingNode.gridY + y;

                // Then if checkX and checkY are superior/equal 0 and in the gridSize 
                if((checkX >= 0 && checkX < _gridSizeX) && (checkY >= 0 && checkY < _gridSizeY))
                {
                    // We can add them to the neighors list with there position x and y
                    // We do this for each neighbors in the 3 by 3 square (except we don't take diagonal)
                    neighbors.Add(_grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    // This function calculates using a world position, the position of an element in Node
    public Node GetNodeFromWorldPoint(Vector2 worldPos)
    {
        // We find at which percentage in the grid the element is
        // Also clamp it between 0 and 1
        float percentX = (worldPos.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
        float percentY = (worldPos.y + _gridWorldSize.y / 2) / _gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Multiply the percentage by the gridSizePos to find the right node the element is on
        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        // Give back the position
        return _grid[x,y];
    }

    private void ResetPathsGizmos()
    {
        Paths.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, _gridWorldSize.y, 1));

        if (_grid != null)
        {
            foreach (Node n in _grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
            }
            if (Paths != null)
            {
                int i = 0;
                foreach (List<Node> path in Paths)
                {
                    foreach (Node n in path)
                    {
                        Gizmos.color = _colorsPath[i];

                        Gizmos.DrawCube(n.worldPosition, Vector3.one * (_nodeDiameter - .1f));
                    }
                    i++;
                }
            }
        }
    }
}
