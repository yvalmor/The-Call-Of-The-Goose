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
        public Ennemis Op;
        public PlayerMonobehavior Pl;
    
        public Battle State;
    
        // Start is called before the first frame update
        void Start()
        {
            Pl.Start();
            Op.Start();
            State = Battle.PlayerTurn;
        }

        private void Update()
        {
            if (State == Battle.PlayerTurn && Input.GetKeyDown(KeyCode.B))
            {
                PlayerTurnB();
            }

            if (State == Battle.PlayerTurn && Input.GetKeyDown(KeyCode.Space))
            {
                PlayerTurnS();
            }
        }

        IEnumerator PlayerAttack ()
        {
            Op.TakeDamage(Pl.Attaque);
        
            yield return new WaitForSeconds(2f);
        
            if (Op.HP <= 0)
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
            Pl.TakeDamage(Op.Attaque1);
        
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
                //messsage combat perdu :(
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
                Pl.ManaPlayer.SetHp(Pl.Mana);
                StartCoroutine(PlayerAttack());
            }
        }

        public void PlayerTurnS()
        {
            if (Pl.Endurance > 0)
            {
                Pl.Endurance -= 5;
                Pl.EndurancePlayer.SetHp(Pl.Endurance);
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