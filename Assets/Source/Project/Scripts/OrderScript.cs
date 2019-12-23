#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class OrderScript : MonoBehaviour
{
    public void SelectGameObjects()
    {
        List<Transform> transforms = new List<Transform>();
        transforms.AddRange(Selection.transforms);
        if (transforms.Count <= 2)
            return;

        //Get the first bigger distance between two of transforms selected
        Vector3 maxPointA = Vector3.zero;
        Vector3 maxPointB = Vector3.zero;

        Vector3 testPointA = Vector3.zero;
        Vector3 testPointB = Vector3.zero;

        for (int i = 0; i < transforms.Count; i++)
        {
            testPointA = transforms[i].position;
            for (int j = i + 1; j < transforms.Count; j++)
            {
                testPointB = transforms[j].position;
                if (Vector3.Distance(testPointA, testPointB) > Vector3.Distance(maxPointA, maxPointB))
                {
                    maxPointA = testPointA;
                    maxPointB = testPointB;
                }
            }
        }

        //Sort transforms selected by near to far of maxPointA
        transforms = MergeSort(transforms, maxPointA);

        //Distribute gameObjects at equal distance between maxPointA to maxPointB
        var normalVector = (maxPointB - maxPointA).normalized;
        var step = Vector3.Distance(maxPointA, maxPointB) / (transforms.Count - 1);
        for (int i = 0; i < transforms.Count; i++)
        {
            transforms[i].position = maxPointA + (normalVector * (i * step));
        }
    }

    private List<Transform> MergeSort(List<Transform> transformsSorted, Vector3 pivot)
    {
        if (transformsSorted.Count <= 1)
            return transformsSorted;

        List<Transform> groupA = new List<Transform>();
        List<Transform> groupB = new List<Transform>();

        int middleIndex = transformsSorted.Count / 2;
        for (int i = 0; i < middleIndex; i++)
        {
            groupA.Add(transformsSorted[i]);
        }
        for (int i = middleIndex; i < transformsSorted.Count; i++)
        {
            groupB.Add(transformsSorted[i]);
        }

        groupA = MergeSort(groupA, pivot);
        groupB = MergeSort(groupB, pivot);
        return Merge(groupA, groupB, pivot);
    }

    private List<Transform> Merge(List<Transform> groupA, List<Transform> groupB, Vector3 pivot)
    {
        List<Transform> transformsSorted = new List<Transform>();

        while (groupA.Count > 0 || groupB.Count > 0)
        {
            if (groupA.Count > 0 && groupB.Count > 0)
            {
                if (Vector3.Distance(groupA.First().position, pivot) <= Vector3.Distance(groupB.First().position, pivot))
                {
                    transformsSorted.Add(groupA.First());
                    groupA.Remove(groupA.First());
                }
                else
                {
                    transformsSorted.Add(groupB.First());
                    groupB.Remove(groupB.First());
                }
            }
            else if (groupA.Count > 0)
            {
                transformsSorted.Add(groupA.First());
                groupA.Remove(groupA.First());
            }
            else if (groupB.Count > 0)
            {
                transformsSorted.Add(groupB.First());
                groupB.Remove(groupB.First());
            }
        }
        return transformsSorted;
    }
}
#endif