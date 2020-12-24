using UnityEngine;
using UnityEditor;

namespace Common
{

	[CustomEditor(typeof(EventScriptableObject), true)]
	public class EventScriptableObjectEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var e = target as EventScriptableObject;
			if (GUILayout.Button("Raise a value change event"))
				e.Raise();
		}

	}

}
