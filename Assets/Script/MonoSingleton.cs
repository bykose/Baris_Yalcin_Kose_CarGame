using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    [Header("Reference")]
    private static volatile T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(T)) as T;
            return instance;
        }
    }
}
