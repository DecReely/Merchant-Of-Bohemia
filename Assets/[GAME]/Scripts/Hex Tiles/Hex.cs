using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerchantOfBohemia
{
    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        private GlowHighlight _glowHighlight;
        private HexCoordinates _hexCoordinates;

        public Enums.HexType hexType;

        public Vector3Int HexCoordinates => _hexCoordinates.GetHexCoordinates();

        public bool IsObstacle()
        {
            return this.hexType == Enums.HexType.Obstacle;
        }
        
        public bool IsLocation()
        {
            return this.hexType == Enums.HexType.Location;
        }
        
        public bool IsRoad()
        {
            return this.hexType == Enums.HexType.Road;
        }

        private void Awake()
        {
            _hexCoordinates = GetComponent<HexCoordinates>();
            _glowHighlight = GetComponent<GlowHighlight>();
        }

        public void EnableHighlight()
        {
            _glowHighlight.ToggleGlow(true);
        }

        public void DisableHighlight()
        {
            _glowHighlight.ToggleGlow(false);
        }

        public void ResetHighlight()
        {
            _glowHighlight.ResetGlowHighlight();
        }

        public void HighlightPath()
        {
            _glowHighlight.HighlightValidPath();
        }
    }
}
