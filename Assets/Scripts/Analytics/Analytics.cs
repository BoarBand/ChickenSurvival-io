using Firebase.Extensions;
using Firebase;
using UnityEngine;

namespace SurvivalChicken.AnalyticsParameters
{
    public class Analytics : MonoBehaviour
    {
        private FirebaseApp _app;

        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                }
                else
                {
                    Debug.LogError(string.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
    }
}
