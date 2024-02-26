using UnityEngine;

/// <summary>
/// Detele all copies of the object use derived class
/// </summary>
/// <typeparam name="T">T is the class that derived from this class, T is a Component</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Component 
{
    #region Variable private
    private static T instance;

    #endregion

    #region Property
    public static T Instance { 
        get {
            if(instance == null){
                instance = (T)FindFirstObjectByType(typeof(T));
                if(instance == null){
                    Setup();
                }
            }
            return instance;
        }
    }

    #endregion

    private void Awake() {
        RemoveDuplicates();
    }

    private void RemoveDuplicates()
    {
        if(instance == null){
            instance = this as T;
        }
        else{
            Destroy(gameObject);
        }
    }

    private static void Setup()
    {
        GameObject gameObj = new();
        gameObj.name = typeof(T).Name;
        instance = gameObj.AddComponent<T>();
    }
}
