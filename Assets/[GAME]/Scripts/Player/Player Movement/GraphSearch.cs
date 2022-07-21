using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class GraphSearch
    {
        public static BFSResult BFSGetRange(HexGrid hexGrid, Vector3Int startPoint)
        {
            Dictionary<Vector3Int, Vector3Int?> visitedNodes = new Dictionary<Vector3Int, Vector3Int?>();
            Queue<Vector3Int> nodesToVisitQueue = new Queue<Vector3Int>();
            
            nodesToVisitQueue.Enqueue(startPoint);
            visitedNodes.Add(startPoint, null);

            while (nodesToVisitQueue.Count > 0)
            {
                Vector3Int currentNode = nodesToVisitQueue.Dequeue();
                foreach (Vector3Int neighbourPosition in hexGrid.GetNeighboursFor(currentNode))
                {
                    if (hexGrid.GetTileAt(neighbourPosition).IsObstacle())
                        continue;

                    if (hexGrid.GetTileAt(neighbourPosition).IsRoad())
                    {
                        if (!visitedNodes.ContainsKey(neighbourPosition))
                        {
                            visitedNodes[neighbourPosition] = currentNode;
                            nodesToVisitQueue.Enqueue(neighbourPosition);
                        }
                    }
                    
                    else if (hexGrid.GetTileAt(neighbourPosition).IsLocation())
                    {
                        if (!visitedNodes.ContainsKey(neighbourPosition))
                        {
                            visitedNodes[neighbourPosition] = currentNode;
                        }
                    }
                    
                }
            }

            return new BFSResult { VisitedNodesDictionary = visitedNodes };
        }

        public static List<Vector3Int> GeneratePathBFS(Vector3Int current, Dictionary<Vector3Int, Vector3Int?> visitedNodesDictionary)
        {
            List<Vector3Int> path = new List<Vector3Int>();
            path.Add(current);
            while (visitedNodesDictionary[current] != null)
            {
                path.Add(visitedNodesDictionary[current].Value);
                current = visitedNodesDictionary[current].Value;
            }
            path.Reverse();
            return path.Skip(1).ToList();
        }
    }
    
    public struct BFSResult
    {
        public Dictionary<Vector3Int, Vector3Int?> VisitedNodesDictionary;

        public List<Vector3Int> GetPathTo(Vector3Int destination)
        {
            if (VisitedNodesDictionary.ContainsKey(destination) == false)
                return new List<Vector3Int>();
            return GraphSearch.GeneratePathBFS(destination, VisitedNodesDictionary);
        }

        public bool IsHexPositionInRange(Vector3Int position)
        {
            return VisitedNodesDictionary.ContainsKey(position);
        }

        public IEnumerable<Vector3Int> GetRangePositions()
        {
            return VisitedNodesDictionary.Keys;
        }
    }
}
