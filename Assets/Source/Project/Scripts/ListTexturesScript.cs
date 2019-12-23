#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class ListTexturesScript : MonoBehaviour
{
    public List<string> texturesNames;

    public void ListTexturesFileSize()
    {
        List<string> files = new List<string>();
        texturesNames = new List<string>();

        files.AddRange(
            Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".bmp") || f.EndsWith(".exr") || f.EndsWith(".gif")
                         || f.EndsWith(".hdr") || f.EndsWith(".iff") || f.EndsWith(".jpg")
                         || f.EndsWith(".pict") || f.EndsWith(".png") || f.EndsWith(".psd")
                         || f.EndsWith(".tga") || f.EndsWith(".tiff"))
                .OrderBy(f => new FileInfo(f).Length)
        );

        foreach (string file in files)
        {
            var index = file.LastIndexOf("/");
            var path = file.Substring(index + 1);

            var texture = (Texture)AssetDatabase.LoadAssetAtPath(path, typeof(Texture));
            texturesNames.Add(texture.name);
        }
    }

    public void ListTexturesAreaSize()
    {
        List<string> files = new List<string>();
        List<Texture> textures = new List<Texture>();
        texturesNames = new List<string>();

        files.AddRange(
            Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".bmp") || f.EndsWith(".exr") || f.EndsWith(".gif")
                         || f.EndsWith(".hdr") || f.EndsWith(".iff") || f.EndsWith(".jpg")
                         || f.EndsWith(".pict") || f.EndsWith(".png") || f.EndsWith(".psd")
                         || f.EndsWith(".tga") || f.EndsWith(".tiff"))
        );

        foreach (string file in files)
        {
            var index = file.LastIndexOf("/");
            var path = file.Substring(index + 1);

            var texture = (Texture)AssetDatabase.LoadAssetAtPath(path, typeof(Texture));
            textures.Add(texture);
        }

        textures = MergeSort(textures);

        foreach (Texture texture in textures)
            texturesNames.Add(texture.name);
    }

    private List<Texture> MergeSort(List<Texture> texturesSorted)
    {
        if (texturesSorted.Count <= 1)
            return texturesSorted;

        List<Texture> groupA = new List<Texture>();
        List<Texture> groupB = new List<Texture>();

        int middleIndex = texturesSorted.Count / 2;
        for (int i = 0; i < middleIndex; i++)
        {
            groupA.Add(texturesSorted[i]);
        }
        for (int i = middleIndex; i < texturesSorted.Count; i++)
        {
            groupB.Add(texturesSorted[i]);
        }

        groupA = MergeSort(groupA);
        groupB = MergeSort(groupB);
        return Merge(groupA, groupB);
    }

    private List<Texture> Merge(List<Texture> groupA, List<Texture> groupB)
    {
        List<Texture> texturesSorted = new List<Texture>();

        while (groupA.Count > 0 || groupB.Count > 0)
        {
            if (groupA.Count > 0 && groupB.Count > 0)
            {
                if (groupA.First().width* groupA.First().height <= groupB.First().width * groupB.First().height)
                {
                    texturesSorted.Add(groupA.First());
                    groupA.Remove(groupA.First());
                }
                else
                {
                    texturesSorted.Add(groupB.First());
                    groupB.Remove(groupB.First());
                }
            }
            else if (groupA.Count > 0)
            {
                texturesSorted.Add(groupA.First());
                groupA.Remove(groupA.First());
            }
            else if (groupB.Count > 0)
            {
                texturesSorted.Add(groupB.First());
                groupB.Remove(groupB.First());
            }
        }
        return texturesSorted;
    }
}
#endif