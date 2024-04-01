using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic pathfinding class.
/// Code can be cleaned up, dirty but works
/// Uses depth first research
/// </summary>
public class PathFinding : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    int[][] mapData = new int[][] { 
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 0, 0, 0, 0, 1, 1, 1},
        new int[]{1, 1, 1, 0, 1, 1, 1, 1, 1, 1},
        new int[]{1, 0, 0, 0, 1, 1, 0, 1, 1, 1},
        new int[]{1, 1, 1, 0, 0, 0, 0, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };

    void Start()
    {
        findPath(new Vector2Int(0, 1), new Vector2Int(3, 5));
    }
    /// <summary>
    /// find path from a point to a point
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    List<Vector2Int> findPath(Vector2Int start, Vector2Int target)
    {
        Dictionary<Vector2Int, Vector2Int> followedPath = new Dictionary<Vector2Int, Vector2Int>();
        followedPath.Add(start, new Vector2Int(-1, -1));

        Vector2Int currentPoint = start;
        List<Vector2Int> points = new List<Vector2Int>()
        {
            start
        };

        while (points.Count > 0)
        {
            currentPoint = points[0];
            points.RemoveAt(0);

            List<Vector2Int> neighbors = new List<Vector2Int>();
            if(getBottomNeighbor(mapData, currentPoint) > 0)
            {
                neighbors.Add(currentPoint + Vector2Int.up);
            }
            if (getLeftNeighbor(mapData, currentPoint) > 0)
            {
                neighbors.Add(currentPoint + Vector2Int.left);
            }
            if (getRightNeighbor(mapData, currentPoint) > 0)
            {
                neighbors.Add(currentPoint + Vector2Int.right);
            }
            if (getUpperNeighbor(mapData, currentPoint) > 0)
            {
                neighbors.Add(currentPoint + Vector2Int.down);
            }

            foreach(Vector2Int next in neighbors)
            {
                if (!followedPath.ContainsKey(next))
                {
                    followedPath.Add(next, currentPoint);
                    points.Insert(0, next);
                }

            }
            if (currentPoint.x == target.x && currentPoint.y == target.y) 
            {
                //Debug.Log("Reached");
                break;
            }
        }
        // construct the path
        currentPoint = target;
        List<Vector2Int> pathPoints = new List<Vector2Int>();
        while(currentPoint != start)
        {
            pathPoints.Add(followedPath[currentPoint]);
            currentPoint = followedPath[currentPoint];
        }

        /*
        foreach(Vector2Int p in pathPoints)
        {
            Debug.Log(p);
            Debug.Log("Value of " + p + ":" + mapData[p.x][p.y]);
        }
        */
        pathPoints.Reverse();
        return pathPoints;
    }
    private int getRightNeighbor(int[][] mapdata, Vector2Int position)
    {
        try
        {
            return mapdata[position.x + 1][position.y];
        }catch(Exception e) { }
        return -1;
    }
    private int getLeftNeighbor(int[][] mapData, Vector2Int position)
    {
        try
        {
            return mapData[position.x - 1][position.y];
        }catch(Exception e) { }
        return -1;
    }
    private int getBottomNeighbor(int[][] mapData, Vector2Int position)
    {
        try
        {
            return mapData[position.x][position.y + 1];
        }catch(Exception e) { }
        return -1;
    }
    private int getUpperNeighbor(int[][] mapData, Vector2Int position)
    {
        try
        {
            return mapData[position.x][position.y - 1];
        }catch(Exception e) { }
        return -1;
    }
}
