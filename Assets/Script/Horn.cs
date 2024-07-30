using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Horn : MonoBehaviour
{
    public event EventHandler OnchangeHeroPos;


    [SerializeField] AudioClip hornSound;
    [SerializeField] Player player;
    public static Horn instance;

    [SerializeField] Transform[] Heros;
    [SerializeField] Transform spawnedPoint;
    [SerializeField] Vector2 offset;
    [SerializeField] Transform canvasPopup;
    [SerializeField] TextMeshProUGUI cost;


    private System.Random random;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Hide();
        player.OnHerosSpawaned += Player_OnHerosSpawaned;
        random = new System.Random();
    }

    private void Player_OnHerosSpawaned(object sender, System.EventArgs e)
    {
        Debug.Log("HeroSpawned");
        Vector2 currentPos = spawnedPoint.position;

        HashSet<int> chosenIndices = new HashSet<int>();
            
        while (chosenIndices.Count < 2)
        {
            int randomIndex = random.Next(Heros.Length);
            if (chosenIndices.Add(randomIndex))
            {

                Transform heroToSpawn = Heros[randomIndex];
                Transform Hero = Instantiate(heroToSpawn, currentPos, Quaternion.identity);
                Hero.transform.parent = spawnedPoint;
                currentPos += offset;
            }


        }
        OnchangeHeroPos?.Invoke(this, EventArgs.Empty);

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindAnyObjectByType<Player>();
        }
    }

    public void PlayHornSound()
    {
        SoundManager.instance.PlaySound(hornSound);
    }
    public void Show()
    {

        canvasPopup.gameObject.SetActive(true);
        cost.text = " Press E to Summon the Merchant Hero";

    }
    public void Hide()
    {
        canvasPopup.gameObject.SetActive(false);

    }
}
