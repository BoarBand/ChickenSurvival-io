using System;

namespace SurvivalChicken.Structures
{
    public struct Damage
    {
        private int _damage;
        private int _critDamageChance;
        private int _critDamageValue;

        public bool IsCritical { get; private set; }
        public int TotalDamage { get; private set; }

        public Damage(int damage, int critDamageChance, int critDamageValue)
        {
            _damage = damage;
            _critDamageChance = critDamageChance;
            _critDamageValue = critDamageValue;

            IsCritical = false;
            TotalDamage = _damage;
            
            if(CriticalDamage(out int finalDamage))
            {
                TotalDamage = finalDamage;
                IsCritical = true;
            }
        }

        public Damage(int damage)
        {
            _damage = damage;
            _critDamageChance = 0;
            _critDamageValue = 0;

            IsCritical = false;
            TotalDamage = _damage;
        }

        private bool CriticalDamage(out int damage)
        {
            Random random = new Random();
            int chance = random.Next(0, 100);

            if(chance <= _critDamageChance)
            {
                damage = (int)(_damage + (_damage * (_critDamageValue / 100f)));
                return true;
            }
            else
            {
                damage = _damage;
                return false;
            }
        }
    }
}
