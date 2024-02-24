using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkipIntro : MonoBehaviour {
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private float fadeInTime = 3f;
    private float _fadeInTimer = 0f;
    private TMP_Text _text;
    [SerializeField] private Color32 active;
    [SerializeField] private Color32 disactive;
    private void Start() {
        _text = GetComponent<TMP_Text>();
        _text.color = disactive;
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown("escape") && _text.color.a == 0f) {
            StartCoroutine(FadeIn());
        } else if (Input.GetKeyDown("escape"))sceneChanger.GoToNextScene();
    }

    private IEnumerator FadeIn() {
        while (_fadeInTimer < fadeInTime) {
            _fadeInTimer += Time.deltaTime;
            yield return null;
            _text.color =  Color.Lerp(disactive,active,_fadeInTimer/fadeInTime);
        }
        _text.color = active;
    } 
}
