using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class GridEdgeRenderer : MonoBehaviour
{

    // cell prefab that contains up, down, left, and right walls
    public GameObject cellPrefab;      
    public GameObject winCollider;  
    private List<List<GameObject>> cellInstances = new List<List<GameObject>>();
    private int rows = 8;
    private int cols = 8;

    void Start()
    {
        // initialize grid
        for (int i = 0; i < rows; i++)
        {
            cellInstances.Add(new List<GameObject>());  // initialize row for 2D list
            for (int j = 0; j < cols; j++)
            {
                PlaceCell(new Vector2(i * 2f, j * 2f), i);
            }
        }

        // generate the maze
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // dfs backtracking algorithm for generating maze
        // pick a random neighbor of the current cell and create a path from
        // the current cell to the next cell

        // dfs stack
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        // bottom left cell on cartesian plane
        Vector2Int start = new Vector2Int(0, 0);  
        stack.Push(start);

        // store visited cells to a hash set
        // include start upon initialization because
        // that is the first cell we visit
        HashSet<Vector2Int> visited = new HashSet<Vector2Int> {start};

        // dfs loop
        while (stack.Count > 0) {
            // get current cell and remove it from stack
            Vector2Int currentCell = stack.Pop();
            
            List<Vector2Int> currentCellNeighbors = GetUnvisitedNeighbors(currentCell, visited);

            if (currentCellNeighbors.Count > 0) {
                // push back current cell to stack
                // for backtracking purposes
                stack.Push(currentCell);

                // randomly pick a neighboring cell
                // for maze randomness
                // this is to ensure that each iteration provides
                // a different maze
                Vector2Int nextCell = currentCellNeighbors[Random.Range(0, currentCellNeighbors.Count)];

                // remove the walls to create a path
                // from current to next cell
                RemoveWall(currentCell, nextCell);

                // update visited cells and push the next cell to the stack
                // as the next cell to explore neighbors from
                visited.Add(nextCell);
                stack.Push(nextCell);
            }
        }

        // create the entrance at (0, 0) after generating the maze
        CreateEntrance();

        // create the exit for the maze randomly
        CreateExit();
    }

    void CreateEntrance() {
        // access bottom left object and destroy the wall
        // to create an entrance
        int x = 0, y = 4;
        GameObject currentCellObj = cellInstances[x][y];
        Destroy(currentCellObj.transform.Find("LeftWall").gameObject);
    }

    void CreateExit() {
        // randomize at the top right side for the exit from row 5 to 7 (y axis)
        int yAxis = Random.Range(3, rows - 1);
        // access the cell object
        Vector2Int start = new Vector2Int(cols - 1, yAxis); 
        GameObject currentCellObj = cellInstances[start.x][start.y];
        Transform rightWall = currentCellObj.transform.Find("RightWall");
        // exit trigger
        Debug.Log("test" + currentCellObj.transform.position);
        if (rightWall != null) {
            // get the wall's position to use for the collider
            Vector3 wallPosition = rightWall.position;
            
            // destroy the right wall to create the exit
            Destroy(rightWall.gameObject);
            Debug.Log("Destroyed Wall Position: " + wallPosition);

            // add an offset to shift the collider to the right
            float offset = 0.5f; // adjust as needed
            Vector3 colliderPosition = new Vector3(wallPosition.x + offset, wallPosition.y, wallPosition.z);
            winCollider.transform.position = colliderPosition;
            winCollider.SetActive(true);
            Debug.Log("Exit Collider Position: " + winCollider.transform.position);
        } else {
            Debug.LogError("RightWall not found for the current cell.");
        }
    }

    // function to place cell at a specific position
    void PlaceCell(Vector2 position, int row)
    {
        GameObject cellInstance = Instantiate(cellPrefab, position, Quaternion.identity);
        cellInstances[row].Add(cellInstance); // Store in the 2D list
    }

    // function to remove the wall between two cells
    void RemoveWall(Vector2Int current, Vector2Int next)
    {
        // access cell instances
        GameObject currentCellObject = cellInstances[current.x][current.y];
        GameObject nextCellObject = cellInstances[next.x][next.y];

        // get the direction of the path
        Vector2Int direction = next - current;

        // destroy the wall objects that faces each other
        if (direction == Vector2Int.up) {
            Destroy(currentCellObject.transform.Find("UpWall").gameObject);
            Destroy(nextCellObject.transform.Find("DownWall").gameObject);
        } else if (direction == Vector2Int.down) {
            Destroy(currentCellObject.transform.Find("DownWall").gameObject);
            Destroy(nextCellObject.transform.Find("UpWall").gameObject);
        } else if (direction == Vector2Int.left) {
            Destroy(currentCellObject.transform.Find("LeftWall").gameObject);
            Destroy(nextCellObject.transform.Find("RightWall").gameObject);
        } else if (direction == Vector2Int.right) {
            Destroy(currentCellObject.transform.Find("RightWall").gameObject);
            Destroy(nextCellObject.transform.Find("LeftWall").gameObject);
        }
    }

    // get unvisited neighbors of a cell for path generation
    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell, HashSet<Vector2Int> visited)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        // directions
        // https://docs.unity3d.com/ScriptReference/Vector2Int.html
        Vector2Int[] directions = {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right};

        // try all directions 
        // to check for neighboring cells
        foreach (Vector2Int dir in directions) {
            Vector2Int neighbor = cell + dir;

            // validate cell location and check if it has been visited already
            if (IsValidCell(neighbor) && !visited.Contains(neighbor)) {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    // check if a cell is within grid bounds
    bool IsValidCell(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < rows && cell.y >= 0 && cell.y < cols;
    }
}