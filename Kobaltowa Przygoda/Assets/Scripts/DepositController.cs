using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DepositController : SerializedMonoBehaviour
{
    static public float tickRate = 0.3f;
    private float tickTimer = 0f;


    [SerializeField] int requiredMinePoints = 50;
    [SerializeField] int defaultExcavateRate = 1;
    [SerializeField] int defaultEfficiencyRate = 1;


    [Space(5)]
    [Title("UI")]
    [SerializeField] private DepositUI depositUI;

    private int currentMinePoints = 0;

    private int excavatedCobalt = 0;

    [Space(5)]
    [Title("Debug")]
    [ShowInInspector] [ReadOnly] private int minerCount = 0;
    private int efficiencyRate = 0;

    bool isMining = false;

    private void Start()
    {
        UpdateRates(0, defaultEfficiencyRate);
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
            currentMinePoints += minerCount * defaultExcavateRate;
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

    public int KidnapWorker()
    {
        minerCount--;
        UpdateRates(minerCount, defaultEfficiencyRate);
        return 1;
    }

    public bool MiningStatus()
    {
        return isMining;
    }


    public int GetWorkerCount()
    {
        return minerCount;
    }
}
