﻿using NueGames.Card;
using NueGames.Characters;
using NueGames.Combat;
using NueGames.Data.Collection;
using NueGames.Enums;
using UnityEngine;

namespace NueGames.Action
{
    /// <summary>
    /// 給予傷害
    /// </summary>
    public class DamageAction : GameActionBase
    {
        public override GameActionType ActionType => GameActionType.Damage;
        
   
        /// <summary>
        /// 執行遊戲行為的功能
        /// </summary>
        protected override void DoMainAction()
        {
            foreach (var target in TargetList)
            {
                PlaySpawnTextFx($"{DamageInfo.GetAfterBlockDamage()}", target);
            
                target.BeAttacked(DamageInfo);
            }
            
        }
    }
}