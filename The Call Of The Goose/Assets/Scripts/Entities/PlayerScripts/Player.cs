﻿using System;
using System.Collections.Generic;
using Item;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.PlayerScripts
{
    public class Player : Entity
    {
        public Mana _mana;
        public Endurance _endurance;
        public Inventory Inventory;
        public RelicInventory RelicInventory;
        
        private int exp;
        private int gold;
        public int attack;
        public HealthPoint ManaPlayer;
        public HealthPoint EndurancePlayer;
        private int[] expTreshold = {100, 164, 268, 441, 723, 1186, 1945, 3190, 5233}; // exp nécessaire pour lvl up

        public GameObject Combat;
        
        private void Awake()
        {
            if (!PhotonNetwork.IsConnected) return;
            
            Inventory.player = gameObject;
            RelicInventory.player = gameObject;
            Combat = GameObject.Find("Combat");
            Combat.SetActive(false);
        }

        public int Hp{
            get => health.health;
            set => health.health = value;
        }
        public int Mana
        {
            get => _mana.mana;
            set => _mana.mana = value;
        }
        public int Endurance
        {
            get => _endurance.endurance;
            set => _endurance.endurance = value;
        }

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

        public void TakeDamage(int value) => health.TakeDamage(value);
        public void Heal(int value) => health.Heal(value);
        public void GainMaxHp(int value) => health.GainMaxHp(value);
        
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

        public void BuyItem(Item.Item item)
        {
            if (gold < item.price)
                return;

            gold -= item.price;
            
            switch (item)
            {
                case Consumable consumable:
                    AddToInventory(consumable);
                    break;
                case Relique relique:
                    AddRelicToInventory(relique);
                    break;
            }
        }

        public void AddToInventory(Consumable consumable) => Inventory.Add(consumable);
        public void AddRelicToInventory(Relique relique) => RelicInventory.Add(relique);

        public void GameOver()
        {
            SceneManager.LoadScene("Game Over");
        }

        public void BeginFight(Ennemy ennemy)
        {
            
        }

        public bool IsMine()
        {
            return !PhotonNetwork.IsConnected || photonView.IsMine;
        }
    }
}
