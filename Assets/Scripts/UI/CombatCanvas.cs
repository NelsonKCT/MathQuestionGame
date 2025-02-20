﻿using System;
using Feedback;
using NueGames.Combat;
using NueGames.Enums;
using NueGames.Managers;
using TMPro;
using UnityEngine;

namespace NueGames.UI
{
    public class CombatCanvas : CanvasBase
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI drawPileTextField;
        [SerializeField] private TextMeshProUGUI discardPileTextField;
        [SerializeField] private TextMeshProUGUI exhaustPileTextField;
        [SerializeField] private TextMeshProUGUI manaTextTextField;
        [SerializeField] private TextMeshProUGUI mathManaTextTextField;
        
        [Header("Panels")]
        [SerializeField] private GameObject combatWinPanel;
        [SerializeField] private GameObject combatLosePanel;

        [Header("FeedBack")] 
        [SerializeField] private IFeedback onMathManaChangeFeedback;

        [SerializeField] private IFeedback onManaChangeFeedback;
 
        public GameObject CombatWinPanel => combatWinPanel;
        public GameObject CombatLosePanel => combatLosePanel;


        #region Setup
        private void Awake()
        {
            CombatWinPanel.SetActive(false);
            CombatLosePanel.SetActive(false);
            SetMathManaText();
            CombatManager.ManaManager.OnGainMana += OnManaChange;
        }

        #endregion

        #region Public Methods
        public void SetPileTexts()
        {
            drawPileTextField.text = $"{CollectionManager.DrawPile.Count.ToString()}";
            discardPileTextField.text = $"{CollectionManager.DiscardPile.Count.ToString()}";
            exhaustPileTextField.text =  $"{CollectionManager.ExhaustPile.Count.ToString()}";
            manaTextTextField.text = $"{GameManager.PersistentGameplayData.CurrentMana.ToString()}/{GameManager.PersistentGameplayData.MaxMana}";
        }

        public void OnMathManaChange()
        {
            SetMathManaText();
            onMathManaChangeFeedback.Play();
        }

        public void OnManaChange(int value)
        {
           onManaChangeFeedback.Play(); 
           manaTextTextField.text = $"{GameManager.PersistentGameplayData.CurrentMana.ToString()}/{GameManager.PersistentGameplayData.MaxMana}";
        }

        private void SetMathManaText()
        {
            int mathMana = CombatManager.GetMainAllyPowerValue(PowerType.MathMana);
            mathManaTextTextField.text = $"{mathMana}";
        }

        public override void ResetCanvas()
        {
            base.ResetCanvas();
            CombatWinPanel.SetActive(false);
            CombatLosePanel.SetActive(false);
        }

        public void EndTurn()
        {
            if (CombatManager.CurrentCombatStateType == CombatStateType.AllyTurn)
                CombatManager.EndTurn();
        }
        #endregion
    }
}