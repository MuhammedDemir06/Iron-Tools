#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using IronTools.Attributes;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    private Dictionary<string, object[]> paramValues = new Dictionary<string, object[]>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var buttonAttr = method.GetCustomAttribute<ButtonAttribute>();
            if (buttonAttr != null)
            {
                string label = string.IsNullOrEmpty(buttonAttr.ButtonLabel) ? method.Name : buttonAttr.ButtonLabel;

                Texture2D icon = null;
                if (!string.IsNullOrEmpty(buttonAttr.IconPath))
                    icon = Resources.Load<Texture2D>(buttonAttr.IconPath);

                GUIContent content = buttonAttr.OnlyIcon
                    ? new GUIContent(icon)
                    : new GUIContent(label, icon);

                var parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    if (!paramValues.ContainsKey(method.Name))
                        paramValues[method.Name] = new object[parameters.Length];

                    if (!buttonAttr.OnlyIcon)
                        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var param = parameters[i];
                        paramValues[method.Name][i] = DrawParameterField(param, paramValues[method.Name][i]);
                    }

                    if (GUILayout.Button(content, GUILayout.Height(30)))
                    {
                        method.Invoke(target, paramValues[method.Name]);
                    }
                }
                else
                {
                    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

                    if (buttonAttr.OnlyIcon)
                    {
                        buttonStyle.imagePosition = ImagePosition.ImageOnly;
                        if (GUILayout.Button(content, buttonStyle, GUILayout.Width(70), GUILayout.Height(40)))
                        {
                            method.Invoke(target, null);
                        }
                    }
                    else
                    {
                        buttonStyle.imagePosition = ImagePosition.ImageLeft;
                        if (GUILayout.Button(content, buttonStyle, GUILayout.Height(30)))
                        {
                            method.Invoke(target, null);
                        }
                    }


                    if (buttonAttr.OnlyIcon && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition) && icon != null)
                    {
                        GUI.Label(new Rect(Event.current.mousePosition.x + 15, Event.current.mousePosition.y, 100, 20), method.Name, EditorStyles.helpBox);
                        Repaint(); 
                    }
                    else if (buttonAttr.OnlyIcon && Event.current.type == EventType.MouseDown)
                    {
                        method.Invoke(target, null);
                    }
                }
            }
        }
    }



    private object DrawParameterField(ParameterInfo param, object currentValue)
    {
        var type = param.ParameterType;

        if (type == typeof(int))
        {
            return EditorGUILayout.IntField(param.Name, currentValue != null ? (int)currentValue : 0);
        }
        else if (type == typeof(float))
        {
            return EditorGUILayout.FloatField(param.Name, currentValue != null ? (float)currentValue : 0f);
        }
        else if (type == typeof(bool))
        {
            return EditorGUILayout.Toggle(param.Name, currentValue != null ? (bool)currentValue : false);
        }
        else if (type == typeof(string))
        {
            return EditorGUILayout.TextField(param.Name, currentValue != null ? (string)currentValue : "");
        }
        else if (type == typeof(Vector3))
        {
            return EditorGUILayout.Vector3Field(param.Name, currentValue != null ? (Vector3)currentValue : Vector3.zero);
        }
        else if (type.IsEnum)
        {
            return EditorGUILayout.EnumPopup(param.Name, currentValue != null ? (System.Enum)currentValue : (System.Enum)System.Enum.GetValues(type).GetValue(0));
        }
        else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
        {
            return EditorGUILayout.ObjectField(param.Name, currentValue as UnityEngine.Object, type, true);
        }
        else
        {
            EditorGUILayout.LabelField(param.Name, $"Unsupported type: {type.Name}");
            return null;
        }
    }
}
#endif
