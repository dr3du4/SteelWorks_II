using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KidsMaster))]
public class PlayerMine : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] MinerAssignPanel minerAssignPanel;

    [Space(5)]
    [Header("Binds")]
    public KeyCode depositInteractBind = KeyCode.E;

    [Space(5)]
    [Header("Helper")]


    DepositController currentDeposit = null;
    int minerDesignation = 0;

    public int minerCount = 0;

    KidsMaster kidsMaster;

    public int totalCobalt = 0;
    public int maxHeldCobalt = 0;


    private void Start()
    {
        minerAssignPanel.SetPlayerMine(this);
        kidsMaster = GetComponent<KidsMaster>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(depositInteractBind) && currentDeposit != null)
            InteractWithDeposit(currentDeposit);

        
    }

    void InteractWithDeposit(DepositController deposit)
    {
        if (deposit.MiningStatus())
        {
            (int gatheredCobalt, List<Kid> returnedMiners) = deposit.StopExcavation();
            totalCobalt += gatheredCobalt;
            //minerCount += returnedMiners;
            Debug.Log(returnedMiners.Count);
            kidsMaster.ReturnKids(returnedMiners);
            minerAssignPanel.SetVisibility(true);
        }
        else
        {
            if (!deposit.depositDepleted)
            {
                minerDesignation = minerAssignPanel.GetMinerCountSelection();


                if (minerCount >= minerDesignation && minerDesignation > 0)
                {
                    deposit.BeginExcavation(kidsMaster.RemoveKids(minerDesignation));
                    //minerCount -= minerDesignation;
                    minerAssignPanel.SetVisibility(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CobaltDeposit"))
        {
            currentDeposit = collision.gameObject.GetComponent<DepositController>();
            if (currentDeposit && !currentDeposit.MiningStatus() && maxHeldCobalt != totalCobalt && !currentDeposit.depositDepleted)
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
