using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public enum Battle {PlayerTurn, EnnemyTurn, Won, Lost}
    public class Système : MonoBehaviour
    {
        public Ennemy Op;
        public Player Pl;
    
        public Battle State;
        
        public bool inputB, inputSpace, action;
    
        // Start is called before the first frame update
        void Start()
        {
            Pl.Start();
            Op.Start();
            State = Battle.PlayerTurn;
        }

        private void Update()
        {
            inputB = Input.GetKey(KeyCode.B);
            inputSpace = Input.GetKey(KeyCode.Space);
            
            if (State == Battle.PlayerTurn)
            {
                action = true;
                if (inputB)
                    PlayerTurnB();
                else if (inputSpace)
                    PlayerTurnS();
            }

            action = false;
        }

        IEnumerator PlayerAttack ()
        {
            Op.TakeDamage(Pl.Attack);
        
            yield return new WaitForSeconds(2f);
        
            if (Op.Hp <= 0)
            {
                State = Battle.Won;
                End();
            }
            else
            {
                State = Battle.EnnemyTurn;
                AdTurn();
            }
        }

        IEnumerator EnemyTurn()
        {
            Pl.TakeDamage(Op.Attaque);
        
            yield return new WaitForSeconds(1f);
        
            if (Pl.Hp <= 0)
            {
                State = Battle.Lost;
                End();
            }
            else
            {
                State = Battle.PlayerTurn;
                PlayerTurn();
            }
        }
        void End()
        {
            if (State == Battle.Lost)
            {
                Pl.GameOver();
            }
            else
            {
                //message combat gagné ;)
            }
        }

        public void PlayerTurn()
        {
            StartCoroutine(PlayerAttack());
        }
    
        public void PlayerTurnB()
        {
            if (Pl.Mana > 0)
            {
                Pl.Mana -= 5;
                Pl.ManaPlayer.Set(Pl.Mana);
                StartCoroutine(PlayerAttack());
            }
        }

        public void PlayerTurnS()
        {
            if (Pl.Endurance > 0)
            {
                Pl.Endurance -= 5;
                Pl.EndurancePlayer.Set(Pl.Endurance);
                StartCoroutine(PlayerAttack());
            }
        }

        public void AdTurn()
        {
            if (State != Battle.EnnemyTurn)
            {
                return;
            }

            StartCoroutine(EnemyTurn());
        }
    }
}