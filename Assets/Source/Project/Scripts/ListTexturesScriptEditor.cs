#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ListTexturesScript))]
public class ListTexturesScriptEditor : Editor
{
    int indexTexture = 0;
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        ListTexturesScript listTexturesScript = (ListTexturesScript)target;

        GUILayout.Label("Textures");
        indexTexture = EditorGUILayout.Popup(indexTexture, listTexturesScript.texturesNames.ToArray());

        if (GUILayout.Button("List Textures By File Size"))
        {
            listTexturesScript.ListTexturesFileSize();
        }

        if (GUILayout.Button("List Textures By Area Size"))
        {
            listTexturesScript.ListTexturesAreaSize();
        }
    }
}
#endif