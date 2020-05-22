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

        public GameObject enemyPrefab;
        public GameObject playerPrefab;

        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        public Player player;
        public Ennemy enemy;

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
            GameObject playerGO = Instantiate(playerPrefab, playerPos, Quaternion.identity, playerBattleStation);
            player = playerGO.GetComponent<Player>();
            GameObject enemyGO = Instantiate(enemyPrefab, ennemyPos, Quaternion.identity, enemyBattleStation);
            enemy = enemyGO.GetComponent<Ennemy>();

            dialogText.text = $"A wild {enemy.name} approaches ...";
            playerBattleHud.InitHUD(player);
            enemyBattleHud.InitHUD(enemy);

            yield return new WaitForSeconds(2f);

            state = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }

        #endregion

        #region Player attacks

        IEnumerator PhysicalAttack()
        {
            dialogText.text = $"You attack {enemy.name} physically";

            player.Endurance -= 5;
        
            enemy.TakeDamage(player.attack);
            enemyBattleHud.SetHUD(enemy);

            yield return new WaitForSeconds(1f);

            if (enemy.health.health == 0)
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
            dialogText.text = $"You attack {enemy.name} with magic";

            player.Mana -= 5;
        
            enemy.TakeDamage(player.attack);
            enemyBattleHud.SetHUD(enemy);

            yield return new WaitForSeconds(1f);

            if (enemy.Hp == 0)
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
            dialogText.text = $"You attack {enemy.name}";

            enemy.TakeDamage(player.attack / 2);
            enemyBattleHud.SetHUD(enemy);

            yield return new WaitForSeconds(1f);

            if (enemy.Hp == 0)
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
            dialogText.text = $"{enemy.name} attacks!";

            yield return new WaitForSeconds(1f);

            player.TakeDamage(enemy.Attaque);
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
            Destroy(enemy.gameObject);
        
            if (state == BattleState.WON)
            {
                dialogText.text = $"You defeated {enemy.name}!";

                yield return new WaitForSeconds(2f);
            }
            else
            {
                dialogText.text = $"You were slain by {enemy.name}...";

                yield return new WaitForSeconds(2f);

                player.GameOver();
            }
        }

        #endregion
    }
}