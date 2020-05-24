using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Entities;
using Entities.PlayerScripts;
using Item;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


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

        public Transform playerBattleStation, ennemyBattleStation;
        private Transform oldPosition;

        public Player player;
        public Ennemy ennemy;

        public EnemyBattleHud ennemyBattleHud;
        public PlayerBattleHud playerBattleHud;

        public Text dialogText;

        public BattleState state;

        #endregion

        #region Setup

        private void Start()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            foreach (GameObject o in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (o.GetComponent<Player>().IsMine())
                    player = o.GetComponent<Player>();
            }
        }

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
            Vector3 newPosition = ennemyBattleStation.position;

            newPosition.x += 0.1f;
            newPosition.y -= 1f;
            
            ennemy.transform.position = newPosition;
            
            dialogText.text = $"A wild {ennemy.name} approaches ...";
            playerBattleHud.InitHUD(player);
            ennemyBattleHud.InitHUD(ennemy);

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
            ennemyBattleHud.SetHUD(ennemy);

            yield return new WaitForSeconds(1f);

            if (ennemy.health.health == 0)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
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
            ennemyBattleHud.SetHUD(ennemy);

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
            ennemyBattleHud.SetHUD(ennemy);

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

        public void OnFleeButton()
        {
            if (state != BattleState.PLAYERTURN)
                return;
            StartCoroutine(Random.Range(0f, 1f) < 0.8f ? EndBattle() : CouldntFlee());
        }

        IEnumerator CouldntFlee()
        {
            dialogText.text = "You couldn't flee!";
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }

        #endregion

        #region End

        IEnumerator EndBattle()
        {
            if (state == BattleState.WON)
            {
                dialogText.text = $"You defeated {ennemy.name}!";

                yield return new WaitForSeconds(2f);

                int rand1 = Random.Range(0, 1);
                Consumable drop_c = ennemy.c_loot[Random.Range(0, ennemy.c_loot.Length)];
                Relique r_drop = ennemy.loot[Random.Range(0, ennemy.loot.Length)];

                if (rand1 == 0)
                {
                    player.AddToInventory(drop_c);
                    dialogText.text = $"You looted a {drop_c.name}";
                }
                else
                    dialogText.text = "You looted no consumable";

                if (Random.Range(0, 4) < 2)
                {
                    player.AddRelicToInventory(r_drop);
                    if (rand1 == 0)
                        dialogText.text += $" and a {r_drop.name}";
                    else
                        dialogText.text = $"You looted a {r_drop.name}";
                }
                else
                    dialogText.text += " and no relic";
                
                yield return new WaitForSeconds(2f);
                player.Gold += ennemy.gold_loot;
                dialogText.text = $"You also found {ennemy.gold_loot} gold";
                
                yield return new WaitForSeconds(1.5f);
                player.EndFight();
            }
            else if (state == BattleState.LOST)
            {
                dialogText.text = $"You were slain by {ennemy.name}...";

                yield return new WaitForSeconds(2f);

                player.GameOver();
            }
            else if (!ennemy.Ennemies.Boss)
            {
                dialogText.text = "You flee!";
                
                yield return new WaitForSeconds(2f);
                Destroy(ennemy.gameObject);
                player.EndFight();
            }

            player.GetComponent<CameraControl>().disabled = false;
            Vector3 scale = player.transform.localScale;
            scale.x /= 1.5f;
            scale.y /= 1.5f;
            player.transform.localScale = scale;
        }

        #endregion

        public void SetFighters(Player player, Ennemy ennemy)
        {
            this.player = player;
            this.ennemy = ennemy;
        }
    }
}
