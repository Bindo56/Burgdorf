using System;

using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler OnHerosSpawaned;
    public event EventHandler OnWeaponStoreUpgrade;

    [SerializeField] WeapeonShop weapeonShops;
    public event EventHandler OnArmourStoreUpgrade;

    [SerializeField] armourShop armourShops;
 //   public event EventHandler Onbattle;
    public event EventHandler OntableUpgrade;

    [SerializeField] Tables tabless;
    [SerializeField] Horn horns;
    [SerializeField] battle battles;

    [SerializeField] heroManager heroManager;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask InteractObjects;
    [SerializeField] LayerMask InteractPopup;
    Vector2 lastInteractDir;
    bool isWalking;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource footstep;

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
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();

        }
    }

    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        HandleInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //  HandleInteraction();
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        if (moveDir != Vector2.zero)
        {
            lastInteractDir = moveDir;
        }
        int interactDistance = 1;
        var hit = Physics2D.Raycast(transform.position, lastInteractDir, interactDistance, InteractPopup);

        if (hit.collider != null && hit.transform.TryGetComponent(out WeapeonShop weapeonShop))
        {
            if (weapeonShops != weapeonShop)
            {
                weapeonShops = weapeonShop;
                WeapeonShop.Instance.Show();
            }
        }
        else
        {
            weaponPopup();
            weapeonShops = null;
        }
        if (hit.collider != null && hit.transform.TryGetComponent(out armourShop armour))
        {
            if (armourShops != armour)
            {
                armourShops = armour;
                armourShop.Instance.Show();
            }
        }
        else
        {
            armourShopPopup();
            armourShops = null;
        }
        if (hit.collider != null && hit.transform.TryGetComponent(out Tables tables))
        {
            if (tabless != tables)
            {
                tabless = tables;
                Tables.Instance.Show();
            }
        }
        else
        {
            Tables.Instance.Hide();
            tabless = null;
        }
        if (hit.collider != null && hit.transform.TryGetComponent(out Horn horn))
        {
            if (horns != horn)
            {
                horns = horn;
                Horn.instance.Show();
            }
        }
        else
        {
            Horn.instance.Hide();
            horns = null;
        }
        if (hit.collider != null && hit.transform.TryGetComponent(out battle battl))
        {
            if (battles != battl)
            {
                battles = battl;
                battle.instance.Show();
            }
        }
        else
        {
            battle.instance.Hide();
            battles = null;
        }

    }
    private void armourShopPopup()
    {
        armourShop.Instance.Hide();
    }

    private void weaponPopup()
    {
        WeapeonShop.Instance.Hide();
    }






    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();


        Vector2 moveDir = new Vector2(inputVector.x, inputVector.y);

        if (moveDir != Vector2.zero)
        {
            lastInteractDir = moveDir;
        }
        int interactDistance = 1;

        var hit = Physics2D.Raycast(transform.position, lastInteractDir, interactDistance, InteractObjects);


        Debug.Log(hit.transform.name);

        if (hit.transform.TryGetComponent(out Horn horn))
        {
            Horn.instance.PlayHornSound();
            Debug.Log(hit.transform.name);
            OnHerosSpawaned?.Invoke(this, EventArgs.Empty);

        }

        if (hit.transform.TryGetComponent(out Heros hero))
        {
            hero.Show();

        }


        if (hit.transform.TryGetComponent(out battle battle))
        {
            BattleEntry();
        }

        if (hit.transform.TryGetComponent(out WeapeonShop weapeonShop))
        {
            Debug.Log("weaponStore");
            OnWeaponStoreUpgrade?.Invoke(this, EventArgs.Empty);

        }
        if (hit.transform.TryGetComponent(out armourShop armourShop))
        {
            Debug.Log("armourStore");
            OnArmourStoreUpgrade?.Invoke(this, EventArgs.Empty);
        }
        if (hit.transform.TryGetComponent(out Tables tables))
        {
            OntableUpgrade?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            return;
        }

    }

    private void BattleEntry()
    {
        heroManager.Instance.LoadDetectedHeroes();


        if (heroManager.Instance.saveHeros.Count == 0 )
        {          
            battle.instance.cost.text = " Summon Heros First";
            Debug.Log("No Heros in hero manager");
        }
        else
        {
            heroManager.Instance.SaveDetectedHeroes();
            SceneManager.LoadScene(1);

        }





    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        if (inputVector == Vector2.zero)
        {
            anim.SetBool("walk", false);
            footstep.Stop();

        }
        else
        {
            anim.SetBool("walk", true);
            footstep.Play();

        }
        Vector2 moveDir = new Vector3(inputVector.x, inputVector.y);
        float playerSize = 0.4f;
        float moveDistances = moveSpeed * Time.deltaTime;
        float playerHeight = 0.8f;

        Vector2 capsuleSize = new Vector2(playerSize, playerHeight);

        bool canMove = !Physics2D.CapsuleCast(transform.position, capsuleSize, CapsuleDirection2D.Vertical, 0, moveDir, moveDistances);



/*
        if (!canMove)
        {
            Vector2 moveDirX = new Vector2(moveDir.x, 0).normalized;

            canMove = !Physics2D.CapsuleCast(transform.position, capsuleSize, CapsuleDirection2D.Vertical, 0, moveDirX, moveDistances);

            if (canMove)
            {
                moveDir = moveDirX;


            }
            else
            {
                Vector2 moveDirY = new Vector2(0, moveDir.y).normalized;
                canMove = !Physics2D.CapsuleCast(transform.position, capsuleSize, CapsuleDirection2D.Vertical, 0, moveDirY, moveDistances);
                if (canMove)
                {
                    moveDir = moveDirY;
                }
                else
                {
                    //cannot move anywhere
                }
            }
        }*/

        if (canMove)
        {
            transform.position += (Vector3)(moveDir * moveDistances);
            FlipSprite(moveDir);
        }

        isWalking = moveDir != Vector2.zero;
    }

    private void FlipSprite(Vector2 moveDir)
    {
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }

    public bool Iswalking()
    {
        return isWalking;
    }



}
