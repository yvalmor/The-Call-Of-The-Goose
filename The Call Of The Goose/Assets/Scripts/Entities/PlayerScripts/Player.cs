using System;
using System.Collections.Generic;
using Item;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.PlayerScripts
{
    public class Player : MonoBehaviourPun
    {
        public Health _health;
        public Mana _mana;
        public Endurance _endurance;
        public Inventory Inventory;
        public RelicInventory RelicInventory;

        public int armor;
        private int lvl = 1;
        private int exp;
        private int gold;
        public int attack;
        public new string name;
        public HealthPoint HPPlayer;
        public HealthPoint ManaPlayer;
        public HealthPoint EndurancePlayer;
        public int floor;
        private int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up

        private void Awake()
        {
            if (!PhotonNetwork.IsConnected) return;
            
            Inventory.player = gameObject;
            RelicInventory.player = gameObject;
        }

        public int Hp => _health.health;
        public int Mana => _mana.mana;
        public int Endurance => _endurance.endurance;

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public int Attack
        {
            get => attack;
            set => attack = value;
        }

        public void TakeDamage(int value) => _health.TakeDamage(value);
        public void Heal(int value) => _health.Heal(value);
        public void GainMaxHp(int value) => _health.GainMaxHp(value);
        
        public void RegenMana(int value) => _mana.RegenMana(value);
        public void UseMana(int value) => _mana.UseMana(value);
        public void GainMaxMana(int value) => _mana.GainMaxMana(value);
        
        public void RegenEndurance(int value) => _endurance.RegenEndurance(value);
        public void UseEndurance(int value) => _endurance.UseEndurance(value);
        public void GainMaxEndurance(int value) => _endurance.GainMaxEndurance(value);
        
        public bool GainExp(int gain)
        {
            exp += gain;
            if (lvl < 10 && exp >= expTreshold[lvl - 1])
            {
                exp -= expTreshold[lvl - 1];
                lvl++;
                return true;
            }

            return false;
        }

        public void AddToInventory(Consumable consumable) => Inventory.Add(consumable);
        public void AddRelicToInventory(Relique relique) => RelicInventory.Add(relique);

        public void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }

        public void LaunchFight(Ennemy ennemy)
        {
            
        }

        public bool IsMine()
        {
            return !PhotonNetwork.IsConnected || photonView.IsMine;
        }
    }
}
