using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HungrySliderControler : MonoBehaviour
{
    public Slider foodSlider;
    public float food;
    public hungryManager _HungryManager;
    
    public Image fillAreaImage;
    public Image handleSlideAreaImage;

    private Color originalColorfill;
    private Color originalColorhandle;
    void Start()
    {
        originalColorfill = fillAreaImage.color;
        originalColorhandle = handleSlideAreaImage.color;
        UpdateSliderValue();
        StartCoroutine(PulseSlider());
    }

    void Update()
    {

        food = _HungryManager.avarageHunger;
        UpdateSliderValue();
    }

    void UpdateSliderValue()
    {
        foodSlider.value = food;
    }

    IEnumerator PulseSlider()
    {
        while (true)
        {
            if (food < 0)
            {
                // Pulsuj sliderem, jeśli wartość jedzenia jest mniejsza niż 0
                foodSlider.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                
                handleSlideAreaImage.color=Color.red;
                fillAreaImage.color = Color.red;
                
                
                yield return new WaitForSeconds(0.5f);
                
                foodSlider.transform.localScale = Vector3.one;


                handleSlideAreaImage.color = originalColorhandle;
                fillAreaImage.color = originalColorfill;
                
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                // Jeśli wartość jedzenia jest większa lub równa 0, zaczekaj
                yield return null;
            }
        }
    }
}