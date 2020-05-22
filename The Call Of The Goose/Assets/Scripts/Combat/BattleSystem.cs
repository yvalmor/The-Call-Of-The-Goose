using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Entities;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

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

    IEnumerator PhysicalAttack()
    {
        dialogText.text = $"You attack {enemy.name} physically";

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

    public void PlayerPhysicalAttack()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PhysicalAttack());
    }

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

    public void OnPhysicalAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        PlayerPhysicalAttack();
    }

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
}