using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class PlayerBattleHud : MonoBehaviour
    {
        public Text entityName;
        public Text entityLvl;
        public HealthBar hp;
        public HealthBar mana;
        public HealthBar endu;

        public void InitHUD(Player player)
        {
            entityName.text = player.name;
            entityLvl.text = $"Level {player.lvl}"; 
            hp.SetMaxHealth(player.health.maxHealth); 
            mana.SetMaxHealth(player._mana.maxMana); 
            endu.SetMaxHealth(player._endurance.maxEndurance);
        }

        public void SetHUD(Player player)
        {
            hp.SetHealth(player.health.health);
            mana.SetHealth(player._mana.mana);
            endu.SetHealth(player._endurance.endurance);
        }
    }
}
