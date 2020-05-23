using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Entities;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public enum BattleState
    {
        START,
        PLAYERTURN,
        ENEMYTURN,
        WON,
        LOST
    }

    public class BattleSystem : MonoBehaviour
    {
        #region Variables

        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        public Player player;
        public Ennemy ennemy;

        public EnemyBattleHud enemyBattleHud;
        public PlayerBattleHud playerBattleHud;

        public Text dialogText;

        public BattleState state;

        #endregion

        #region Setup

        void StartCombat()
        {
            state = BattleState.START;
            StartCoroutine(SetupBattle());
        }

        private void OnEnable()
        {
            StartCombat();
        }

        IEnumerator SetupBattle()
        {
            Vector3 playerPos = playerBattleStation.position,
                ennemyPos = enemyBattleStation.position;
            playerPos.y += 1f;
            ennemyPos.y += 0.2f;

            dialogText.text = $"A wild {ennemy.name} approaches ...";
            playerBattleHud.InitHUD(player);
            enemyBattleHud.InitHUD(ennemy);

            yield return new WaitForSeconds(2f);

            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }

        #endregion

        #region Player attacks

        IEnumerator PhysicalAttack()
        {
            dialogText.text = $"You attack {ennemy.name} physically";

            player.UseEndurance(5);
        
            ennemy.TakeDamage(player.attack);
            enemyBattleHud.SetHUD(ennemy);

            yield return new WaitForSeconds(1f);

            if (ennemy.health.health == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
                // ecran fin de combat
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    
        IEnumerator MagicalAttack()
        {
            dialogText.text = $"You attack {ennemy.name} with magic";

            player.UseMana(5);
        
            ennemy.TakeDamage(player.attack);
            enemyBattleHud.SetHUD(ennemy);

            yield return new WaitForSeconds(1f);

            if (ennemy.Hp == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
                // ecran fin de combat
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    
        IEnumerator NeutralAttack()
        {
            dialogText.text = $"You attack {ennemy.name}";

            ennemy.TakeDamage(player.attack / 2);
            enemyBattleHud.SetHUD(ennemy);

            yield return new WaitForSeconds(1f);

            if (ennemy.Hp == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
                // ecran fin de combat
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }

        private void PlayerPhysicalAttack()
        {
            if (player.Endurance > 5)
                StartCoroutine(PhysicalAttack());
        }
    
        private void PlayerMagicalAttack()
        {
            if (player.Mana > 5)
                StartCoroutine(MagicalAttack());
        }

        private void PlayerNeutralAttack()
        {
            StartCoroutine(NeutralAttack());
        }

        #endregion

        #region Turns

        IEnumerator EnemyTurn()
        {
            dialogText.text = $"{ennemy.name} attacks!";

            yield return new WaitForSeconds(1f);

            player.TakeDamage(ennemy.Attaque);
            playerBattleHud.SetHUD(player);

            if (player.health.health == 0)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
                // ecran fin de combat
            }
            else
            {
                state = BattleState.PLAYERTURN;
                StartCoroutine(PlayerTurn());
            }
        }

        IEnumerator PlayerTurn()
        {
            yield return new WaitForSeconds(1f);

            dialogText.text = "Choose an action:";
        }

        #endregion

        #region Buttons

        public void OnPhysicalAttackButton()
        {
            if (state != BattleState.PLAYERTURN)
                return;
            PlayerPhysicalAttack();
        }
    
        public void OnMagicalAttackButton()
        {
            if (state != BattleState.PLAYERTURN)
                return;
            PlayerMagicalAttack();
        }
    
        public void OnNeutralAttackButton()
        {
            if (state != BattleState.PLAYERTURN)
                return;
            PlayerNeutralAttack();
        }

        #endregion

        #region End

        IEnumerator EndBattle()
        {
            Destroy(ennemy.gameObject);
        
            if (state == BattleState.WON)
            {
                dialogText.text = $"You defeated {ennemy.name}!";

                yield return new WaitForSeconds(2f);
            }
            else
            {
                dialogText.text = $"You were slain by {ennemy.name}...";

                yield return new WaitForSeconds(2f);

                player.GameOver();
            }
        }

        #endregion

        public void SetFighters(Player player, Ennemy ennemy)
        {
            this.player = player;
            this.ennemy = ennemy;
        }
    }
}