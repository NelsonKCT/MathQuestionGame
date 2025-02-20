using System;
using System.Collections;
using System.Collections.Generic;
using NueGames.Characters;
using NueGames.Enums;
using NueGames.Managers;
using UnityEngine;

namespace NueGames.Combat
{
    public class CharacterHighlightController
    {
        private static CombatManager CombatManager => CombatManager.Instance;


        public void ActivateEnemyHighlight(EnemyBase enemyBase)
        {
            enemyBase.EnemyCanvas.SetHighlight(true);
        }
        
        public void OnDraggedCardOutsideHand(ActionTargetType targetTypeTargetType)
        {
            switch (targetTypeTargetType)
            {
                case ActionTargetType.AllEnemies:
                case ActionTargetType.RandomEnemy:
                    ActivateAllEnemyHighlights();
                    break;
                case ActionTargetType.Ally:
                    ActivateAllAllyHighlights();
                    break;
                case ActionTargetType.Enemy:
                case ActionTargetType.WithoutTarget:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetTypeTargetType), targetTypeTargetType, null);
            }
        }

        void ActivateAllEnemyHighlights()
        {
            foreach (var currentEnemy in CombatManager.CurrentEnemiesList)
                currentEnemy.EnemyCanvas.SetHighlight(true);
        }

        void ActivateAllAllyHighlights()
        {
            foreach (var currentAlly in CombatManager.CurrentAlliesList)
                currentAlly.AllyCanvas.SetHighlight(true);
        }
        
        public void DeactivateAllHighlights()
        {
            DeactivateAllyHighlights();
            DeactivateEnemyHighlights();
        }
        
        public void DeactivateAllyHighlights()
        {
            foreach (var currentAlly in CombatManager.CurrentAlliesList)
                currentAlly.AllyCanvas.SetHighlight(false);
        }
        
        public void DeactivateEnemyHighlights()
        {
            foreach (var currentEnemy in CombatManager.CurrentEnemiesList)
                currentEnemy.EnemyCanvas.SetHighlight(false);
        }
    }
}

