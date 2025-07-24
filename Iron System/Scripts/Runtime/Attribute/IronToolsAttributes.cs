namespace IronTools.Attributes
{
    using System;
    using UnityEngine;

    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionFieldName;
        public ShowIfAttribute(string conditionFieldName)
        {
            ConditionFieldName = conditionFieldName;
        }
    }

    public class ButtonAttribute : Attribute
    {
        public string ButtonLabel;
        public string IconPath;
        public bool OnlyIcon;

        public ButtonAttribute(string label = null, string iconPath = null, bool onlyIcon = false)
        {
            ButtonLabel = label;
            IconPath = iconPath;
            OnlyIcon = onlyIcon;
        }
    }
}
