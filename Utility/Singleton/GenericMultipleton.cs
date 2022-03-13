using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAA.Utility.Singleton
{
    public class GenericMultipleton<T> : MonoBehaviour where T : Component
    {
        protected static Dictionary<int, T> instances = new Dictionary<int, T>();
        public static bool InstanceExists(int instanceID)
        {
            return instances.ContainsKey(instanceID);
        }
        public static T GetInstance(int instanceID)
        {
            if (instances.TryGetValue(instanceID, out T instance))
            {
                return instance;
            }
            return null;
        }
        public static T CreateInstance(int instanceID)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).Name;
            T instance = obj.AddComponent<T>();
            (instance as GenericMultipleton<T>).RegisterInstance(instanceID);
            return instance;
        }
        public static T CreateInstance(int instanceID, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            T instance = go.GetComponent<T>();
            (instance as GenericMultipleton<T>).RegisterInstance(instanceID);
            return instance;
        }

        public int ID;
        protected bool registerInstanceOnAwake = false;
        public virtual void Awake()
        {
            GenericSingletonManager.OnDestroyAllSingletons += DestroyInstance;
            if (!registerInstanceOnAwake)
                return;

            if (!InstanceExists(ID))
            {
                RegisterInstance();
                InitializeInstance();
            }
            else
            {
                Debug.LogWarning($"There already exists a {this.GetType().ToString()} using the index {ID} in this scene. This instance {gameObject.name} will be deleted.");
                Destroy(gameObject);
            }
        }
        protected virtual void RegisterInstance()
        {
            instances.Add(ID, this as T);
        }
        protected virtual void RegisterInstance(int index)
        {
            this.ID = index;
            instances.Add(index, this as T);
        }

        protected virtual void InitializeInstance()
        {

        }

        protected virtual void DestroyInstance()
        {
            instances.Remove(ID);
            Destroy(gameObject);
        }
    }
}