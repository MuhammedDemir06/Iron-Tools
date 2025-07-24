#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class IronToolsSetupWizard : EditorWindow
{
    [MenuItem("Tools/IronTools Setup Wizard")]
    public static void ShowWindow()
    {
        GetWindow<IronToolsSetupWizard>("IronTools Setup");
    }

    private Vector2 scrollPos;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Welcome to IronTools Attribute System", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.HelpBox("This tool helps you integrate IronTools Attributes into your project.", MessageType.Info);

        EditorGUILayout.LabelField("Step 1: Import the IronTools package into your project.");
        EditorGUILayout.LabelField("Step 2: Add the 'using IronTools.Attributes;' directive in your scripts.");
        EditorGUILayout.LabelField("Step 3: Use the provided attributes to decorate your classes and properties.");
        EditorGUILayout.LabelField("Step 4: Use the custom editor drawers that come with IronTools for enhanced inspector experience.");

        EditorGUILayout.Space();

        if (GUILayout.Button("Open Documentation"))
        {
            Application.OpenURL("https://github.com/MuhammedDemir06/odin-inspector-tools#readme");
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("My Youtube Channel"))
        {
            Application.OpenURL("https://www.youtube.com/@issoeEnt/videos");
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Visit Support / Contact"))
        {
            Application.OpenURL("https://www.linkedin.com/in/muhammed-demir-945625311");
        }

        EditorGUILayout.EndScrollView();
    }
}
#endif
