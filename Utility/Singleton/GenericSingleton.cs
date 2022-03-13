using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAA.Utility.Singleton
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                        (instance as GenericSingleton<T>).InitializeSingleton();
                        Debug.LogWarning($"A Singleton of type {instance.GetType().ToString()} was not found. It has been automatically created." +
                                            "You might want to consider having one in the Scene by default.");
                    }
                    GenericSingletonManager.OnDestroyAllSingletons += (instance as GenericSingleton<T>).DestroyInstance;
                }
                return instance; 
            }
        }

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                GenericSingletonManager.OnDestroyAllSingletons += DestroyInstance;
                InitializeSingleton();
            }
            else
            {
                Debug.LogWarning($"There already exists a {this.GetType().ToString()} in this scene. This instance {gameObject.name} will be deleted.");
                Destroy(gameObject);
            }
        }

        protected virtual void InitializeSingleton()
        {

        }

        protected virtual void DestroyInstance()
        {
            instance = null;
            Destroy(instance.gameObject);
        }
    }

    public static class GenericSingletonManager
    {
        public static Action OnDestroyAllSingletons;

        public static void DestroyAllSingletons()
        {
            OnDestroyAllSingletons?.Invoke();
            OnDestroyAllSingletons = null;
        }
    }
}