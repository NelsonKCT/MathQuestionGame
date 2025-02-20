﻿using System.Collections.Generic;
using NueGames.Characters;
using NueGames.Data.Collection;
using NueGames.Enums;
using NueGames.Managers;
using NueGames.Relic;
using UnityEngine;

namespace NueGames.Parameters
{
    /// <summary>
    /// 遊戲行為(GameAction)所需參數
    /// </summary>
    public class ActionParameters
    {
        #region 必填

        /// <summary>
        /// 行為類別
        /// </summary>
        public GameActionType ActionType;
        /// <summary>
        /// 行為基礎數值
        /// </summary>
        public int BaseValue;
        /// <summary>
        /// 行為目標對象
        /// </summary>
        public List<CharacterBase> TargetList;
        /// <summary>
        /// 行為來源
        /// </summary>
        public ActionSource ActionSource;
        /// <summary>
        /// 行為資料
        /// </summary>
        public ActionData ActionData;
        

        #endregion


        #region 選填

        /// <summary>
        /// 行為產生者
        /// </summary>
        public CharacterBase Self;
        /// <summary>
        /// 卡片資料(卡牌行為才需要)
        /// </summary>
        public CardData CardData;
        /// <summary>
        /// 傷害源自哪一個能力(能力才需要)
        /// </summary>
        public PowerType SourcePower;
        /// <summary>
        /// 傷害源自哪一個遺物(遺物才需要)
        /// </summary>
        public RelicType SourceRelic;
        /// <summary>
        /// 加成數值
        /// </summary>
        public float MultiplierValue;
        /// <summary>
        /// 遊戲資料清單
        /// </summary>
        public ActionDataClip ActionDataClip;

        #endregion

        public ActionParameters()
        {
        }

    }
}