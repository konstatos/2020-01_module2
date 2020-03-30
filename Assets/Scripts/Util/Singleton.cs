
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : Singleton<T>
{
    static T instance;
    public static T Instance { get {
            if (instance == null) {
                var go = new GameObject(typeof(T).Name);
                instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);
            }
            return instance;
        } }
}
