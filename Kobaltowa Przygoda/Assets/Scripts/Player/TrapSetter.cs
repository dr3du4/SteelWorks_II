using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSetter : MonoBehaviour
{
    public,, int selectedTrapLevel = 1;
    [SerializeField] private GameObject trap;
    [SerializeField] private List<int> trapReg = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedTrapLevel = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) selectedTrapLevel = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) selectedTrapLevel = 3;
        if (Input.GetKeyDown(KeyCode.R)) SetTrap(selectedTrapLevel);
    }

    public void AddTrap(int level) {
        trapReg[level - 1]++;
    }
    
    private void SetTrap(int level) {
        if (trapReg[level - 1] == 0) return;
        trapReg[level - 1]--;
        Trap t = Instantiate(trap, transform.position, transform.rotation).GetComponent<Trap>();
        t.trapLevel = level;
    }
}
