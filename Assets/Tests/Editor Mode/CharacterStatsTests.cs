﻿using NSubstitute;
using NueGames.Characters;
using NueGames.Enums;
using NueGames.Power;
using NUnit.Framework;

namespace Tests.Editor_Mode
{
    public class CharacterStatsTests
    {
        [Test]
        public void gain_10_damage()
        {
            // Arrange
            var maxHealth = 100;
            var targetStats = new CharacterStats(maxHealth,  null);
            var damage = 10;
        
            // Act
            targetStats.Damage(10);
        
            // Assert
            var currentHealth = targetStats.CurrentHealth;
            var expectHealth = 90;
            Assert.AreEqual(expectHealth, currentHealth);
        }

        [Test]
        [TestCase(PowerType.Strength, 3)]
        [TestCase(PowerType.Block, 5)]
        public void apply_power(PowerType powerType, int value)
        {
            // Arrange
            var characterStats = new CharacterStats(10,  null);
            // TODO 需要設置 Character，但 Character 是 monobehavior，無法使用Substitute.For
            
            // Act
            characterStats.ApplyPower(powerType, value);
            
            // Assert
            bool havePower = characterStats.PowerDict.ContainsKey(powerType);
            int powerValue = characterStats.PowerDict[powerType].Value;
            int expectValue = value;
            
            Assert.IsTrue(havePower);
            Assert.AreEqual(powerValue, expectValue);
        }
    }
}