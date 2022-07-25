using System;
using System.Collections.Generic;
using UnityEngine;

namespace MerchantOfBohemia
{
    public static class EventHandler
    {
        #region TimeEvents
        
        // Advance Game Minute
        public static event Action<int, Enums.Season, int, Enums.TimeInterval, int, int> AdvanceGameMinuteEvent;

        public static void CallAdvanceGameMinuteEvent(int gameYear, Enums.Season gameSeason, int gameDay, Enums.TimeInterval timeInterval, int gameHour, int gameMinute)
        {
            AdvanceGameMinuteEvent?.Invoke(gameYear, gameSeason, gameDay, timeInterval, gameHour, gameMinute);
        }
        
        // Advance Game Hour
        public static event Action<int, Enums.Season, int, Enums.TimeInterval, int, int> AdvanceGameHourEvent;

        public static void CallAdvanceGameHourEvent(int gameYear, Enums.Season gameSeason, int gameDay, Enums.TimeInterval timeInterval, int gameHour, 
            int gameMinute)
        {
            AdvanceGameHourEvent?.Invoke(gameYear, gameSeason, gameDay, timeInterval, gameHour, gameMinute);
        }
        
        // Advance Game Day
        public static event Action<int, Enums.Season, int, Enums.TimeInterval, int, int> AdvanceGameDayEvent;

        public static void CallAdvanceGameDayEvent(int gameYear, Enums.Season gameSeason, int gameDay, Enums.TimeInterval timeInterval, int gameHour,
            int gameMinute)
        {
            AdvanceGameDayEvent?.Invoke(gameYear, gameSeason, gameDay, timeInterval, gameHour, gameMinute);
        }
        
        // Advance Game Season
        public static event Action<int, Enums.Season, int, Enums.TimeInterval, int, int> AdvanceGameSeasonEvent;

        public static void CallAdvanceGameSeasonEvent(int gameYear, Enums.Season gameSeason, int gameDay, Enums.TimeInterval timeInterval, int gameHour,
            int gameMinute)
        {
            AdvanceGameSeasonEvent?.Invoke(gameYear, gameSeason, gameDay, timeInterval, gameHour, gameMinute);
        }
        
        // Advance Game Year
        public static event Action<int, Enums.Season, int, Enums.TimeInterval, int, int> AdvanceGameYearEvent;

        public static void CallAdvanceGameYearEvent(int gameYear, Enums.Season gameSeason, int gameDay, Enums.TimeInterval timeInterval, int gameHour,
            int gameMinute)
        {
            AdvanceGameYearEvent?.Invoke(gameYear, gameSeason, gameDay, timeInterval, gameHour, gameMinute);
        }
        
        #endregion

        #region PlayerInputs
        
        // Pointer Click
        public static event Action<Vector3> PointerClickEvent;

        public static void CallPointerClickEvent(Vector3 mousePosition)
        {
            PointerClickEvent?.Invoke(mousePosition);
        }
        
        // Player Selected
        
        public static event Action<GameObject> PlayerSelectedEvent;

        public static void CallPlayerSelectedEvent(GameObject player)
        {
            PlayerSelectedEvent?.Invoke(player);
        }
        
        // Terrain Selected

        public static event Action<GameObject> TerrainSelectedEvent;

        public static void CallTerrainSelectedEvent(GameObject hex)
        {
            TerrainSelectedEvent?.Invoke(hex);
        }

        #endregion

        #region Movement
        
        // Movement Started
        public static event Action<PlayerMovement> MovementStartedEvent;

        public static void CallMovementStartedEvent(PlayerMovement playerMovement)
        {
            MovementStartedEvent?.Invoke(playerMovement);
        }

        // Movement Finished
        public static event Action<PlayerMovement> MovementFinishedEvent;

        public static void CallMovementFinishedEvent(PlayerMovement playerMovement)
        {
            MovementFinishedEvent?.Invoke(playerMovement);
        }
        
        #endregion
    }
}
