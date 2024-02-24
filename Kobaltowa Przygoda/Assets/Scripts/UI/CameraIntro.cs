using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIntro : MonoBehaviour {
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private Vector3 target;
    [SerializeField] private float introTime = 15f;
    [SerializeField] private float zoomOutTime = 12f;
    private Camera _camera;
    private float introTimer = 0f;
    private Vector3 start;
    private void Start() {
        _camera = GetComponent<Camera>();
        start = transform.position;
    }
    private void Update() {
        introTimer += Time.deltaTime;
        while (introTimer < zoomOutTime) {
            transform.position = Vector3.Lerp(start, target, introTimer / zoomOutTime);
            _camera.orthographicSize = Mathf.Lerp(5f, 15f, introTimer / zoomOutTime);
        }
        if (introTimer >= introTime) sceneChanger.GoToNextScene(); 
    }
}
