using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class MovementSystem : MonoBehaviour
    {
        private BFSResult _movementRange = new BFSResult();
        private List<Vector3Int> _currentPath = new List<Vector3Int>();

        public void HideRange(HexGrid hexGrid)
        {
            foreach (Vector3Int hexPosition in _movementRange.GetRangePositions())
            {
                hexGrid.GetTileAt(hexPosition).DisableHighlight();
            }

            _movementRange = new BFSResult();
        }
        
        public void ShowRange(Unit selectedUnit, HexGrid hexGrid)
        {
            CalculateRange(selectedUnit, hexGrid);
            
            foreach (Vector3Int hexPosition in _movementRange.GetRangePositions())
            {
                hexGrid.GetTileAt(hexPosition).EnableHighlight();
            }
        }

        public void CalculateRange(Unit selectedUnit, HexGrid hexGrid)
        {
            _movementRange = GraphSearch.BFSGetRange(hexGrid, hexGrid.GetClosestHex(selectedUnit.transform.position));
        }

        public void ShowPath(Vector3Int selectedHexPosition, HexGrid hexGrid)
        {
            if (_movementRange.GetRangePositions().Contains(selectedHexPosition))
            {
                foreach (Vector3Int hexPosition in _currentPath)
                {
                    hexGrid.GetTileAt(hexPosition).ResetHighlight();
                }

                _currentPath = _movementRange.GetPathTo(selectedHexPosition);

                foreach (Vector3Int hexPosition in _currentPath)
                {
                    hexGrid.GetTileAt(hexPosition).HighlightPath();
                }
            }
        }

        public void MoveUnit(Unit selectedUnit, HexGrid hexGrid)
        {
            selectedUnit.MoveThroughPath(_currentPath.Select(pos => hexGrid.GetTileAt(pos).transform.position).ToList());
        }

        public bool IsHexInRange(Vector3Int hexPosition)
        {
            return _movementRange.IsHexPositionInRange(hexPosition);
        }
    }
}