using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.Linq;

public class NaturalSortChildren : EditorWindow
{
    [MenuItem("Tools/Sort Children By Name (Natural Order)")]
    static void SortChildren()
    {
        GameObject selected = Selection.activeGameObject;

        if (selected == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        Transform parent = selected.transform;

        Transform[] children = new Transform[parent.childCount];
        for (int i = 0; i < parent.childCount; i++)
        {
            children[i] = parent.GetChild(i);
        }

        // Natural sort: compare names by numeric value where possible
        System.Array.Sort(children, (a, b) => NaturalCompare(a.name, b.name));

        for (int i = 0; i < children.Length; i++)
        {
            children[i].SetSiblingIndex(i);
        }

        Debug.Log("Children of " + selected.name + " sorted naturally.");
    }

    static int NaturalCompare(string a, string b)
    {
        return EditorUtility.NaturalCompare(a, b);
    }
}