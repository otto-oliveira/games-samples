/*
 * Copyright 2024 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DisplayRateController : MonoBehaviour
{
    [FormerlySerializedAs("ApiText")] public Text apiText;

    [FormerlySerializedAs("GameModeText")] public Text gameModeText;

    [FormerlySerializedAs("AdaptivePerformanceText")] public Text adaptivePerformanceText;

    [FormerlySerializedAs("TargetFrameRateText")] [SerializeField] private  Text targetFrameRateText;
    [FormerlySerializedAs("DisplayRateText")] [SerializeField] private Text displayRateText;


    [SerializeField] private Slider framerateSlider;
    [SerializeField] private Slider displayRateSlider;

    void Start()
    {
        DisplayRateUtil.Init();
        //SetFrameRateFromGameMode();
        framerateSlider.onValueChanged.AddListener(OnFramerateChanged);
        displayRateSlider.onValueChanged.AddListener(OnDisplayRateChanged);
        StartCoroutine(UpdateStats());
    }

    private void OnDisplayRateChanged(float value)
    {
        var rate = value * 30;
        var final = (int)rate;
        DisplayRateUtil.SetDisplayRefreshRate(final);
        StartCoroutine(UpdateStats());
    }

    private void OnFramerateChanged(float value)
    {
        var rate = value * 30;
        var final = (int)rate;
        Application.targetFrameRate = final;
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats()
    {
        yield return new WaitForSeconds(0.3f);
        targetFrameRateText.text = $"Target Framerate {Application.targetFrameRate}";
        displayRateText.text = $"Display Rate {DisplayRateUtil.GetDeviceRefreshRate()}";
    }

}