using System;
using UnityEngine;


namespace MerchantOfBohemia.Characters
{
    public class Character : ScriptableObject
    {
        //private Inventory _inventory;
        [SerializeField]
        private GameObject _gameObjectPrefab;
        [SerializeField]
        private String _name;
        [SerializeField]
        private int _partySize;
    }
}
