﻿using NueGames.Action;
using NueGames.Card;
using NueGames.Enums;
using NueGames.Parameters;
using RandomTool;

namespace Action
{
    /// <summary>
    ///  隨機行動
    /// </summary>
    public class UseRandomAction : GameActionBase
    {
        public override GameActionType ActionType => GameActionType.UseRandom;
        private RandomActionData _randomActionData;
        
        public override void SetValue(ActionParameters parameters)
        {
            base.SetValue(parameters);
            _randomActionData = parameters.CardData.RandomActionData;
        }

        protected override void DoMainAction()
        {
            GameActionBase action = RandomGameActionGenerator.GetRandomAction(_randomActionData, ActionParameters);
            action.DoAction();
        }
    }
}