using UnityEngine;

public class CubeChange : MonoBehaviour
{
    public GameObject cube;

    [Range(0, 1)]
    public float sizeReduce;

    public CubeScriptableObject cubeScriptableObject;

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Selected GameObject: " + cube.name);

        GUI.Label(new Rect(10, 40, 200, 20), "Reduce size");
        sizeReduce = GUI.HorizontalSlider(new Rect(10, 70, 90, 20), sizeReduce, 0, 1);
        GUI.Label(new Rect(110, 70, 90, 20), sizeReduce.ToString());

        var size = cubeScriptableObject.size * sizeReduce;
        if (size == Vector3.zero)
            size = cubeScriptableObject.size * 0.01f;

        cube.transform.localScale = size;

        GUI.Label(new Rect(10, 100, 200, 20), "ScriptableObject: " + cubeScriptableObject.name);
    }

}
