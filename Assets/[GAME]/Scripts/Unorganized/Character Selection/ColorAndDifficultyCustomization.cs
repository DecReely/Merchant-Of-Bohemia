using MerchantOfBohemia.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MerchantOfBohemia.Character_Selection
{
    public class ColorAndDifficultyCustomization : MonoBehaviour
    {
        private const int ColorVariantNumber = 4;

        [SerializeField] private Player player;
        [SerializeField] private GameObject allCampPrefabs;
        [SerializeField] private GameObject allRecruitPrefabs;
        [SerializeField] private GameObject[] camps;
        
        private int _selectedCamp = 0;

        [SerializeField] private TMP_Text colorText, difficultyText;
        private Player.GameDifficulty _gameDifficulty;

        public void Start()
        {
            camps[0].SetActive(true);
        }

        public void PreviousColor()
        {
            camps[_selectedCamp].SetActive(false);
            _selectedCamp--;
            if (_selectedCamp < 0)
                _selectedCamp += camps.Length;
            camps[_selectedCamp].SetActive(true);
            
            Debug.Log(_selectedCamp);
        }
        
        public void NextColor()
        {
            camps[_selectedCamp].SetActive(false);
            _selectedCamp = (_selectedCamp + 1) % camps.Length;
            camps[_selectedCamp].SetActive(true);
            
            Debug.Log(_selectedCamp);
        }
        
        public void PreviousDifficulty()
        {
            if (_gameDifficulty != Player.GameDifficulty.easy)
                _gameDifficulty--;
            else
                _gameDifficulty = Player.GameDifficulty.hard;
            difficultyText.text = _gameDifficulty.ToString();
        }
        
        public void NextDifficulty()
        {
            if (_gameDifficulty != Player.GameDifficulty.hard)
                _gameDifficulty++;
            else
                _gameDifficulty = Player.GameDifficulty.easy;

            difficultyText.text = _gameDifficulty.ToString();
        }

        public void LoadSkillCustomizationMenu()
        {
            player.campPrefab = allCampPrefabs.transform.GetChild(_selectedCamp).gameObject;
            
            player.recruitLvl1 = allRecruitPrefabs.transform.GetChild(_selectedCamp * ColorVariantNumber).gameObject;
            player.recruitLvl2 = allRecruitPrefabs.transform.GetChild(_selectedCamp * ColorVariantNumber + 1).gameObject;
            player.recruitLvl3 = allRecruitPrefabs.transform.GetChild(_selectedCamp * ColorVariantNumber + 2).gameObject;
            player.recruitLvl4 = allRecruitPrefabs.transform.GetChild(_selectedCamp * ColorVariantNumber + 3).gameObject;
            
            player.gameDifficulty = _gameDifficulty;
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }

    }
}
