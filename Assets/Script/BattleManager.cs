using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] float timerToChangeSence = 25;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] TextMeshProUGUI timer;

    private void Update()
    {
        timerToChangeSence -= Time.deltaTime;
        timer.text = timerToChangeSence.ToString("#") + "   " + " Press Ecs Key to End the Battle ";
        if(timerToChangeSence <= 0 || Input.GetKeyDown(KeyCode.Escape) || enemyManager.enemies.Count == 0 || enemyManager.battleHero.Count == 0)
        {
            SceneManager.LoadScene(0);
           heroManager.Instance.detectedHeroes.Clear();
        }
    }
}
