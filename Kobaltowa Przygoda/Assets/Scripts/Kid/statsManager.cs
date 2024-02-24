using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statsManager : MonoBehaviour
{
    private List<GameObject> kidObjects;
    public List<Kid> kids = new List<Kid>();
    public KidsMaster _KidsMaster;
    public float avarageSpeed;
    public float avarageLadownosc;
    public float avarageEfficiency;
    void Start()
    {
        // Pobierz listę kidObjects z KidListManager
        kids = _KidsMaster.KidsList;
        Debug.Log("Liczba obiektów KID z innego skryptu: " + kids.Count);
        
    }

    private void Update()
    {
        statsActual();
    }

    void statsActual()
    {
        float helper1 = 0;
        float helper2 = 0;
        float helper3 = 0;
        foreach (Kid kid in kids)
        {
            Kid kidScript = kid.GetComponent<Kid>();
            helper1 += kidScript.speed;
            helper2 += kidScript.efficiency;
            helper3 += kidScript.ladownsc;

        }

        if (kids.Count > 0)
        {
            avarageSpeed = helper1 / kids.Count;
            avarageEfficiency = helper2 / kids.Count;
            avarageLadownosc = helper3 / kids.Count;
        }
    }
}
