using System;
using MerchantOfBohemia.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MerchantOfBohemia.CharacterDevelopment
{
    public class SkillAssignment : MonoBehaviour
    {
        [SerializeField] private Player player;

        [SerializeField] private Button mainLvlButton,
            intelligenceLvlButton,
            charismaLvlButton,
            scoutLvlButton,
            veteranLvlButton,
            traderLvlButton,
            captainLvlButton;
        
        [SerializeField] private TMP_Text mainLvlText,
            intelligenceLvlText,
            charismaLvlText,
            scoutLvlText,
            veteranLvlText,
            traderLvlText,
            captainLvlText;
        
        private int _tempMainLvl, _tempIntelligenceLvl, _tempCharismaLvl, _tempScoutLvl, _tempVeteranLvl, _tempTraderLvl, _tempCaptainLvl;
        private int _tempAttributePoint, _tempIntelligenceSkillPoint, _tempCharismaSkillPoint;

        private void Start()
        {
            switch (player.gameDifficulty)
            {
                case Player.GameDifficulty.easy:
                    player.level = 4;
                    player.money = 250;
                    break;
                case Player.GameDifficulty.medium:
                    player.level = 3;
                    player.money = 200;
                    break;
                case Player.GameDifficulty.hard:
                    player.level = 2;
                    player.money = 150;
                    break;
                default:
                    player.level = 3;
                    player.money = 200;
                    break;
            }

            _tempMainLvl = player.level;
            _tempAttributePoint = _tempMainLvl; // sadece ilk skill menüsü için çalışır, sonradan değiştirilmesi lazım.

            mainLvlText.text = "Main Level : " + _tempMainLvl;
            intelligenceLvlText.text = "Intelligence Level : " + _tempIntelligenceLvl;
            charismaLvlText.text = "Charisma Level : " + _tempCharismaLvl;
            scoutLvlText.text = "Scout Level : " + _tempScoutLvl;
            veteranLvlText.text = "Veteran Level : " + _tempVeteranLvl;
            traderLvlText.text = "Trader Level : " + _tempTraderLvl;
            captainLvlText.text = "Captain Level : " + _tempCaptainLvl;
            
            UpdateConditions();;
        }

        public void AssignAttributePointToIntelligence()
        {
            _tempAttributePoint--;
            _tempIntelligenceLvl++;
            _tempIntelligenceSkillPoint++;

            Debug.Log("Main Level: " + _tempMainLvl + " / Intelligence level increased to " + _tempIntelligenceLvl);
        }
        
        public void AssignAttributePointToCharisma()
        {
            _tempAttributePoint--;
            _tempCharismaLvl++;
            _tempCharismaSkillPoint++;
            
            Debug.Log("Main Level: " + _tempMainLvl + " / Charisma level increased to " + _tempCharismaLvl);
        }

        public void AssignSkillPointToScout()
        {
            _tempIntelligenceSkillPoint--;
            _tempScoutLvl++;
            
            Debug.Log("Intelligence Level: " + _tempIntelligenceLvl + " / Intelligence Skill Points: " + _tempIntelligenceSkillPoint + " / Scout level increased to " + _tempScoutLvl);
        }
        
        public void AssignSkillPointToVeteran()
        {
            _tempIntelligenceSkillPoint--;
            _tempVeteranLvl++;
            
            Debug.Log("Intelligence Level: " + _tempIntelligenceLvl + " / Veteran level increased to " + _tempVeteranLvl);
        }
        
        public void AssignSkillPointToTrader()
        {
            _tempCharismaSkillPoint--;
            _tempTraderLvl++;
            
            Debug.Log("Charisma Level: " + _tempCharismaLvl + " / Trader level increased to " + _tempTraderLvl);
        }
        
        public void AssignSkillPointToCaptain()
        {
            _tempCharismaSkillPoint--;
            _tempCaptainLvl++;
            
            Debug.Log("Charisma Level: " + _tempCharismaLvl + " / Captain level increased to " + _tempCaptainLvl);
        }

        public void ResetAssignment()
        {
            _tempMainLvl = player.level;
            _tempAttributePoint = _tempMainLvl;
            _tempIntelligenceLvl = 0;
            _tempCharismaLvl = 0;
            _tempScoutLvl = 0;
            _tempVeteranLvl = 0;
            _tempTraderLvl = 0;
            _tempCaptainLvl = 0;

            _tempIntelligenceSkillPoint = 0;
            _tempCharismaSkillPoint = 0;
        }
        

        public void UpdateConditions()
        {
            if (_tempAttributePoint == 0)
            {
                intelligenceLvlButton.interactable = false;
                charismaLvlButton.interactable = false;
            }
            else
            {
                intelligenceLvlButton.interactable = true;
                charismaLvlButton.interactable = true;
            }

            if (_tempIntelligenceSkillPoint == 0)
            {
                scoutLvlButton.interactable = false;
                veteranLvlButton.interactable = false;
            }
            else
            {
                scoutLvlButton.interactable = true;
                veteranLvlButton.interactable = true;
            }
            
            if (_tempCharismaSkillPoint == 0)
            {
                traderLvlButton.interactable = false;
                captainLvlButton.interactable = false;
            }
            else
            {
                traderLvlButton.interactable = true;
                captainLvlButton.interactable = true;
            }
            
            mainLvlText.text = "Main Level : " + _tempMainLvl;
            intelligenceLvlText.text = "Intelligence Level : " + _tempIntelligenceLvl;
            charismaLvlText.text = "Charisma Level : " + _tempCharismaLvl;
            scoutLvlText.text = "Scout Level : " + _tempScoutLvl;
            veteranLvlText.text = "Veteran Level : " + _tempVeteranLvl;
            traderLvlText.text = "Trader Level : " + _tempTraderLvl;
            captainLvlText.text = "Captain Level : " + _tempCaptainLvl;
        }
        
        public void LoadGame()
        {
            player.PlayerAttributes.intelligence = _tempIntelligenceLvl;
            player.PlayerAttributes.charisma = _tempCharismaLvl;
            player.PlayerSkills.scout = _tempScoutLvl;
            player.PlayerSkills.veteran = _tempVeteranLvl;
            player.PlayerSkills.trader = _tempTraderLvl;
            player.PlayerSkills.captain = _tempCaptainLvl;

            player.partySize = 1;

            player.nextLevelAt = 60 * player.level;
            
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        
    }
}
