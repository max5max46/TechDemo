using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CashDoor : MonoBehaviour
{
    public int coinAmount;
    TextMeshPro coinAmountText;

    // Start is called before the first frame update
    void Start()
    {
        coinAmountText = transform.GetChild(0).GetComponent<TextMeshPro>();
        coinAmountText.text = "Pay " + coinAmount + " Coins to Open";
    }
}
