using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepositUI : MonoBehaviour
{
    [SerializeField] Slider progressSlider;
    [SerializeField] TextMeshProUGUI cobaltCountText;

    public void SetMaxValue(int maxValue)
    {
        progressSlider.maxValue = maxValue;
    }

    public void UpdateExcavationProgress(int currentValue)
    {
        progressSlider.gameObject.SetActive(currentValue != 0);
        progressSlider.value = currentValue;
    }

    public void UpdateExcavatedCobalt(int amount)
    {
        cobaltCountText.gameObject.SetActive(amount != 0);
        cobaltCountText.text = amount.ToString();
    }
}
