using Entities.PlayerScripts;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu (fileName = "new Relique", menuName = "Items/Relique")]
    public class Relique : Item
    {
        public int HpBonus,
            ArmorBonus,
            ManaBonus,
            EnduranceBonus,
            AttaqueBonus;
        
        public override void Use()
        {
            string bonusStat = $"Bonus Stats:\n";
            if (HpBonus != 0) bonusStat += $"HP: +{HpBonus}\n";
            if (ArmorBonus != 0) bonusStat += $"Armor: +{ArmorBonus}\n";
            if (ManaBonus != 0) bonusStat += $"Mana: +{ManaBonus}\n";
            if (EnduranceBonus != 0) bonusStat += $"Endurance: +{EnduranceBonus}\n";
            if (AttaqueBonus != 0) bonusStat += $"Attaque: +{AttaqueBonus}\n";
            
            Debug.Log(bonusStat);
        }

        public void Gain(Player player)
        {
            player.GainMaxEndurance(EnduranceBonus);
            player.GainMaxHp(HpBonus);
            player.GainMaxMana(ManaBonus);
            player.Attack += AttaqueBonus;
            player.Armor += ArmorBonus;
        }
    }
}