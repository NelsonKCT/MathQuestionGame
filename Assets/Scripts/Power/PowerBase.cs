﻿using GameListener;
using NueGames.Action;
using NueGames.Card;
using NueGames.Characters;
using NueGames.Combat;
using NueGames.Enums;
using NueGames.Managers;
using NueGames.Parameters;
using UnityEngine;

namespace NueGames.Power
{
    /// <summary>
    /// 能力（ex: 力量、易傷、中毒）的基底 class
    /// </summary>
    public abstract class PowerBase : GameEventListener
    {
        /// <summary>
        /// 能力類型
        /// </summary>
        // TODO 改名成 PowerName
        // PowerType 應該是 Buff, DeBuff
        public abstract PowerType PowerType  { get;}
        /// <summary>
        /// 能力數值
        /// </summary>
        public int Amount;
        /// <summary>
        /// 能力是否被觸發
        /// </summary>
        public bool IsActive;
        /// <summary>
        /// 回合結束時數值 - 1
        /// </summary>
        public bool DecreaseOverTurn;
        /// <summary>
        /// 永久能力(本場戰鬥)
        /// </summary>
        public bool IsPermanent; 
        /// <summary>
        /// 數值可以是負數
        /// </summary>
        public bool CanNegativeStack;
        /// <summary>
        /// 回合結束時清除能力
        /// </summary>
        public bool ClearAtNextTurn;
        /// <summary>
        /// 能力持有者
        /// </summary>
        public CharacterBase Owner;
        /// <summary>
        /// 計數器，用來計算如回合數、答對題數、使用卡片張數等等
        /// </summary>
        public int Counter;
        /// <summary>
        /// 發動事件，所需的計數
        /// </summary>
        public int NeedCounter;
        
        protected GameActionExecutor GameActionExecutor => GameActionExecutor.Instance;
        
        
        #region SetUp

        public virtual void SetOwner(CharacterBase owner)
        {
            Owner = owner;
        }
        
        #endregion
        
        

        #region 能力控制
        
        /// <summary>
        /// 將能力 x 倍數
        /// </summary>
        public virtual void MultiplyPower(int multiplyAmount)
        {
            int addAmount = Mathf.RoundToInt(Amount * (multiplyAmount - 1));
            StackPower(addAmount);
        }
        
        /// <summary>
        /// 增加能力數值
        /// </summary>
        /// <param name="stackAmount"></param>
        public virtual void StackPower(int stackAmount)
        {
            if (IsActive)
            {
                Amount += stackAmount;
                Owner?.CharacterStats.OnPowerChanged?.Invoke(PowerType, Amount);
                
            }
            else
            {
                Amount = stackAmount;
                IsActive = true;
                Owner?.CharacterStats.OnPowerApplied?.Invoke(PowerType, Amount);
            }

            if (stackAmount > 0)
            {
                Owner?.CharacterStats.OnPowerIncrease?.Invoke(PowerType, stackAmount);
            }

            CheckClearPower();
        }
        

        

        /// <summary>
        /// 檢查要不要清除能力
        /// </summary>
        private void CheckClearPower()
        {
            //Check status
            if (Amount <= 0)
            {
                if (CanNegativeStack)
                {
                    if (Amount == 0 && !IsPermanent)
                        ClearPower();
                }
                else
                {
                    if (!IsPermanent)
                        ClearPower();
                }
            }
        }
        
        /// <summary>
        /// 清除能力
        /// </summary>
        public void ClearPower()
        {
            IsActive = false;
            Amount = 0;
            Owner.CharacterStats.PowerDict.Remove(PowerType);
            Owner.CharacterStats.OnPowerCleared.Invoke(PowerType);
            UnSubscribeAllEvent();
        }

        #endregion


        
        #region 事件觸發
        

        /// <summary>
        /// 回合結束時，更新能力
        /// </summary>
        public void UpdatePowerStatus()
        {
            //One turn only statuses
            if (ClearAtNextTurn)
            {
                ClearPower();
                return;
            }
            
            if (DecreaseOverTurn) 
                StackPower(-1);
            
            //Check status
            if (Amount <= 0)
            {
                if (CanNegativeStack)
                {
                    if (Amount == 0 && !IsPermanent)
                        ClearPower();
                }
                else
                {
                    if (!IsPermanent)
                        ClearPower();
                }
            }
        }

        
        /// <summary>
        /// 當能力改變時
        /// </summary>
        protected virtual void OnPowerChange(){}
        
        /// <summary>
        /// 事件: 當能力數值增加時觸發
        /// </summary>
        protected virtual void OnPowerIncrease(PowerType powerType, int value){}
        
        
        #endregion


        #region 工具

        public bool IsCharacterTurn(TurnInfo info)
        {
            return info.CharacterType == GetOwnerCharacterType();
        }
        
        
        /// <summary>
        /// 取得能力的持有對象的 CharacterType
        /// </summary>
        /// <returns></returns>
        public CharacterType GetOwnerCharacterType()
        {
            return Owner.CharacterType;
        }

        
        

        /// <summary>
        /// 取得 DamageInfo
        /// </summary>
        /// <param name="damageValue"></param>
        /// <param name="fixDamage"></param>
        /// <param name="canPierceArmor"></param>
        /// <returns></returns>
        protected DamageInfo GetDamageInfo(int damageValue, bool fixDamage  = false, bool canPierceArmor  = false)
        {
            DamageInfo damageInfo = new DamageInfo()
            {
                BaseValue = damageValue,
                Target = Owner,
                FixDamage = fixDamage,
                CanPierceArmor = canPierceArmor,
                ActionSource = ActionSource.Power,
                SourcePower = PowerType
            };

            return damageInfo;
        }

        #endregion
        
        
    }
}