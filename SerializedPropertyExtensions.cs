#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

namespace AAA.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static void ForeachVisibleChildren(
            this SerializedProperty serializedProperty,
            Action<SerializedProperty> action
        )
        {
            if (!serializedProperty.hasVisibleChildren)
            {
                return;
            }

            var endProperty = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            while (!SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                action.Invoke(serializedProperty);
                serializedProperty.NextVisible(false);
            }
        }
        public static float GetVisibleChildrenHeight(this SerializedProperty serializedProperty)
        {
            if (!serializedProperty.hasVisibleChildren)
            {
                return 0;
            }

            var height = 0f;
            var endProperty = serializedProperty.GetEndProperty();

            serializedProperty.NextVisible(true);

            while (!SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                height += EditorGUI.GetPropertyHeight(serializedProperty);
                serializedProperty.NextVisible(false);
                height += EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }

        public static void AddElementToArray<T>(this SerializedProperty serializedProperty, T unityObject) where  T : UnityEngine.Object
        {
            if(!serializedProperty.isArray)
            {
                throw new Exception("SerializedProperty is not of type array");
            }

            serializedProperty.arraySize++;

            var elementProperty = serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1);

            elementProperty.objectReferenceValue = unityObject;
        }

        public static void RemoveElementFromArray(
            this SerializedProperty serializedProperty,
            int index
        )
        {
            if(!serializedProperty.isArray)
            {
                throw new Exception("SerializedProperty is not of type array");
            }

            if(serializedProperty.arraySize <= index)
            {
                return;
            }

            var oldSize = serializedProperty.arraySize;

            serializedProperty.DeleteArrayElementAtIndex(index);

            if (serializedProperty.arraySize == oldSize)
            {
                serializedProperty.DeleteArrayElementAtIndex(index);
            }
        }
    }
}
#endif
