#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeChange))]
public class CubeChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Change ScriptableObjects"))
        {
            CubeChange cubeChange = (CubeChange)target;
            cubeChange.cube.transform.localScale = cubeChange.cubeScriptableObject.size;
        }
    }
}
#endif