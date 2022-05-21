using MerchantOfBohemia.CharacterDevelopment;
using UnityEngine;

namespace MerchantOfBohemia.Characters
{
    [CreateAssetMenu(menuName = "Create Character/Player")]
    public class Player : Character
    {
        public int experience;
        public int nextLevelAt;
        public int money;
        
        [Header("Attributes")]
        public Attributes PlayerAttributes = new Attributes();
        [Header("Skills")]
        public Skills PlayerSkills = new Skills();

        [Header("Camp Prefab")]
        public GameObject campPrefab;
        [Header("Recruits")]
        public GameObject recruitLvl1;
        public GameObject recruitLvl2;
        public GameObject recruitLvl3;
        public GameObject recruitLvl4;
        [Header("Game Difficulty")]
        public GameDifficulty gameDifficulty;

        public enum GameDifficulty
        {
            easy,
            medium,
            hard
        }
    }
}
