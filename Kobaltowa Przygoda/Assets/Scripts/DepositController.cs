using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositController : MonoBehaviour
{
    static public float tickRate = 0.3f;
    private float tickTimer = 0f;


    [SerializeField] int requiredMinePoints = 50;
    [SerializeField] int defaultExcavateRate = 1;
    [SerializeField] int defaultEfficiencyRate = 1;


    [Space(5)]
    [Header("UI")]
    [SerializeField] private DepositUI depositUI;

    private int currentMinePoints = 0;

    private int excavatedCobalt = 0;

    private int minerCount = 1;
    private int efficiencyRate = 1;

    bool isMining = false;

    private void Start()
    {
        UpdateRates(defaultExcavateRate, defaultEfficiencyRate);
        depositUI.SetMaxValue(requiredMinePoints);
        depositUI.UpdateExcavationProgress(0);
        depositUI.UpdateExcavatedCobalt(0);
    }

    private void Update()
    {
        if (isMining && Time.time > tickTimer)
        {
            tickTimer = Time.time + tickRate;   
            Excavate();
        }
    }

    void Excavate()
    {
        if (currentMinePoints < requiredMinePoints)
            currentMinePoints += minerCount;
        else
        {
            currentMinePoints = 0;
            excavatedCobalt += efficiencyRate;
            depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        }
        depositUI.UpdateExcavationProgress(currentMinePoints);
    }


    void UpdateRates(int minerCount, int efficiencyRate)
    {
        this.minerCount = minerCount;
        this.efficiencyRate = efficiencyRate;
    }

    public void BeginExcavation(int minerCount)
    {
        isMining = true;
        UpdateRates(minerCount, defaultEfficiencyRate);
        depositUI.SetMaxValue(requiredMinePoints);
    }

    public (int, int) StopExcavation()
    {
        isMining = false;
        int retCobalt = excavatedCobalt;
        int retMiner = minerCount;
        excavatedCobalt = 0;
        depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        return (retCobalt, retMiner);
    }

    public bool MiningStatus()
    {
        return isMining;
    }

}
