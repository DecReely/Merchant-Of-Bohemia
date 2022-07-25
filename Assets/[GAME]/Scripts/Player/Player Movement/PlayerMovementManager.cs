using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class PlayerMovementManager : MonoBehaviour
    {
        private HexGrid _hexGrid;
        private MovementSystem _movementSystem;

        private PlayerMovement _playerMovement;
        private Hex _previouslySelectedHex;

        private void Awake()
        {
            _hexGrid = FindObjectOfType<HexGrid>();
            _movementSystem = FindObjectOfType<MovementSystem>();
        }

        private void OnEnable()
        {
            EventHandler.PlayerSelectedEvent += HandlePlayerSelected;
            EventHandler.TerrainSelectedEvent += HandleTerrainSelected;
        }
        
        private void OnDisable()
        {
            EventHandler.PlayerSelectedEvent -= HandlePlayerSelected;
            EventHandler.TerrainSelectedEvent -= HandleTerrainSelected;
        }

        public void HandlePlayerSelected(GameObject player)
        {
            if (GameManager.GameState != Enums.GameState.Idle)
                return;
            
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

            if (CheckIfPlayerSelectedAgain(playerMovement))
            {
                ClearOldSelection();
                return;
            }

            PrepareUnitForMovement(playerMovement);
        }

        private void PrepareUnitForMovement(PlayerMovement playerMovement)
        {
            if (this._playerMovement != null)
            {
                ClearOldSelection();
            }

            this._playerMovement = playerMovement;
            this._playerMovement.Select();
            _movementSystem.ShowRange(this._playerMovement, this._hexGrid);
        }

        private bool CheckIfPlayerSelectedAgain(PlayerMovement playerMovement)
        {
            return this._playerMovement == playerMovement;
        }

        private void ClearOldSelection()
        {
            _previouslySelectedHex = null;
            this._playerMovement.Deselect();
            _movementSystem.HideRange(this._hexGrid);
            this._playerMovement = null;
        }

        public void HandleTerrainSelected(GameObject hexGO)
        {
            if (_playerMovement == null || GameManager.GameState != Enums.GameState.Idle)
            {
                return;
            }

            Hex selectedHex = hexGO.GetComponent<Hex>();

            if (HandleHexOutOfRange(selectedHex.HexCoordinates) ||
                HandleSelectedHexIsPlayerHex(selectedHex.HexCoordinates) || selectedHex.hexType != Enums.HexType.Location)
                return;

            HandleTargetHexSelected(selectedHex);
        }

        private bool HandleHexOutOfRange(Vector3Int hexPosition)
        {
            return _movementSystem.IsHexInRange(hexPosition) == false;
        }

        private bool HandleSelectedHexIsPlayerHex(Vector3Int hexPosition)
        {
            if (hexPosition == _hexGrid.GetClosestHex(_playerMovement.transform.position))
            {
                _playerMovement.Deselect();
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
                _movementSystem.ShowPath(selectedHex.HexCoordinates, this._hexGrid);
            }
            else
            {
                EventHandler.CallMovementStartedEvent(_playerMovement);
                _movementSystem.MovePlayer(_playerMovement, this._hexGrid);
                ClearOldSelection();
            }
        }
    }
}
