using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DepositController : SerializedMonoBehaviour
{
    static public float tickRate = 0.3f;
    private float tickTimer = 0f;


    [SerializeField] int requiredMinePoints = 50;
    [SerializeField] (int, int) storedCobaltRange = (3, 9);
    [SerializeField] int excavateSpeedModifier = 1;
    [SerializeField] int defaultEfficiencyRate = 1;


    [Space(5)]
    [Title("UI")]
    [SerializeField] private DepositUI depositUI;

    private int currentMinePoints = 0;

    [Space(5)]
    [Title("Debug")]
    [ReadOnly] [ShowInInspector] int cobalt = 0;
    private int excavatedCobalt = 0;

    [Space(5)]
    [Title("Debug")]
    [SerializeField] [ReadOnly] private int minerCount = 0;
    private int miningSpeed = 0;
    private int efficiencyRate = 0;

    bool isMining = false;


    private void Awake()
    {
        UpdateRates(0, defaultEfficiencyRate);
        depositUI.SetMaxValue(requiredMinePoints);
        depositUI.UpdateExcavationProgress(0);
        depositUI.UpdateExcavatedCobalt(0);

        cobalt = Random.Range(storedCobaltRange.Item1, storedCobaltRange.Item2);
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
        if (minerCount == 0)
            isMining = false;

        if (currentMinePoints < requiredMinePoints)
            currentMinePoints += miningSpeed;
        else
        {
            currentMinePoints = 0;
            excavatedCobalt += efficiencyRate;
            cobalt--;
            depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        }

        if(cobalt == 0)
        {
            DestroyDeposit();
        }
        depositUI.UpdateExcavationProgress(currentMinePoints);
    }


    void UpdateRates(int minerCount, int efficiencyRate)
    {
        miningSpeed = minerCount * excavateSpeedModifier;
        this.efficiencyRate = efficiencyRate;
    }

    public void BeginExcavation(int minerCount)
    {
        isMining = true;
        this.minerCount = minerCount;
        UpdateRates(this.minerCount, defaultEfficiencyRate);
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

    public void DestroyDeposit()
    {
        isMining = true;
        UpdateRates(0, 0);
        // Yeet children, destroy deposit object (set active false), give children cobalt
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
