using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinerAssignPanel : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] GameObject invalidMinerCountPanel;
    [SerializeField] int maxMinerCount = 5;
    private int minMinerCount = 0;


    private int currentMinerCount = 0;
    Transform depositPivot = null;
    private PlayerMine playerMine;

    [Space(5)]
    [Header("Binds")]
    [SerializeField] KeyCode addCount = KeyCode.UpArrow;
    [SerializeField] KeyCode lowerCount = KeyCode.DownArrow;

    private void Start()
    {
        SetVisibility(false);
    }

    public void SetVisibility(bool visibility)
    {
        gameObject.SetActive(visibility);
        if(playerMine)
            invalidMinerCountPanel.SetActive(playerMine.minerCount < currentMinerCount);
    }

    public void SetPivot(Transform pivot)
    {
        depositPivot = pivot;
    }

    public void SetPlayerMine(PlayerMine player)
    {
        playerMine = player;
    }

    private void Update()
    {
        if (gameObject.activeSelf && depositPivot)
        {
            if (Input.GetKeyDown(addCount) && currentMinerCount < maxMinerCount)
            {
                currentMinerCount++;
                UpdateUI();
            }
            else if (Input.GetKeyDown(lowerCount) && currentMinerCount > minMinerCount)
            {
                currentMinerCount--;
                UpdateUI();
            }

            GetComponent<RectTransform>().position = cam.WorldToScreenPoint(depositPivot.position);
            invalidMinerCountPanel.GetComponent<RectTransform>().position = cam.WorldToScreenPoint(depositPivot.position);
        }
    }

    public void UpdateUI()
    {
        numberText.text = currentMinerCount.ToString();
        if(playerMine)
            invalidMinerCountPanel.SetActive(playerMine.minerCount < currentMinerCount);
    }

    public int GetMinerCountSelection()
    {
        return currentMinerCount;
    }

}
