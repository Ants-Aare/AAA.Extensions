using UnityEngine;
using UnityEditor;
using Rect = UnityEngine.Rect;

namespace AAA.GlobalVariables.Variables
{
    public class GlobalVariableReferenceDrawer : PropertyDrawer
    {
        private const int PaddingSize = 4;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);

            var useConstantProperty = property.FindPropertyRelative("UseConstant");
            if (!useConstantProperty.boolValue)
            {
                var variableProperty = property.FindPropertyRelative("Variable");
                if (variableProperty.objectReferenceValue != null)
                {
                    height += (EditorGUIUtility.singleLineHeight * 2)
                              + (EditorGUIUtility.standardVerticalSpacing * 2);
                }

                height += PaddingSize * 2;
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var useConstantProperty = property.FindPropertyRelative("UseConstant");

            DrawDropDown(position, useConstantProperty);
            DrawProperties(position, useConstantProperty, property);

            EditorGUI.EndProperty();
        }

        private static void DrawDropDown(Rect position, SerializedProperty useConstantProperty)
        {
            position.position += Vector2.left * 15;
            var rect = new Rect(position.position, Vector2.one * 20);
            var content = EditorGUIUtility.IconContent("Icon Dropdown");
            var style = new GUIStyle() { fixedWidth = 50f, border = new RectOffset(1, 1, 1, 1) };

            if (EditorGUI.DropdownButton(rect, content, FocusType.Keyboard, style))
                ShowContextMenu(useConstantProperty);
        }

        private void DrawProperties(Rect position, SerializedProperty useConstantProperty, SerializedProperty property)
        {
            if (useConstantProperty.boolValue)
                EditorGUI.PropertyField(position, property.FindPropertyRelative("ConstantValue"), GUIContent.none);
            else
            {
                var variableProperty = property.FindPropertyRelative("Variable");
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.ObjectField(position, variableProperty, GUIContent.none);
                if (variableProperty.objectReferenceValue != null)
                {
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    position.height = (EditorGUIUtility.singleLineHeight * 2)
                                      + EditorGUIUtility.standardVerticalSpacing
                                      + (PaddingSize * 2);
                    GUI.BeginGroup(position, EditorStyles.helpBox);
                    //EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    var serializedObject = new SerializedObject(variableProperty.objectReferenceValue);
                    EditorGUI.BeginChangeCheck();

                    var valueProperty = serializedObject.FindProperty("value");
                    var defaultValueProperty = serializedObject.FindProperty("defaultValue");

                    var localRect = new Rect(PaddingSize, PaddingSize, position.width - (PaddingSize * 2),
                        EditorGUIUtility.singleLineHeight);

                    EditorGUIUtility.labelWidth = 35;
                    EditorGUI.PropertyField(localRect, valueProperty);

                    localRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                    EditorGUIUtility.labelWidth = 44;
                    EditorGUI.PropertyField(localRect, defaultValueProperty);

                    if (EditorGUI.EndChangeCheck())
                    {
                        ((IValidatable)variableProperty.objectReferenceValue).OnValidate();
                        serializedObject.ApplyModifiedProperties();
                    }

                    GUI.EndGroup();
                    //EditorGUILayout.EndVertical();
                }
            }
        }

        private static void ShowContextMenu(SerializedProperty useConstantProperty)
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Use Constant"), useConstantProperty.boolValue,
                () => SetProperty(useConstantProperty, true));
            menu.AddItem(new GUIContent("Use Variable"), !useConstantProperty.boolValue,
                () => SetProperty(useConstantProperty, false));
            menu.ShowAsContext();
        }

        private static void SetProperty(SerializedProperty useConstantProperty, bool value)
        {
            useConstantProperty.boolValue = value;
            useConstantProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}