﻿using NueGames.Card;
using NueGames.Characters;
using NueGames.Combat;
using NueGames.Enums;
using NueGames.Relic;
using UnityEngine;

namespace NueGames.Parameters
{
    public class DamageInfo
    {
        /// <summary>
        /// 傷害來源
        /// </summary>
        /// <returns></returns>
        public ActionSource ActionSource;
        /// <summary>
        /// 傷害對象
        /// </summary>
        public CharacterBase Target;
        /// <summary>
        /// 基礎傷害數值
        /// </summary>
        public int BaseValue ;
        /// <summary>
        /// 固定傷害，不受狀態影響
        /// </summary>
        public bool FixDamage;
        /// <summary>
        /// 可以穿甲
        /// </summary>
        public bool CanPierceArmor;

        #region 選填

        /// <summary>
        /// 傷害源自哪個角色
        /// </summary>
        public CharacterBase Self;
        /// <summary>
        /// 傷害源自哪一個能力
        /// </summary>
        public PowerType SourcePower;
        /// <summary>
        /// 傷害源自哪一個遺物
        /// </summary>
        public RelicType SourceRelic;
        
        /// <summary>
        /// 加成數值
        /// </summary>
        public float MultiplierValue;
        /// <summary>
        /// 加成數量
        /// </summary>
        public float MultiplierAmount;


        #endregion

        public DamageInfo()
        {
        }

        public DamageInfo(ActionParameters parameters)
        {
            ActionSource = parameters.ActionSource;
            Target = parameters.Target;
            BaseValue = parameters.BaseValue;
            MultiplierValue = parameters.MultiplierValue;
            FixDamage = false; // TODO
            CanPierceArmor = false;
            
            Self = parameters.Self;
            SourcePower = parameters.SourcePower;
            SourceRelic = parameters.SourceRelic;
        }

        /// <summary>
        /// 取得加成數值
        /// </summary>
        /// <returns></returns>
        public int GetAddictionValue()
        {
            Debug.Log( $" GetAddictionValue {BaseValue} + {MultiplierAmount} * {MultiplierValue}"  );
            return Mathf.RoundToInt(BaseValue + MultiplierAmount * MultiplierValue);
        }

        public int GetDamageValue()
        {
            return CombatCalculator.GetDamageValue(this);
        }


        public override string ToString()
        {
            return $"{nameof(ActionSource)}: {ActionSource}, {nameof(Target)}: {Target}, {nameof(BaseValue)}: {BaseValue}, {nameof(FixDamage)}: {FixDamage}, {nameof(CanPierceArmor)}: {CanPierceArmor}, {nameof(Self)}: {Self}, {nameof(SourcePower)}: {SourcePower}, {nameof(SourceRelic)}: {SourceRelic}, {nameof(MultiplierValue)}: {MultiplierValue}, {nameof(MultiplierAmount)}: {MultiplierAmount}";
        }
    }
}