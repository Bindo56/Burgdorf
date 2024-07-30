using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  
    public static GameManager Instance { get; private set; }
    [SerializeField] private float castRadius;
   // [SerializeField] private heroManager heroManager;
   
    [SerializeField] Transform tutorial;
    public int coins;
    public TextMeshProUGUI notification;
    public int Coins
    {
        get => coins;
        set
        {
            coins = Mathf.Max(0, value); 
        }
    }

    public List<Hero> detectedHeroes;

    private void Awake()
    {
       
      detectedHeroes.Clear();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
   

    void Update()
    {
        Debug.Log(detectedHeroes.Count);
       
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            tutorial.gameObject.SetActive(true);
        }
   

       
    }
    public void AddCoins(int amount)
    {
        if (Coins + amount < 0)
        {
            Debug.LogWarning("Attempted to remove more coins than available. Operation not allowed.");
        }
        else
        {
            Coins += amount;
            Debug.Log($"Added {amount} coins. Total coins: {coins}");
        }
    }
    public bool CanAfford(int amount)
    {
        return coins >= amount;
    }

    public bool SpendCoins(int amount)
    {
        if (CanAfford(amount))
        {
            Coins -= amount;
            Debug.Log($"Spent {amount} coins. Total coins: {Coins}");
            return true;
        }
        else
        {
            Debug.LogWarning("Not enough coins to complete the purchase.");
            return false;
        }
    }

   public void DetectHeroes(Hero heroS)
    {

        if (heroS == null)
        {
            Debug.LogWarning("HeroScriptableObject is null.");
            return;
        }

        Hero hero = heroS;
        if (!heroManager.Instance.detectedHeroes.Contains(hero))
        {
            heroManager.Instance.detectedHeroes.Add(hero);
            Debug.Log($"Added Hero: {hero.heroName}");
            heroManager.Instance.SaveDetectedHeroes();
        }
        else
        {
            Debug.Log($"Hero {hero.heroName} already detected.");
        }


    }
   



   /* private void HeroData(RaycastHit2D hit)
    {
        if (hit.transform.TryGetComponent(out HeroSOData heros))
        {
            // Add hero to detectedHeroes list if not already added
            if (!detectedHeroes.Contains(heros))
            {
                detectedHeroes.Add(heros);
                Debug.Log($"Detected Hero: {heros.heroData.heroName}");

                if (!heroManager.heroes.Contains(heros.heroData))
                {
                    heroManager.heroes.Add(heros.heroData);
                    Debug.Log($"Added Hero to HeroManager: {heros.heroData.heroName}");
                }
                else
                {
                    Debug.Log("alreadyhasthetype");
                }
            }
        }
        else
        {
            Debug.LogWarning("HerosAI component not found on the detected GameObject!");
        }
    }*/

   
}

