using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButtonScirpt : MonoBehaviour
{
  
    public List<GameObject> sklepy =new List<GameObject>();
    public GameObject odpowiedniSklep;

    void Start()
    {
      
      
        foreach (GameObject sklep in sklepy)
        {
            sklep.SetActive(false);
        }

    }

    void Update()
    {
        
    }

    public void OnOff()
    {
        foreach  (GameObject sklep in sklepy)
        {
            sklep.SetActive(false);
        }
        odpowiedniSklep.SetActive(true);
    }

    private void Awake()
    {
          GameObject[] sklepyTag = GameObject.FindGameObjectsWithTag("Sklep");
        
                // Dodaj znalezione obiekty do listy
                foreach (GameObject sklepObject in sklepyTag)
                {
                    sklepy.Add(sklepObject);
                }
        
    }
}
