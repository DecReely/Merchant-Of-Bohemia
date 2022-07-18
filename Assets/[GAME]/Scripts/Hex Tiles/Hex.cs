using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerchantOfBohemia
{
    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        private HexCoordinates _hexCoordinates;

        public Vector3Int HexCoordinates => _hexCoordinates.GetHexCoordinates();

        private void Awake()
        {
            _hexCoordinates = GetComponent<HexCoordinates>();
        }
    }
}
