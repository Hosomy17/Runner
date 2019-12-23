#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OrderScript))]
public class OrderScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OrderScript orderScript = (OrderScript)target;

        if (GUILayout.Button("Order All Selected"))
        {
            orderScript.SelectGameObjects();
        }
    }
}
#endif