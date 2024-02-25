using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCobalt : MonoBehaviour
{
    Kid k;
    //sdads
    [SerializeField] GameObject imageObject;

    private void Start()
    {
        k = GetComponent<Kid>();
    }

    private void Update()
    {

        if (k.holdCobalt > 0 && !imageObject.activeSelf)
            imageObject.SetActive(true);
        else if (k.holdCobalt == 0 && imageObject.activeSelf)
            imageObject.SetActive(false);
    }
}
