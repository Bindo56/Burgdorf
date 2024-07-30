using System;
using TMPro;
using UnityEngine;

public class BattleHeroData : MonoBehaviour
{
    public Hero heroData;
    private Enemy assignedEnemy;
    private int currentHealth;
    bool isAlive = true;

    [SerializeField] TextMeshProUGUI healthUI;

    public event Action<BattleHeroData> OnDeath;
    void Start()
    {
        currentHealth = heroData.playerHealth;
    }
    private void Update()
    {
        healthUI.text = "Health :- " + currentHealth.ToString("##");
        IsAlive();
    }
    public void AssignEnemy(Enemy enemy)
    {
        assignedEnemy = enemy;
    }
    public Enemy GetAssignedEnemy()
    {
        return assignedEnemy;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{heroData.heroName} takes {damage} damage, remaining health: {currentHealth}");
        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }
    public bool IsAlive()
    {
        return isAlive;
    }

    public void Attack(Enemy enemy)
    {
        enemy.TakeDamage(heroData.attackPower);
    }

    private void Die()
    {
        isAlive = false;
        OnDeath?.Invoke(this);
        Debug.Log($"{heroData.heroName} has died.");
        Destroy(gameObject);
    }
}
