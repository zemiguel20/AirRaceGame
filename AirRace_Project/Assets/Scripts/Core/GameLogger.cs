/*
 * As recommended in the documentation, development Debug logs should be wrapped in a conditional method
 * https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity7.html
 */

using System.Diagnostics;

namespace Assets.Scripts.GameLogger
{
    public static class GameLogger
    {

        [Conditional("UNITY_EDITOR")]
        public static void Debug(string logMsg)
        {

            UnityEngine.Debug.Log(logMsg);

        }

    }
}