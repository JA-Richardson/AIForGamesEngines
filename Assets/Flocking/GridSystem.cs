// GridSystem.cs
using System.Collections.Generic;
using UnityEngine;

// The GridSystem class manages the spatial partitioning of boids in the
// flocking simulation, making it easier and more efficient to search for
// neighboring boids.
public class GridSystem : MonoBehaviour
{
    public Vector3 gridSize = new Vector3(100, 100, 100);
    public float cellSize = 5;
    public Vector3 gridPosition = Vector3.zero;
    private Dictionary<Vector3Int, List<Boid_Script>> grid;

    // Initialize the grid dictionary
    void Awake()
    {
        grid = new Dictionary<Vector3Int, List<Boid_Script>>();
    }

    // Add a boid to the grid
    public void AddBoid(Boid_Script boid)
    {
        Vector3Int cellCoords = WorldToCellCoords(boid.transform.position);
        if (!grid.ContainsKey(cellCoords))
        {
            grid[cellCoords] = new List<Boid_Script>();
        }
        grid[cellCoords].Add(boid);
    }

    // Remove a boid from the grid
    public void RemoveBoid(Boid_Script boid, Vector3 oldPosition)
    {
        Vector3Int cellCoords = WorldToCellCoords(oldPosition);
        if (grid.ContainsKey(cellCoords))
        {
            grid[cellCoords].Remove(boid);
        }
    }

    // Get a list of neighboring boids within a given radius
    public List<Boid_Script> GetNeighbors(Boid_Script boid, float radius)
    {
        Vector3Int cellCoords = WorldToCellCoords(boid.transform.position);
        List<Boid_Script> neighbors = new List<Boid_Script>();

        int searchRadius = Mathf.CeilToInt(radius / cellSize);

        for (int x = -searchRadius; x <= searchRadius; x++)
        {
            for (int y = -searchRadius; y <= searchRadius; y++)
            {
                for (int z = -searchRadius; z <= searchRadius; z++)
                {
                    Vector3Int offset = new Vector3Int(x, y, z);
                    float sqrOffsetMagnitude = offset.sqrMagnitude * cellSize * cellSize;
                    if (sqrOffsetMagnitude > radius * radius)
                    {
                        continue;
                    }

                    Vector3Int neighborCoords = cellCoords + offset;
                    if (grid.ContainsKey(neighborCoords))
                    {
                        foreach (Boid_Script neighborBoid in grid[neighborCoords])
                        {
                            if (neighborBoid != null)
                            {
                                float sqrDistance = (boid.transform.position - neighborBoid.transform.position).sqrMagnitude;
                                if (neighborBoid != boid && sqrDistance <= radius * radius)
                                {
                                    neighbors.Add(neighborBoid);
                                }
                            }
                        }
                    }
                }
            }
        }

        return neighbors;
    }

    // Draw the grid in the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector3 gridCenter = gridPosition;
        Gizmos.DrawWireCube(gridCenter, gridSize);

        if (grid == null)
        {
            return;
        }

        Gizmos.color = Color.white;
        Vector3 cellScale = new Vector3(cellSize, cellSize, cellSize);

        foreach (Vector3Int cellCoords in grid.Keys)
        {
            if (grid[cellCoords].Count > 0)
            {
                Vector3 cellCenter = CellCoordsToWorld(cellCoords) + cellScale / 2;
                Gizmos.DrawWireCube(cellCenter, cellScale);
            }
        }
    }



    // Convert world position to grid cell coordinates
    private Vector3Int WorldToCellCoords(Vector3 worldPosition)
    {
        Vector3 normalizedPosition = (worldPosition - gridPosition + gridSize / 2) / cellSize;
        return new Vector3Int(Mathf.FloorToInt(normalizedPosition.x), Mathf.FloorToInt(normalizedPosition.y), Mathf.FloorToInt(normalizedPosition.z));
    }

    // Convert grid cell coordinates to world position
    private Vector3 CellCoordsToWorld(Vector3Int cellCoords)
    {
        return (Vector3)cellCoords * cellSize + gridPosition - gridSize / 2 + new Vector3(cellSize / 2, cellSize / 2, cellSize / 2);
    }

}

