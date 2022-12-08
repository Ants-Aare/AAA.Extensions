using System;
using UnityEngine;

//FUTURE MAINTAINER: TAKE CARE WHEN UPDATING THIS. MAKE IT RETROCOMPATIBLE OR NOTIFY EVERYONE.

namespace AAA.Utility.Extensions
{
    public static class PlayerPrefsExtensions
    {
        public static void SetBool(string key, bool value)
        {
            var intState = Convert.ToInt32(value);
            PlayerPrefs.SetInt(key, intState);
        }

        public static bool GetBool(string key)
        {
            var intState = PlayerPrefs.GetInt(key);
            return Convert.ToBoolean(intState);
        }

        public static bool GetBool(string key, bool defaultValue)
        {
            var intState = PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
            return Convert.ToBoolean(intState);
        }

        public static void SetEnum<T>(
            string key,
            T value) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = Convert.ToInt32(value);
            PlayerPrefs.SetInt(key, intState);
        }

        public static T GetEnum<T>(
            string key) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = PlayerPrefs.GetInt(key);
            return (T)Enum.ToObject(typeof(T), intState);
        }

        public static T GetEnum<T>(
            string key,
            T defaultValue) where T : Enum
        {
            //TODO: If we find a non alloc version, it'd be better
            var intState = PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
            return (T)Enum.ToObject(typeof(T), intState);
        }

        public static void SetLong(
            string key,
            long value)
        {
            var stringValue = value.ToString();
            PlayerPrefs.SetString(key, stringValue);
        }

        public static long GetLong(
            string key)
        {
            var stringValue = PlayerPrefs.GetString(key);
            return long.Parse(stringValue);
        }

        public static long GetLong(
            string key,
            long defaultValue)
        {
            var stringValue = PlayerPrefs.GetString(key, defaultValue.ToString());
            return long.Parse(stringValue);
        }
    }
}
