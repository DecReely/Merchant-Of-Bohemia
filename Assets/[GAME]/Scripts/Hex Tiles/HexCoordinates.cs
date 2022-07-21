using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MerchantOfBohemia
{
    public class HexCoordinates : MonoBehaviour
    {
        public const float XOffset = 4.3f, YOffset = 1, ZOffset = 5f;

        [Header("Offset Coordinates")] [SerializeField]
        private Vector3Int offsetCoordinates;

        private void Awake()
        {
            offsetCoordinates = ConvertPositionToOffset(transform.position);
        }

        public static Vector3Int ConvertPositionToOffset(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x / XOffset);
            int y = Mathf.RoundToInt(position.y / YOffset);
            int z = Mathf.CeilToInt(position.z / ZOffset);

            return new Vector3Int(x, y, z);
        }

        public Vector3Int GetHexCoordinates()
        {
            return offsetCoordinates;
        }
    }
}
