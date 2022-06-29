using TMPro;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class GameClock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _seasonText;
        [SerializeField] private TextMeshProUGUI _dateText;
        [SerializeField] private TextMeshProUGUI _timeIntervalText;

        private void OnEnable()
        {
            EventHandler.AdvanceGameMinuteEvent += UpdateGameClock;
        }
        
        private void OnDisable()
        {
            EventHandler.AdvanceGameMinuteEvent -= UpdateGameClock;
        }

        private void UpdateGameClock(int gameYear, Enums.Season gameSeason, int gameDay, 
            Enums.TimeInterval timeInterval, int gameHour, int gameMinute)
        {
            string minute;
            string hour;
            string day;

            gameMinute = gameMinute - (gameMinute % 30);
            
            if (gameMinute < 15)
            {
                minute = "0" + gameMinute.ToString();
            }
            else
            {
                minute = gameMinute.ToString();
            }

            if (gameHour < 10)
            {
                hour = "0" + gameHour.ToString();
            }
            else
            {
                hour = gameHour.ToString();
            }

            _timeText.SetText(hour + ":" + minute);

            if (gameDay < 10)
            {
                day = "0" + gameDay.ToString();
            }
            else
            {
                day = gameDay.ToString();
            }
            
            _dateText.SetText(day + "/" + gameYear.ToString());
            
            _seasonText.SetText(gameSeason.ToString());
            
            _timeIntervalText.SetText(GetTimeInterval(timeInterval));
        }
        
        private string GetTimeInterval(Enums.TimeInterval currentTimeInterval)
        {
            switch (currentTimeInterval)
            {
                case Enums.TimeInterval.Dawn:
                    return "Dawn";
                case Enums.TimeInterval.EarlyMorning:
                    return "Early Morning";
                case Enums.TimeInterval.Morning:
                    return "Morning";
                case Enums.TimeInterval.Noon:
                    return "Noon";
                case Enums.TimeInterval.Afternoon:
                    return "Afternoon";
                case Enums.TimeInterval.Dusk:
                    return "Dusk";
                case Enums.TimeInterval.Evening:
                    return "Evening";
                case Enums.TimeInterval.Midnight:
                    return "Midnight";
                case Enums.TimeInterval.LateNight:
                    return "Late Night";
            }
            return "null";
        }
    }
}
