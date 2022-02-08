using UnityEngine;
using UnityEditor;

namespace Common
{
#if UNITY_EDITOR
[CustomEditor(typeof(OnSettingsChangedSO), true)]
public class EventScriptableObjectEditor : Editor
{
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    var e = target as OnSettingsChangedSO;
    if (GUILayout.Button("Raise a value change event")) e?.InvokeOnSettingsChanged();
  }
}
#endif
}
