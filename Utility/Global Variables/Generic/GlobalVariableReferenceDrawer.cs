using UnityEngine;
using UnityEditor;

namespace AAA.Utility.GlobalVariables
{
    public class GlobalVariableReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var useConstantProperty = property.FindPropertyRelative("UseConstant");

            position = DrawDropDown(position, useConstantProperty);
            DrawProperties(position, useConstantProperty, property);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var useConstantProperty = property.FindPropertyRelative("UseConstant");

            return base.GetPropertyHeight(property, label) + (useConstantProperty.boolValue ? 0 : EditorGUIUtility.singleLineHeight);
        }

        private static Rect DrawDropDown(Rect position, SerializedProperty useConstantProperty)
        {
            var rect = new Rect(position.position, Vector2.one * 20);
            var content = EditorGUIUtility.IconContent("Icon Dropdown");
            var style = new GUIStyle() { fixedWidth = 50f, border = new RectOffset(1, 1, 1, 1) };

            if (EditorGUI.DropdownButton(rect, content, FocusType.Keyboard, style))
                ShowContextMenu(useConstantProperty);

            position.position += Vector2.right * 15;
            position.width -= 15;
            return position;
        }

        private void DrawProperties(Rect position, SerializedProperty useConstantProperty, SerializedProperty property)
        {
            if (useConstantProperty.boolValue)
                EditorGUI.PropertyField(position, property.FindPropertyRelative("ConstantValue"), GUIContent.none);
            else
            {
                var variableProperty = property.FindPropertyRelative("Variable");
                EditorGUI.ObjectField(position, variableProperty, GUIContent.none);
                // if (variableProperty.objectReferenceValue != null)
                // {
                //     position.yMax -= EditorGUIUtility.singleLineHeight;
                //     position.yMin -= EditorGUIUtility.singleLineHeight;
                //     EditorGUI.PropertyField(position, variableProperty.FindPropertyRelative("value"), GUIContent.none);
                // }
            }
        }

        private static void ShowContextMenu(SerializedProperty useConstantProperty)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Constant"), useConstantProperty.boolValue, () => SetProperty(useConstantProperty, true));
            menu.AddItem(new GUIContent("Use Variable"), !useConstantProperty.boolValue, () => SetProperty(useConstantProperty, false));
            menu.ShowAsContext();
        }

        private static void SetProperty(SerializedProperty useConstantProperty, bool value)
        {
            useConstantProperty.boolValue = value;
            useConstantProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}