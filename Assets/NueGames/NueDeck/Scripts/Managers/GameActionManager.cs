﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.NueGames.NueDeck.Scripts.Action;
using NueGames.NueDeck.Scripts.Card;
using NueGames.NueDeck.Scripts.Enums;
using NueGames.NueDeck.Scripts.Power;
using UnityEngine;

namespace NueGames.NueDeck.Scripts.Managers
{
    public class GameActionManager : MonoBehaviour
    {
        private List<GameActionBase> actions;
        private GameActionBase currentAction;
        private GameActionBase previousAction;

        private Phase phase;
        [SerializeField]private bool actionIsDone;
        
        private GameActionManager() { }

        public static GameActionManager Instance { get; private set; }

        #region SetUp
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            Initialize();
        }

        void Initialize()
        {
            gameActionClasses = Assembly.GetAssembly(typeof(GameActionBase)).GetTypes()
                .Where(t => typeof(GameActionBase).IsAssignableFrom(t) && t.IsAbstract == false);
            
            actions = new List<GameActionBase>();
            phase = Phase.WaitingOnUser;
            actionIsDone = true;
        }
        

        #endregion

        void Update()
        {
            HandleGameActions();
        }

        #region Action Coroutine
        private void HandleGameActions()
        {
            switch (phase)
            {
                case Phase.WaitingOnUser:
                    HandleWaitingOnUser();
                    break;
                case Phase.ExecutingAction:
                    HandleExecutingAction();
                    break;
            }
        }

        private void HandleWaitingOnUser()
        {
            TryGetNextAction();
        }

        private void HandleExecutingAction()
        {
            if (actionIsDone && currentAction != null)
            {
                DoCurrentAction();
            }
            else
            {
                GetNextAction();
            }
        }
        
        private void DoCurrentAction()
        {
            currentAction.DoAction();
            Debug.Log($"Do action {currentAction.GetType().Name}");
            actionIsDone = false;
            StartCoroutine(DoActionRoutine(currentAction.Duration));
        }

        private IEnumerator DoActionRoutine(float wait)
        {
            yield return new WaitForSeconds(wait);
            actionIsDone = true;
        }
        
        private void GetNextAction()
        {
            previousAction = currentAction;
            currentAction = null;
            Debug.Log($"Get action {currentAction?.GetType().Name}");
            TryGetNextAction();
            if (currentAction == null ) {
                phase = Phase.WaitingOnUser;
            }
        }
        
        
        private void TryGetNextAction()
        {
            if (actions.Any() ) {
                currentAction = actions[0];
                Debug.Log($"Try Get action {currentAction.GetType().Name}");
                actions.RemoveAt(0);
                phase = Phase.ExecutingAction;
            }
        }

        #endregion
        
        private static IEnumerable<Type> gameActionClasses;

        public void AddToTop(GameActionType actionType, CardActionParameter cardActionParameter)
        {
            GameActionBase gameActionBase = GetGameAction(actionType);
            gameActionBase.SetValue(cardActionParameter);
            AddToTop(gameActionBase);
        }
        
        public void AddToBottom(GameActionType actionType, CardActionParameter cardActionParameter)
        {
            GameActionBase gameActionBase = GetGameAction(actionType);
            gameActionBase.SetValue(cardActionParameter);
            AddToBottom(gameActionBase);
        }
        
        public void AddToTop(GameActionBase action) {
            actions.Insert(0, action);
        }
        
        public void AddToBottom(GameActionBase action) {
            actions.Add(action);
        }

        private GameActionBase GetGameAction(GameActionType actionType)
        {
            string gameActionName = actionType.ToString() + "Action";
            foreach (var powerBase in gameActionClasses)
            {
                if (powerBase.Name == gameActionName)
                {
                    return Activator.CreateInstance(powerBase) as GameActionBase;
                }
            }
            
            Debug.LogError($"沒有 GameAction {gameActionName} 的 Class");
            return null;
        }
    }

    public enum Phase
    {
        WaitingOnUser,
        ExecutingAction
    }
}