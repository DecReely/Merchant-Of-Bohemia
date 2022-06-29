using UnityEngine;

namespace MerchantOfBohemia
{
    public class TimeManager : SingletonMonoBehaviour<TimeManager>
    {
        private int _gameYear;
        private Enums.Season _gameSeason;
        private int _gameDay;
        private int _gameHour;
        private int _gameMinute;
        private Enums.TimeInterval _timeInterval;

        private bool _gameClockPaused;
        private float _gameTick;

        private void Start()
        { 
            // Starting Conditions
            _gameYear = Settings.StartingYear;
            _gameDay = Settings.StartingDay; 
            _gameHour = Settings.StartingHour; 
            _gameMinute = Settings.StartingMinute;
            _gameSeason = CalculateGameSeason();
            _timeInterval = CalculateTimeInterval();
            
            _gameClockPaused = false;
            _gameTick = 0f;
        }

        private void Update()
        {
            if (!_gameClockPaused)
            {
                GameTick();
            }
        }

        private void GameTick()
        {
            _gameTick += Time.deltaTime;

            if (_gameTick >= Settings.SecondsPerGameMinute)
            {
                _gameTick -= Settings.SecondsPerGameMinute;

                UpdateGameMinute(); //Game uses minutes as the smallest time scale.
            }
        }

        private void UpdateGameMinute()
        {
            _gameMinute++;

            if (_gameMinute >= 60)
            {
                _gameMinute = 0;
                _gameHour++;

                if (_gameHour >= 24)
                {
                    _gameHour = 0;
                    _gameDay++;

                    if (_gameDay % 3 == 2)
                    {
                        int gs = (int)_gameSeason;
                        gs++;
                        _gameSeason = (Enums.Season)gs;

                        if (gs >= (int)Enums.Season.count)
                        {
                            gs = 0;
                            _gameSeason = (Enums.Season)gs;
                        }

                        EventHandler.CallAdvanceGameSeasonEvent(_gameYear, _gameSeason, _gameDay, _timeInterval, _gameHour,
                            _gameMinute);
                    }

                    if (_gameDay > 12) //Every 12 days are equal to a year, so => day = month
                    {
                        _gameYear++;
                        EventHandler.CallAdvanceGameYearEvent(_gameYear, _gameSeason, _gameDay, _timeInterval, _gameHour,
                            _gameMinute);
                        
                        _gameDay = 1;
                    }

                    EventHandler.CallAdvanceGameDayEvent(_gameYear, _gameSeason, _gameDay, _timeInterval, _gameHour, _gameMinute);
                }

                _timeInterval = CalculateTimeInterval();
                EventHandler.CallAdvanceGameHourEvent(_gameYear, _gameSeason, _gameDay, _timeInterval, _gameHour, _gameMinute);
            }

            EventHandler.CallAdvanceGameMinuteEvent(_gameYear, _gameSeason, _gameDay, _timeInterval, _gameHour, _gameMinute);
        }

        private Enums.TimeInterval CalculateTimeInterval()
        {
            switch (_gameHour)
            {
                case >= 5 and < 7:
                    return Enums.TimeInterval.Dawn;
                case >= 7 and < 9:
                    return Enums.TimeInterval.EarlyMorning;
                case >= 9 and < 11:
                    return Enums.TimeInterval.Morning;
                case >= 11 and < 13:
                    return Enums.TimeInterval.Noon;
                case >= 13 and < 17:
                    return Enums.TimeInterval.Afternoon;
                case >= 17 and < 19:
                    return Enums.TimeInterval.Dusk;
                case >= 19 and < 22:
                    return Enums.TimeInterval.Evening;
                case (>= 22 and <= 24) or (>= 0 and < 2):
                    return Enums.TimeInterval.Midnight;
                case >= 2 and < 5:
                    return Enums.TimeInterval.LateNight;
                default:
                    return _timeInterval;
            }
        }

        private Enums.Season CalculateGameSeason()
        {
            switch (_gameDay)
            {
                case 2 or 3 or 4:
                    return Enums.Season.Spring;
                case 5 or 6 or 7:
                    return Enums.Season.Summer;
                case 8 or 9 or 10:
                    return Enums.Season.Autumn;
                case 11 or 12 or 1:
                    return Enums.Season.Winter;
                
                default:
                    return Enums.Season.Winter;
            }
        }
    }
}