using UnityEngine;
using System.Collections;

public class StepCounterPlugin {

	public StepCounterPlugin()
    {

    }

    public static int GetStepCount()
    {
        using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var androidPlugin = new AndroidJavaObject("com.nado.stepcounter.StepCounterService"))
                {
                    int count = androidPlugin.Call<int>("GetStepCount");
                    return count;
                }
            }
        }
    }

    public static void StartStepCounterService()
    {
        using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var androidPlugin = new AndroidJavaObject("com.nado.stepcounter.ServiceStarter", currentActivity))
                {
                    Debug.Log("Call StartStepCounterService()");
                    androidPlugin.Call("StartStepCounterService");
                }
            }
        }
    }

    public static void StopStepCounterService()
    {
        using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var androidPlugin = new AndroidJavaObject("com.nado.stepcounter.ServiceStarter", currentActivity))
                {
                    Debug.Log("Call StopStepCounterService()");
                    androidPlugin.Call("StopStepCounterService");
                }
            }
        }
    }
}
