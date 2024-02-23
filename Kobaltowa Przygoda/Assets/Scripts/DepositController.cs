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

    private int currentMinePoints = 0;

    private int excavatedCobalt = 0;

    private int minerCount = 1;
    private int efficiencyRate = 1;

    bool isMining = false;

    private void Start()
    {
        UpdateRates(defaultExcavateRate, defaultEfficiencyRate);
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
        }
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
    }

    public (int, int) StopExcavation()
    {
        isMining = false;
        int retCobalt = excavatedCobalt;
        int retMiner = minerCount;
        excavatedCobalt = 0;
        return (retCobalt, retMiner);
    }

    public bool MiningStatus()
    {
        return isMining;
    }

}
