using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DepositAnim : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    DepositController controller;
    
    [Title("Sprites")]
    [SerializeField] Sprite fullDeposit;
    [SerializeField] Sprite excavatingDeposit;
    [SerializeField] Sprite excavatingDeposit2;
    [SerializeField] Sprite excavatedDeposit;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<DepositController>();
        spriteRenderer.sprite = fullDeposit;
    }

    private void Update()
    {
        if(controller.GetExcavatedPercentage() <= 0.75f && controller.GetExcavatedPercentage() > 0 && spriteRenderer.sprite != excavatingDeposit)
        {
            spriteRenderer.sprite = excavatingDeposit;
        }

        if (controller.GetExcavatedPercentage() <= 0.35f && controller.GetExcavatedPercentage() > 0 && spriteRenderer.sprite != excavatingDeposit2)
        {
            spriteRenderer.sprite = excavatingDeposit2;
        }

        if (controller.GetExcavatedPercentage() == 0 && spriteRenderer.sprite != excavatedDeposit)
        {
            spriteRenderer.sprite = excavatedDeposit;
        }
    }
}
