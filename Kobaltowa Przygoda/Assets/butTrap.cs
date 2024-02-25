using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class butTrap : MonoBehaviour
{
    public int level;
    public TrapSetter _trapSetter;

    void buyTrap()
    {
        _trapSetter.AddTrap(level);
    }

}
