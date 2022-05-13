using UnityEngine;

namespace MerchantOfBohemia.Characters
{
    [CreateAssetMenu(menuName = "Create Character/Player")]
    public class Player : Character
    {
        //private Stats _stats;
        [SerializeField]
        private GameObject _campPrefab;
        [SerializeField]
        private GameObject _recruitPrefab;
    }
}
