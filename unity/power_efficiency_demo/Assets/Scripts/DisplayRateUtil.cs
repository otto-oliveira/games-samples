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

using UnityEngine;

public static class DisplayRateUtil
{
    private static AndroidJavaClass unityPlayerClass;
    private static AndroidJavaObject activity;
    private static AndroidJavaObject displayUtil;

    public static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log($"Init");
        //Find the UnityPlayer class that must be present
        unityPlayerClass ??= new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //Get the Current Activity statically 
        activity ??= unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        //Create a new Display Util object
        displayUtil ??= new AndroidJavaObject("com.google.android.games.DisplayUtil");
        displayUtil.Call("init", activity);
#endif
    }
    
    public static void SetDisplayRefreshRate(int refreshRate)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        displayUtil.Call("setDisplayRefreshRate", refreshRate);
#endif
    }

    public static float GetDeviceRefreshRate()
    {
        float refreshRate = 60; // Default to 60Hz if we can't detect

#if UNITY_ANDROID && !UNITY_EDITOR
        refreshRate = displayUtil.Call<float>("getDisplayRefreshRate");
#endif

        return refreshRate;
    }
}