#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection;
using IronTools.Attributes;

[CustomEditor(typeof(MonoBehaviour), true)]
public class SpecialButtonEditor : Editor
{
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
                {
                    icon = Resources.Load<Texture2D>(buttonAttr.IconPath);
                }

                GUIContent content = new GUIContent(label, icon, "Click to invoke " + label);

                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.imagePosition = ImagePosition.ImageLeft;

                if (GUILayout.Button(content,buttonStyle, GUILayout.Height(30)))
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}
#endif
