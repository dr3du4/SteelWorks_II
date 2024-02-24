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
    private int excavationLimit = 0;

    [Space(5)]
    [Title("Debug")]
    [SerializeField] [ReadOnly] int cobalt = 0;
    [SerializeField] [ReadOnly] List<Kid> miners = new();
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
        if (miners.Count == 0)
            isMining = false;

        if (currentMinePoints < requiredMinePoints)
            currentMinePoints += miningSpeed;
        else
        {
            currentMinePoints = 0;
            excavatedCobalt += efficiencyRate;

            if (excavatedCobalt >= excavationLimit)
            {
                // Can't collect any more cobalt
                isMining = true;
                UpdateRates(0, 0);
                DispenseCobalt();
            }

            cobalt--;
            //miners[Random.Range(0, miners.Count - 1)].holdCobalt++;
            depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        }

        if(cobalt == 0)
        {
            DispenseCobalt();
            DestroyDeposit();
        }
        depositUI.UpdateExcavationProgress(currentMinePoints);
    }

    void DispenseCobalt()
    {
        foreach (Kid k in miners)
        {
            int diff = k.maxCobalt - k.holdCobalt;
            if (diff <= excavatedCobalt)
            {
                k.holdCobalt = k.maxCobalt;
                excavatedCobalt -= diff;
            }
            else
            {
                k.holdCobalt += excavatedCobalt;
                excavatedCobalt = 0;
            }
        }
    }


    void UpdateRates(int excavateSpeed, int efficiencyRate)
    {
        miningSpeed = miners.Count * excavateSpeed;
        this.efficiencyRate = efficiencyRate;
    }

    public void BeginExcavation(List<Kid> newWorker) 
    {
        miners.Clear();
        excavationLimit = 0;
        
        foreach(Kid k in newWorker)
        {
            miners.Add(k);
            excavationLimit += k.maxCobalt - k.holdCobalt;
            k.BeginMining(transform.position);
        }
        isMining = true;
        UpdateRates(excavateSpeedModifier, defaultEfficiencyRate);
        depositUI.SetMaxValue(requiredMinePoints);
    }

    public (int, List<Kid>) StopExcavation()
    {
        isMining = false;
        int retCobalt = excavatedCobalt;
        DispenseCobalt();
        List<Kid> retMiner = new();
        foreach (Kid k in miners)
        {
            k.StopMining();
            retMiner.Add(k);
        }
        miners.Clear();
        excavatedCobalt = 0;
        depositUI.UpdateExcavatedCobalt(excavatedCobalt);
        return (retCobalt, retMiner);
    }

    public Kid KidnapWorker(Kid k)
    {
        UpdateRates(1, defaultEfficiencyRate);
        Kid ret = miners.Find(a=> a == k);
        miners.Remove(k);
        ret.StopMining();
        return ret;
    }

    public Kid KidnapWorker()
    {
        UpdateRates(excavateSpeedModifier, defaultEfficiencyRate);
        Kid k = miners[miners.Count - 1];
        miners.RemoveAt(miners.Count - 1);
        k.StopMining();
        return k;
    }

    public void DestroyDeposit()
    {
        isMining = true;
        UpdateRates(0, 0);
        // Yeet children, destroy deposit object (set active false), give children cobalt
    }

    public bool WorkerInDeposit(Kid k)
    {
        if (miners.Find(a => a.playerId == k.playerId))
            return true;
        return false;
    }

    public bool MiningStatus()
    {
        return isMining;
    }


    public int GetWorkerCount()
    {
        return miners.Count;
    }

    public Kid GetRandomWorker()
    {
        if (GetWorkerCount() > 0)
            return miners[Random.Range(0, miners.Count)];
        return null;
    }
}
