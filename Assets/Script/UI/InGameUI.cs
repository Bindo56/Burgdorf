using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = "Coins :- " +GameManager.Instance.coins.ToString("#");
    }
}
