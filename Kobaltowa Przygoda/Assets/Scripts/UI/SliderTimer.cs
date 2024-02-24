using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    private Slider s;
    private float timer;

    private void Start() {
        s = GetComponent<Slider>();
        s.value = 0f;
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        s.value = timer/DayManager.Instance.dayLength;
    }
}
