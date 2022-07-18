using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class HexGrid : MonoBehaviour
    {
        private Dictionary<Vector3Int, Hex> _hexTileDictionary = new Dictionary<Vector3Int, Hex>();
        private Dictionary<Vector3Int, List<Vector3Int>> _hexTileNeighbourDictionary = new Dictionary<Vector3Int, List<Vector3Int>>();

        private void Start()
        {
            foreach (Hex hex in FindObjectsOfType<Hex>())
            {
                _hexTileDictionary[hex.HexCoordinates] = hex;
            }

            List<Vector3Int> neighbours = GetNeighboursFor(new Vector3Int(-3, 0, 0));
            Debug.Log("Neighbours for (-3,0,0) are: ");
            foreach (Vector3Int neighbour in neighbours)
            {
                Debug.Log(neighbour);
            }
        }

        public Hex GetTileAt(Vector3Int hexCoordinates)
        {
            Hex result = null;
            _hexTileDictionary.TryGetValue(hexCoordinates, out result);
            return result;
        }

        public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
        {
            if (!(_hexTileDictionary.ContainsKey(hexCoordinates)))
                return new List<Vector3Int>();
            if (_hexTileNeighbourDictionary.ContainsKey(hexCoordinates))
                return _hexTileNeighbourDictionary[hexCoordinates];
            
            _hexTileNeighbourDictionary.Add(hexCoordinates, new List<Vector3Int>());

            foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.x))
            {
                if (_hexTileDictionary.ContainsKey(hexCoordinates + direction))
                    _hexTileNeighbourDictionary[hexCoordinates].Add(hexCoordinates + direction);
            }

            return _hexTileNeighbourDictionary[hexCoordinates];
        }

        public static class Direction
        {
            public static List<Vector3Int> DirectionsOffsetOdd = new List<Vector3Int>()
            {
                new Vector3Int(0, 0, 1),  //N
                new Vector3Int(1, 0, 0),  //E1
                new Vector3Int(1, 0, -1), //E2
                new Vector3Int(0, 0, -1), //S 
                new Vector3Int(-1, 0, -1), //W1
                new Vector3Int(-1, 0, 0) //W2
            };
            public static List<Vector3Int> DirectionsOffsetEven = new List<Vector3Int>()
            {
                new Vector3Int(0, 0, 1),  //N
                new Vector3Int(1, 0, 1),  //E1
                new Vector3Int(1, 0, 0),  //E2
                new Vector3Int(0, 0, -1), //S 
                new Vector3Int(-1, 0, 0), //W2
                new Vector3Int(-1, 0, 1)  //W1
            };
            
            public static List<Vector3Int> GetDirectionList(int x)
                => x % 2 == 0 ? DirectionsOffsetEven : DirectionsOffsetOdd;
        }
    }
}
