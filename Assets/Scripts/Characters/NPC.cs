using UnityEngine;

namespace MerchantOfBohemia.Characters
{
    public class NPC : Character
    {
        [SerializeField]
        private int _fightPower; //Determines how well it fights, effects the result of the battle encounter.
    }
}