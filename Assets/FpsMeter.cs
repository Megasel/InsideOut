using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsMeter : MonoBehaviour
{
    public TMP_Text fpsText; 

    private float _deltaTime = 0.0f;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }
}
