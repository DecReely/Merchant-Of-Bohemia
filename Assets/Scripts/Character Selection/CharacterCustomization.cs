using MerchantOfBohemia.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MerchantOfBohemia.Character_Selection
{
    public class CharacterCustomization : MonoBehaviour
    {
        private const int ClothingVariantNumber = 5;
        private const int SkinVariantNumber = 3;

        [SerializeField] private Player player;
        [SerializeField] private GameObject allMerchantPrefabs;
        [SerializeField] private GameObject[] characters;
        
        private int _selectedCharacter = 0;
        private int _gender, _skinColor, _hairColor;

        private string _playerName;

        public void Start()
        {
            characters[0].SetActive(true);
        }

        public void UpdateGender(int gender)
        {
            _gender = gender;
        }

        public void UpdateSkinColor(int skinColor)
        {
            _skinColor = skinColor;
        }

        public void UpdateHairColor(int hairColor)
        {
            _hairColor = hairColor;
            
        }

        public void UpdateModel()
        {
            characters[_selectedCharacter].SetActive(false);
            _selectedCharacter = (_gender * (ClothingVariantNumber * SkinVariantNumber)) +
                                 (_skinColor * ClothingVariantNumber) + (_hairColor);
            characters[_selectedCharacter].SetActive(true);
        }

        public void SetName(string nameInput)
        {
            player.characterName = nameInput;
        }

        public void LoadCampAndRecruitCustomizationMenu()
        {
            player.gameObjectPrefab = allMerchantPrefabs.transform.GetChild(_selectedCharacter).gameObject;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

    }
}
