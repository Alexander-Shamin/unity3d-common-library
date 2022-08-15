namespace UCL.Assets.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    public sealed class EditorWindowLock : Editor
    {
        private static EditorWindow _activeProject;

        [MenuItem("UCL/Hot actions/Toggle Lock %SPACE")]
        static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        [MenuItem("UCL/Hot actions/Toggle Project Lock %#SPACE")]
        static void ToggleProjectLock()
        {
            if (_activeProject == null)
            {
                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
                System.Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
                _activeProject = (EditorWindow)findObjectsOfTypeAll[0];
            }

            if (_activeProject != null && _activeProject.GetType().Name == "ProjectBrowser")
            {
                Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.ProjectBrowser");
                PropertyInfo propertyInfo = type.GetProperty("isLocked", BindingFlags.Instance |
                                                                         BindingFlags.NonPublic |
                                                                         BindingFlags.Public);

                bool value = (bool)propertyInfo.GetValue(_activeProject, null);

                propertyInfo.SetValue(_activeProject, !value, null);

                _activeProject.Repaint();
            }
        }
    }
}
