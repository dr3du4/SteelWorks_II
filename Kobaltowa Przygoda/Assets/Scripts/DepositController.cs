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

    
    private int excavatedCobalt = 0;

    [Space(5)]
    [Title("Debug")]
    [SerializeField] [ReadOnly] private int minerCount = 0;
    [SerializeField] [ReadOnly] int cobalt = 0;
    [SerializeField] [ReadOnly] List<KidData> miners = new();
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

        minerCount = miners.Count;
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


    void UpdateRates(int excavateSpeed, int efficiencyRate)
    {
        miningSpeed = this.minerCount * excavateSpeed;
        this.efficiencyRate = efficiencyRate;
    }

    public void BeginExcavation(int minerCount, List<Kid> newWorker) // Add List<Kid> as argument (so we can store kids)
    {
        miners = new();

        foreach(Kid k in newWorker)
        {
            miners.Add(k.GetKidData());
        }
        isMining = true;
        this.minerCount = miners.Count;
        UpdateRates(excavateSpeedModifier, defaultEfficiencyRate);
        depositUI.SetMaxValue(requiredMinePoints);
    }

    public (int, List<KidData>) StopExcavation()
    {
        isMining = false;
        int retCobalt = excavatedCobalt;
        List<KidData> retMiner = new(miners);
        minerCount = 0;
        miners = new();
        excavatedCobalt = 0;
        depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        return (retCobalt, retMiner);
    }

    public KidData KidnapWorker(KidData k)
    {
        minerCount--;
        UpdateRates(1, defaultEfficiencyRate);
        KidData ret = miners.Find(a=> a.playerId == k.playerId);
        miners.Remove(k);
        return ret;
    }

    public KidData KidnapWorker()
    {
        minerCount--;
        UpdateRates(excavateSpeedModifier, defaultEfficiencyRate);
        KidData k = miners[miners.Count - 1];
        miners.RemoveAt(miners.Count - 1);
        return k;
    }

    public void DestroyDeposit()
    {
        isMining = true;
        UpdateRates(0, 0);
        // Yeet children, destroy deposit object (set active false), give children cobalt
    }

    public bool WorkerInDeposit(KidData k)
    {
        KidData kidData = miners.Find(a => a.playerId == k.playerId);
        if (kidData!=null && kidData.playerId!=-1)
            return true;
        return false;
    }

    public bool MiningStatus()
    {
        return isMining;
    }


    public int GetWorkerCount()
    {
        return minerCount;
    }

    public KidData GetRandomWorker()
    {
        if (GetWorkerCount() > 0)
            return miners[Random.Range(0, miners.Count)];
        return null;
    }
}
