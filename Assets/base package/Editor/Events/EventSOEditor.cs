using UnityEngine;
using UnityEditor;

namespace Common
{

    [CustomEditor(typeof(EventSO), true)]
    public class EventSOEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = target as EventSO;
            if (GUILayout.Button("Raise a value change event"))
                e.Raise();
        }

    }

}
