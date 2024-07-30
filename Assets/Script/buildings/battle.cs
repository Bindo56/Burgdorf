using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class battle : MonoBehaviour
{
    public static battle instance;
    [SerializeField] Transform canvasPopup;
   public TextMeshProUGUI cost;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPos()
    {
       return gameObject.transform;
    }
    public void Show()
    {

        canvasPopup.gameObject.SetActive(true);
        cost.text = " Press E to Grather Merchants and Go to Battle";

    }
    public void Hide()
    {
        canvasPopup.gameObject.SetActive(false);

    }
}
