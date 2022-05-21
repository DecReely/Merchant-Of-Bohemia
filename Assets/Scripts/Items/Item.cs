using UnityEngine;
using UnityEngine.Serialization;

namespace MerchantOfBohemia.Items
{
    public class Item : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string itemName;
        [SerializeField] private int baseValue;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private ItemType itemType;

        private enum ItemType
        {
            Food,
            Transport,
            Material,
            Luxury
        }

    }
}
