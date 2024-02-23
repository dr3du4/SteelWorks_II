using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMine : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] MinerAssignPanel minerAssignPanel;

    [Space(5)]
    [Header("Binds")]
    public static KeyCode depositInteractBind = KeyCode.E;


    DepositController currentDeposit = null;
    int minerDesignation = 0;

    public int minerCount = 0;

    // Probably temporary
    public int totalCobalt = 0;


    private void Update()
    {
        if (Input.GetKeyDown(depositInteractBind) && currentDeposit != null)
            InteractWithDeposit(currentDeposit);
    }

    void InteractWithDeposit(DepositController deposit)
    {
        if (deposit.MiningStatus())
        {
            (int gatheredCobalt, int returnedMiners) = deposit.StopExcavation();
            totalCobalt += gatheredCobalt;
            minerCount += returnedMiners;
            minerAssignPanel.SetVisibility(true);
        }
        else
        {
            minerDesignation = minerAssignPanel.GetMinerCountSelection();
            if(minerCount >= minerDesignation && minerDesignation > 0)
            {
                deposit.BeginExcavation(minerDesignation);
                minerCount -= minerDesignation;
                minerAssignPanel.SetVisibility(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CobaltDeposit"))
        {
            currentDeposit = collision.gameObject.GetComponent<DepositController>();
            if (currentDeposit && !currentDeposit.MiningStatus())
            {
                // Show UI for miner count selection
                minerAssignPanel.SetVisibility(true);
                minerAssignPanel.SetPivot(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CobaltDeposit"))
        {
            currentDeposit = null;
            minerAssignPanel.SetVisibility(false);
        }
    }


}
