using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private BattleHeroData assignedHero;
    [SerializeField] EnemySO enemySO;
    private int currentHealth;
    bool isAlive = true;
    [SerializeField] TextMeshProUGUI healthtext;

    public event Action<Enemy> OnDeath;


    public void AssignHero(BattleHeroData hero)
    {
        assignedHero = hero;
        Debug.Log($"Hero {hero.name} assigned to {gameObject.name}");

    }
    public BattleHeroData GetAssignedHero()
    {
        return assignedHero;
    }

    void Start()
    {
        currentHealth = enemySO.Health;
        InvokeRepeating("Fight", 1f, 1f); // Start fighting every 1 second
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{enemySO.enemyName} takes {damage} damage, remaining health: {currentHealth}");
        if (currentHealth <= 0)
        {
            isAlive = false;
            Die();
        }
    }

    private void Update()
    {
        healthtext.text = "Enemy Health :- " + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            isAlive = false;
            Debug.Log("die");
            GameManager.Instance.AddCoins(enemySO.Coin);
            Destroy(gameObject);
            Die();
        }
    }
    public bool IsAlive()
    {
        return isAlive;
    }

    public void Attack()
    {
        if (assignedHero != null)
        {
            assignedHero.TakeDamage(enemySO.attackPower);
        }
    }
    private void Fight()
    {
        if (assignedHero != null)
        {
            Attack();
            assignedHero.Attack(this);
        }
    }

    private void Die()
    {
        if(!isAlive) { return; }
        isAlive = false;
        GameManager.Instance.AddCoins(enemySO.Coin);
        Debug.Log($"{enemySO.enemyName} has died.");
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

}
