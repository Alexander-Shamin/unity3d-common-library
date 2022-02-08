using UnityEditor;
using UnityEngine;

namespace Common
{
[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredAttributeDrawer : PropertyDrawer
{
  public override void OnGUI(Rect inRect, SerializedProperty inProp, GUIContent label)
  {
    EditorGUI.BeginProperty(inRect, label, inProp);
    if (inProp.objectReferenceValue == null)
    {
      label.text = "[!!!] " + label.text;
      label.tooltip = "This filed cannot be None!";
      GUI.color = Color.red;
    }
    EditorGUI.PropertyField(inRect, inProp, label);
    GUI.color = Color.white;
    EditorGUI.EndProperty();
  }
}
}  // namespace
