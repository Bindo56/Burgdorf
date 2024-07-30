using TMPro;
using UnityEngine;

public class WeapeonShop : MonoBehaviour
{
    public static WeapeonShop Instance;
    [SerializeField] Player player;

    [SerializeField] private Transform buildingTransform;
    [SerializeField] private Sprite[] levelSprites;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI maxUpgradeText;
    [SerializeField] Transform canvasPopup;
    [SerializeField] AudioSource upgradSound;

    private int buildingLevel = 0;
    private int[] upgradeCosts = { 10, 30, 50, 100 };

    // Start is called before the first frame update
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
        canvasPopup.gameObject.SetActive(false);
        player.OnWeaponStoreUpgrade += Player_OnWeaponStoreUpgrade;
       
        LoadBuildingLevel();
        UpdateBuildingSprite();
    }

   

    private void Player_OnWeaponStoreUpgrade(object sender, System.EventArgs e)
    {
        UpgradeBuilding();
    }

    int currentUpgradeCost;
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
        if (buildingLevel == 3)
        {
            cost.text = "Max Upgrade ";

        }
        currentUpgradeCost = upgradeCosts[buildingLevel];
       
    }
    private void UpgradeBuilding()
    {
        if (buildingLevel < levelSprites.Length && buildingLevel < 3)
        {



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
                cost.text = " Not enough Coin to Upgrade";
                Debug.Log("Not enough Coins to upgrade");
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
        return buildingTransform;
    }
   
    public void Show()
    {

        canvasPopup.gameObject.SetActive(true);
        cost.text = " TownHall Upgrade Cost = " + currentUpgradeCost + " Upgrade to Increase Happiness Level of Villagers";

    }
    public void Hide()
    {
        canvasPopup.gameObject.SetActive(false);
       
    }
}
