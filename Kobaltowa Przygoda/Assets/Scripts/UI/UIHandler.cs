using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class UIHandler : MonoBehaviour
{
    [Title("UI Binds")]
    [SerializeField] KeyCode shopToggle = KeyCode.Tab;

    [Space(5)]
    [Title("UI Elements")]
    [SerializeField] [SceneObjectsOnly] Canvas shopCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(shopToggle))
            ToggleShop();

    }

    void ToggleShop()
    {
        if (shopCanvas.gameObject.activeInHierarchy)
        {
            shopCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            shopCanvas.gameObject.SetActive(true);
        }
    }
}
