using Photon.Pun;
using UnityEngine;

namespace Entities.PlayerScripts
{
    public class Entity : MonoBehaviourPun
    {
        public Health health;
        public int lvl;
        public string name;
        public int armor;
        
        public void TakeDamage(int value) => health.TakeDamage(value - armor < 0 ? 0 : value - armor);
        public void Heal(int value) => health.Heal(value);
        public void GainMaxHp(int value) => health.GainMaxHp(value);
    }
}