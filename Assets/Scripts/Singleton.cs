using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = FindObjectOfType<T>();

            if (_instance == null)
            {
                var singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
                singleton.name = _instance.GetType().Name;
                DontDestroyOnLoad(singleton);
            }
        }
    }
}
