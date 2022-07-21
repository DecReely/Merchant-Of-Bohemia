using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MerchantOfBohemia
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public static Enums.GameState GameState;
        public bool asd;
        private void OnEnable()
        {
            EventHandler.MovementStartedEvent += ChangeGameStateToTraveling;
            EventHandler.MovementFinishedEvent += ChangeGameStateToIdle;
        }

        private void OnDisable()
        {
            EventHandler.MovementStartedEvent -= ChangeGameStateToTraveling;
            EventHandler.MovementFinishedEvent -= ChangeGameStateToIdle;
        }

        private void ChangeGameStateToIdle(Unit player)
        {
            GameState = Enums.GameState.Idle;
        }

        private void ChangeGameStateToTraveling(Unit player)
        {
            GameState = Enums.GameState.Traveling;
        }
    }
}
