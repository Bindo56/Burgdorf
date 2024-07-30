using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    //  [SerializeField] private heroManager heroManager;
    [SerializeField] private GameObject[] enemyPrefab;

    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> battleHero = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        
       
    }
    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
               

               
            }
        }

        for (int i = battleHero.Count - 1; i >= 0; i--)
        {
            if (battleHero[i] == null)
            {
                battleHero.RemoveAt(i);
               
            }
        }
    }

    private void SpawnEnemies()
    {
        heroManager.Instance.LoadDetectedHeroes();


        for (int i = 0; i < heroManager.Instance.saveHeros.Count; i++)
        {

            Debug.Log(heroManager.Instance.saveHeros.Count);
            Hero detectedHero = heroManager.Instance.saveHeros[i];

            {
                int enemyIndex = i % enemyPrefab.Length;

                // Spawn enemy
                GameObject enemy = Instantiate(enemyPrefab[enemyIndex], GetRandomSpawnPositionEnemy(), Quaternion.identity);
                enemies.Add(enemy);

                // Spawn hero
                GameObject heroInstance = Instantiate(detectedHero.heroOut.gameObject, GetRandomSpawnPositionHero(), Quaternion.identity);
                battleHero.Add(heroInstance);

                // Assign hero to enemy
                var enemyComponent = enemy.GetComponent<Enemy>();
                var heroComponent = heroInstance.GetComponent<BattleHeroData>();

                AssignHeroToEnemy(heroComponent, enemyComponent);

                Debug.Log($"Assigned Hero: {detectedHero.heroName} to Enemy: {enemy.name}");
            }

            // heroManager.Instance.detectedHeroes.Clear();
        }
    }
        /* private void HeroComponent_OnDeath(BattleHeroData obj)
         {
             var enemy = obj.GetAssignedEnemy();
             if (enemy != null && enemy.IsAlive())
             {
                 ReassignEnemy(enemy);
             }
         }*/

        /*  private void EnemyComponent_OnDeath(Enemy obj)
          {
              var hero = obj.GetAssignedHero();
              if (hero != null && hero.IsAlive())
              {
                  ReassignHero(hero);
              }
          }*/

        private void AssignHeroToEnemy(BattleHeroData hero, Enemy enemy)
        {
            hero.AssignEnemy(enemy);
            enemy.AssignHero(hero);
        }

        /*  private void ReassignHero(BattleHeroData hero)
          {
              foreach (var enemyObj in enemies)
              {
                  var enemy = enemyObj.GetComponent<Enemy>();
                  if (enemy != null && enemy.IsAlive() && enemy.GetAssignedHero() == null)
                  {
                      AssignHeroToEnemy(hero, enemy);
                      Debug.Log($"Reassigned Hero: {hero.name} to Enemy: {enemy.name}");
                      break;
                  }
              }
          }*/

        /* private void ReassignEnemy(Enemy enemy)
         {
             foreach (var heroObj in battleHero)
             {
                 var hero = heroObj.GetComponent<BattleHeroData>();
                 if (hero != null && hero.IsAlive() && hero.GetAssignedEnemy() == null)
                 {
                     AssignHeroToEnemy(hero, enemy);
                     Debug.Log($"Reassigned Enemy: {enemy.name} to Hero: {hero.name}");
                     break;
                 }
             }
         }*/

        private Vector3 GetRandomSpawnPositionHero()
        {
            return new Vector3(Random.Range(-1, -2f), Random.Range(2, -2), 0); // Example random position
        }

        private Vector3 GetRandomSpawnPositionEnemy()
        {
            return new Vector3(Random.Range(0, 1), Random.Range(-2, 2), 0); // Example random position
        }
}

