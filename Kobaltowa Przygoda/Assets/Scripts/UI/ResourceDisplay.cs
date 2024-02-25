using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI cobaltText;

    //blep

    backpack backpack;

    private void Start()
    {
        backpack = DayManager.Instance.GetComponent<backpack>();
    }

    private void Update()
    {
        moneyText.text = backpack.money.ToString();
        cobaltText.text = backpack.cobaltTotal.ToString();
    }
}
