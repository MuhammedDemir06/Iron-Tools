using IronTools.Attributes;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowDividerAttribute))]
public class ShowDividerDrawer : DecoratorDrawer
{
    private const float LineHeight = 2f;
    private const float SpaceBefore = 10f;
    private const float SpaceAfter = 5f;
    private const float LabelHeight = 18f;

    // Lazy-loaded style cache
    private static GUIStyle _centeredBigBoldLabel;
    private static GUIStyle CenteredBigBoldLabel
    {
        get
        {
            if (_centeredBigBoldLabel == null)
            {
                _centeredBigBoldLabel = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 14,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = EditorStyles.boldLabel.normal.textColor }
                };
            }
            return _centeredBigBoldLabel;
        }
    }

    public override float GetHeight()
    {
        ShowDividerAttribute attr = (ShowDividerAttribute)attribute;
        return SpaceBefore + LineHeight + (string.IsNullOrEmpty(attr.Title) ? 0 : LabelHeight) + SpaceAfter;
    }

    public override void OnGUI(Rect position)
    {
        // ✅ Thread safety — Only run in main thread
        if (System.Threading.Thread.CurrentThread.ManagedThreadId != 1)
            return;

        ShowDividerAttribute attr = (ShowDividerAttribute)attribute;

        float y = position.y + SpaceBefore;

        // Draw title if present
        if (!string.IsNullOrEmpty(attr.Title))
        {
            var labelRect = new Rect(position.x, y - 2f, position.width, LabelHeight);
            EditorGUI.LabelField(labelRect, attr.Title, CenteredBigBoldLabel);
            y += LabelHeight;
        }

        // Draw divider line
        var lineRect = new Rect(position.x, y, position.width, LineHeight);
        EditorGUI.DrawRect(lineRect, EditorColorPalette.Resolve(attr.Color));
    }
}
