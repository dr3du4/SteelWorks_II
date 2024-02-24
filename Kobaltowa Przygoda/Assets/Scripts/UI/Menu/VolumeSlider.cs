using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start() {
        if (PlayerPrefs.HasKey("Volume")) slider.value = PlayerPrefs.GetFloat("Volume");
        else PlayerPrefs.SetFloat("Volume",1f);
    }
    
    public void SetVolume() {
        PlayerPrefs.SetFloat("Volume",slider.value);
    }
}
