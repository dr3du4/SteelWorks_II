using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start() {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
    
    public void SetVolume() {
        PlayerPrefs.SetFloat("Volume",slider.value);
    }
}
