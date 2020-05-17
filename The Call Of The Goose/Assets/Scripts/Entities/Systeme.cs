using System;
using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public enum Battle {PlayerTurn, EnnemyTurn, Won, Lost}
    public class Systeme : MonoBehaviour
    {
        public GameObject player, combat;
        
        public Ennemy Op;
        public Player Pl;
    
        public Battle State;
        
        public bool inputB, inputSpace, action;
    
        // Start is called before the first frame update
        void Start()
        {
            Pl = player.GetComponent<Player>();
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
            Debug.Log("EnnemyTurn");
            
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
            
            EndCombat();
        }

        public void PlayerTurn()
        {
            StartCoroutine(PlayerAttack());
        }
    
        public void PlayerTurnB()
        {
            if (Pl.Mana > 0)
            {
                Pl.UseMana(5);
                Pl.ManaPlayer.Set(Pl.Mana);
                StartCoroutine(PlayerAttack());
            }
            Debug.Log("PlayerTurnB");
        }

        public void PlayerTurnS()
        {
            if (Pl.Endurance > 0)
            {
                Pl.UseEndurance(5);
                Pl.EndurancePlayer.Set(Pl.Endurance);
                StartCoroutine(PlayerAttack());
            }
            Debug.Log("PlayerTurnS");
        }

        public void AdTurn()
        {
            if (State != Battle.EnnemyTurn)
            {
                return;
            }

            StartCoroutine(EnemyTurn());
        }

        public void EndCombat()
        {
            player.GetComponent<PlayerMovement>().Activated = true;
            player.tag = "Player";
            combat.SetActive(false);
            player.GetComponent<Camera>().gameObject.SetActive(true);
            player.GetComponent<Rigidbody2D>().WakeUp();
        }
    }
}