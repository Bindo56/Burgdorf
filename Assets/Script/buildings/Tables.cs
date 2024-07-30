using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tables : MonoBehaviour
{
    public static Tables Instance;

    [SerializeField] Player player;

    [SerializeField] private Transform buildingTransform;
    [SerializeField] private Sprite[] levelSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform canvasPopup;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] AudioSource upgradSound;

    private int buildingLevel = 0;
    private int[] upgradeCosts = { 10, 30, 50, 100 };
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Hide();
        player.OntableUpgrade += Player_OntableUpgrade;
       
        LoadBuildingLevel();
        UpdateBuildingSprite();
    }

   

    private void Player_OntableUpgrade(object sender, System.EventArgs e)
    {
        UpgradeBuilding();
    }

    int currentUpgradeCost;

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindAnyObjectByType<Player>();
        }

        if (buildingLevel == 3)
        {
            cost.text = " Max Upgrade";
            return;
        }
        currentUpgradeCost = upgradeCosts[buildingLevel];
        
    }
    private void UpgradeBuilding()
    {
        if (buildingLevel < levelSprites.Length && buildingLevel < 3)
        {
            currentUpgradeCost = upgradeCosts[buildingLevel];

            if (GameManager.Instance.SpendCoins(currentUpgradeCost))
            {
                upgradSound.Play();
                buildingLevel++;
                SaveBuildingLevel();
                UpdateBuildingSprite();
                GameManager.Instance.AddCoins(5);
                GameManager.Instance.notification.text = " 5 Coins Paid By civilian ";
                Invoke("hideNotification", 3);
            }
            else
            {
                Debug.Log("Not enough Coins to upgrade");
                cost.text = " Not enough Coin to Upgrade";
            }
        }
        else
        {
            cost.text = "Max Upgrade";
            Debug.Log("MaxUpgrade");
        }
    }
    private void hideNotification()
    {
        GameManager.Instance.notification.text = "";
    }

    private void UpdateBuildingSprite()
    {
        if (buildingLevel >= 0 && buildingLevel < levelSprites.Length)
        {
            //  spriteRenderer = buildingTransform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = levelSprites[buildingLevel];
        }
    }

    private void SaveBuildingLevel()
    {
        PlayerPrefs.SetInt("BuildingLevel", buildingLevel);
    }

    private void LoadBuildingLevel()
    {
        buildingLevel = PlayerPrefs.GetInt("BuildingLevel", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("BuildingLevel");
    }
    public Transform getPOs()
    {
        return gameObject.transform;
    }
    public void Show()
    {

        canvasPopup.gameObject.SetActive(true);
        cost.text = " House Upgrade Cost = " + currentUpgradeCost + " Upgrade to Increase Happiness Level of Villagers";

    }
    public void Hide()
    {
        canvasPopup.gameObject.SetActive(false);

    }
}
