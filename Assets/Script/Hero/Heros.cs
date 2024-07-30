using System;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Heros : MonoBehaviour
{
    public static Heros instance;
    public event EventHandler OnHired;
    public event EventHandler OnRoaming;
    public event EventHandler OnBattle;

    [SerializeField] string heroStatus;
    Player player;

    float DestroyHerotimer = 60;
   [SerializeField] float tableTimer = 25;
   [SerializeField] float roamingTimer = 25;
   [SerializeField] float weapontimer = 25;

   

    private void Awake()
    {

    }
    public enum State
    {
        hireingPlace,
        Tables,
        roaming,
        armourStore,
        battle
    }
    public State state;

    [SerializeField] Transform hireUI;
    [SerializeField] Button hireBtn;
    [SerializeField] Button popUp;
    [SerializeField] Hero heroSO;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI attackPower;
    [SerializeField] TextMeshProUGUI health;
   // [SerializeField] Animator anim;


    private void Start()
    {
        AssignRandomPos();
       /* if(anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }*/
        player = FindAnyObjectByType<Player>();
       // player.Onbattle += Player_Onbattle;
        state = State.hireingPlace;

        hireUI.gameObject.SetActive(false);
        hireBtn.onClick.AddListener(() =>
        {
            if(GameManager.Instance.Coins < heroSO.hireCoin)
            {
                Debug.Log("NotEnoughCoin");
                coinText.text = " - ";
                Invoke("Hide", 3);
                return;
            }
            GameManager.Instance.SpendCoins(heroSO.hireCoin);
            Hired();
            GameManager.Instance.DetectHeroes(heroSO);
            Invoke("Hide", 1);
            state = State.Tables;
           

        });

        popUp.onClick.AddListener(() =>
        {
            if (!hired)
            {
              Show();
            }
            else
            {
                ShowHeroStatus();
            }

        });
       
    }
    Vector3 randomTransform;
    private void AssignRandomPos()
    {
        Vector3 pos1 = Tables.Instance.getPOs().position;
        Vector3 pos2 = WeapeonShop.Instance.getPOs().position;
        Vector3 pos3 = WeapeonShop.Instance.getPOs().position;
        Vector3[] possiblePositions = new Vector3[] {  pos2, pos3 ,pos1};

        // Select a random position
        int randomIndex = UnityEngine.Random.Range(0, possiblePositions.Length);
       randomTransform = possiblePositions[randomIndex];
    }
 
   /* private void Player_Onbattle(object sender, EventArgs e)
    {
            state = State.battle;
    }*/
    private void Update()
    {
        DestroyHerotimer -= Time.deltaTime;

       


      
        switch (state)
        {
            case State.hireingPlace:

               
                transform.position = Vector2.MoveTowards(transform.position, HireingPlace.instance.getPOs().position  , Time.deltaTime);
                
                
                if (DestroyHerotimer <= 0)
                {
                    Destroy(gameObject);
                }
                break;
            case State.Tables:

                OnHired?.Invoke(this, EventArgs.Empty);
                transform.position = Vector2.MoveTowards(transform.position, Tables.Instance.getPOs().position, Time.deltaTime);
                tableTimer -= Time.deltaTime;
                if (tableTimer <= 0)
                {
                    state = State.roaming;
                    weapontimer = 25;
                }
                break;
            case State.roaming:
                transform.position = Vector2.MoveTowards(transform.position,randomTransform , Time.deltaTime);
                roamingTimer -= Time.deltaTime;
                if (roamingTimer <= 0)
                {
                    state = State.armourStore;
                    weapontimer = 25;
                }
                OnRoaming?.Invoke(this, EventArgs.Empty);
                break;
            case State.armourStore:
                transform.position = Vector2.MoveTowards(transform.position, armourShop.Instance.getPOs().position, Time.deltaTime);
                OnBattle?.Invoke(this, EventArgs.Empty);
                weapontimer -= Time.deltaTime;
                if (weapontimer <= 0)
                {
                    ResetTimer();
                    state = State.Tables;
                }
                break;
            case State.battle:

              //  Debug.Log("switch");
                transform.position = Vector2.MoveTowards(transform.position, battle.instance.GetPos().position, Time.deltaTime);
                break;

        }
    }


    private void ResetTimer()
    {

        tableTimer = 25;
        roamingTimer = 25;

    }

    bool hired = false;
    public void Hired()
    {
        hired = true;
        coinText.text = "Hired";
    }
    private void ShowHeroStatus()
    {
        hireUI.gameObject.SetActive(true);
        coinText.fontSize = 0.3f;
        coinText.text = heroStatus.ToString();
        health.text = heroSO.playerHealth.ToString();
        attackPower.text = heroSO.attackPower.ToString();
        Invoke("Hide", 2);
    }
    public void Show()
    {
        hireUI.gameObject.SetActive(true);
        coinText.text = heroSO.hireCoin.ToString();
        health.text =  heroSO.playerHealth.ToString();
        attackPower.text =  heroSO.attackPower.ToString();
    }
    public void Hide()
    {
        coinText.fontSize = 0.6f;
        hireUI.gameObject.SetActive(false);
    }
}
