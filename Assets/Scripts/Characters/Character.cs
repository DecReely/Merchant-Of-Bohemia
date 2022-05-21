using System;
using System.Collections.Generic;
using MerchantOfBohemia.Items;
using UnityEngine;


namespace MerchantOfBohemia.Characters
{
    public class Character : ScriptableObject
    {
        public List<Item> inventory = new List<Item>();
        public GameObject gameObjectPrefab;
        public String characterName;
        public int level;
        public int partySize;
    }
}
