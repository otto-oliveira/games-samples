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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PerformanceManager : MonoBehaviour
{
    public Text ApiText;

    public Text GameModeText;

    public Text AdaptivePerformanceText;

    [SerializeField] private  Text TargetFrameRateText;
    [SerializeField] private Text DisplayRateText;

    const int BatteryModeFrameRate = 30;
    const int StandardModeFrameRate = 60;
    const int PerformanceModeFrameRate = 120;

    [SerializeField] private Slider framerateSlider;
    [SerializeField] private Slider displayRateSlider;

    void Start()
    {
        SetAdaptivePerformanceText();
        SetGraphicsAPIText();
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
    
    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            // Query the game mode after resuming, as the
            // mode may have been changed by the user while
            // the game was in the background
            //SetFrameRateFromGameMode();
        }
    }

    IEnumerator UpdateStats()
    {
        yield return new WaitForSeconds(0.3f);
        TargetFrameRateText.text = $"Target Framerate {Application.targetFrameRate}";
        DisplayRateText.text = $"Display Rate {DisplayRateUtil.GetDeviceRefreshRate()}";
    }

    void SetGameModeText(string modeText)
    {
        GameModeText.text = $"{modeText}";
        StartCoroutine(UpdateStats());
    }

    void SetFrameRateFromGameMode()
    {
        int gameMode = GameModeUtil.GetGameMode();

        switch (gameMode)
        {
            case GameModeUtil.GameModeUnsupported:
                break;
            case GameModeUtil.GameModeStandard:
                DisplayRateUtil.SetDisplayRefreshRate(StandardModeFrameRate);
                Application.targetFrameRate = StandardModeFrameRate;
                break;
            case GameModeUtil.GameModePerformance:
                DisplayRateUtil.SetDisplayRefreshRate(PerformanceModeFrameRate);
                Application.targetFrameRate = PerformanceModeFrameRate;
                break;
            case GameModeUtil.GameModeBattery:
                DisplayRateUtil.SetDisplayRefreshRate(BatteryModeFrameRate);
                Application.targetFrameRate = BatteryModeFrameRate;
                break;
            case GameModeUtil.GameModeCustom:
                break;
            default:
                break;
        }

        SetGameModeText(GameModeUtil.GetGameModeString(gameMode));
    }

    void SetGraphicsAPIText()
    {
        switch (SystemInfo.graphicsDeviceType)
        {
            case GraphicsDeviceType.Direct3D11:
                ApiText.text = "D3D11";
                break;
            case GraphicsDeviceType.Null:
                ApiText.text = "Null";
                break;
            case GraphicsDeviceType.OpenGLES2:
                ApiText.text = "OpenGL ES 2";
                break;
            case GraphicsDeviceType.OpenGLES3:
                ApiText.text = "OpenGL ES 3";
                break;
            case GraphicsDeviceType.Metal:
                ApiText.text = "Metal";
                break;
            case GraphicsDeviceType.OpenGLCore:
                ApiText.text = "OpenGL Core";
                break;
            case GraphicsDeviceType.Direct3D12:
                ApiText.text = "D3D12";
                break;
            case GraphicsDeviceType.Vulkan:
                ApiText.text = "Vulkan";
                break;
            default:
                ApiText.text = "Unknown API";
                break;
        }
    }

    void SetAdaptivePerformanceText()
    {
        /*IAdaptivePerformance adaptivePerformance = Holder.Instance;
        if (adaptivePerformance == null || !adaptivePerformance.Active)
        {
            AdaptivePerformanceText.text = "Adaptive Performance not active";
        }
        else
        {
            AdaptivePerformanceText.text = "Adaptive Performance active";
        }*/
    }
}