﻿using System;
using System.Collections.Generic;
using NueGames.Data.Characters;
using NueGames.Enums;
using UnityEngine;

namespace NueGames.Data.Encounter
{
    /// <summary>
    /// 遭遇一場戰鬥的敵人清單
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "Enemy Encounter", menuName = "NueDeck/Encounter/EnemyEncounter")]
    public class EnemyEncounter : EncounterBase
    {
        public EnemyEncounterName encounterName;
        public List<EnemyCharacterData> enemyList;
    }

    /// <summary>
    /// 敵人遭遇名稱
    /// </summary>
    /// TODO 撰寫單元測試檢查
    public enum EnemyEncounterName
    {
        // Slay Of Spire (殺戮尖塔)
        SmallSmiles = 1001, // 小史萊姆
        Cultist = 1002, // 邪教徒
        JawWorm = 1003, // 顎蟲
        
        LotsOfSlimes = 1011, // 一大堆史萊姆
        TwoJawWorm = 1012, // 兩隻顎蟲
        
        GremlinNob = 1080, // 大惡魔
    }
    
    
}