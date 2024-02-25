using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyPeople : MonoBehaviour
{
    public int race;
    public KidsMaster player;
    
    public void buySlave()
    {
        player.SpawnSpecificWorker(race);
    }
}
