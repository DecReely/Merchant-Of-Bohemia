using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private HexGrid hexGrid;
        [SerializeField] private MovementSystem movementSystem;

        private Unit _selectedUnit;
        private Hex _previouslySelectedHex;

        private void OnEnable()
        {
            EventHandler.PlayerSelectedEvent += HandleUnitSelected;
            EventHandler.TerrainSelectedEvent += HandleTerrainSelected;
        }
        
        private void OnDisable()
        {
            EventHandler.PlayerSelectedEvent -= HandleUnitSelected;
            EventHandler.TerrainSelectedEvent -= HandleTerrainSelected;
        }

        public void HandleUnitSelected(GameObject unit)
        {
            if (GameManager.GameState != Enums.GameState.Idle)
                return;
            
            Unit unitReference = unit.GetComponent<Unit>();

            if (CheckIfSameUnitSelected(unitReference))
            {
                ClearOldSelection();
                return;
            }

            PrepareUnitForMovement(unitReference);
        }

        private void PrepareUnitForMovement(Unit unitReference)
        {
            if (this._selectedUnit != null)
            {
                ClearOldSelection();
            }

            this._selectedUnit = unitReference;
            this._selectedUnit.Select();
            movementSystem.ShowRange(this._selectedUnit, this.hexGrid);
        }

        private bool CheckIfSameUnitSelected(Unit unitReference)
        {
            return this._selectedUnit == unitReference;
        }

        private void ClearOldSelection()
        {
            _previouslySelectedHex = null;
            this._selectedUnit.Deselect();
            movementSystem.HideRange(this.hexGrid);
            this._selectedUnit = null;
        }

        public void HandleTerrainSelected(GameObject hexGO)
        {
            if (_selectedUnit == null || GameManager.GameState != Enums.GameState.Idle)
            {
                return;
            }

            Hex selectedHex = hexGO.GetComponent<Hex>();

            if (HandleHexOutOfRange(selectedHex.HexCoordinates) ||
                HandleSelectedHexIsUnitHex(selectedHex.HexCoordinates) || selectedHex.hexType != Enums.HexType.Location)
                return;

            HandleTargetHexSelected(selectedHex);
        }

        private bool HandleHexOutOfRange(Vector3Int hexPosition)
        {
            return movementSystem.IsHexInRange(hexPosition) == false;
        }

        private bool HandleSelectedHexIsUnitHex(Vector3Int hexPosition)
        {
            if (hexPosition == hexGrid.GetClosestHex(_selectedUnit.transform.position))
            {
                _selectedUnit.Deselect();
                ClearOldSelection();
                return true;
            }

            return false;
        }

        private void HandleTargetHexSelected(Hex selectedHex)
        {
            if (_previouslySelectedHex == null || _previouslySelectedHex != selectedHex)
            {
                _previouslySelectedHex = selectedHex;
                movementSystem.ShowPath(selectedHex.HexCoordinates, this.hexGrid);
            }
            else
            {
                EventHandler.CallMovementStartedEvent(_selectedUnit);
                movementSystem.MoveUnit(_selectedUnit, this.hexGrid);
                ClearOldSelection();
            }
        }
    }
}
