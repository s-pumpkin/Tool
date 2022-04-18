using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attribute.ListToPopup;

public class ListToPopupTest : MonoBehaviour, ISerializationCallbackReceiver
{
    [ListToPopup] public string SceneName;

    public static List<string> test1 = new List<string>() { "1", "2", "3" };
    [ListToPopup(typeof(ListToPopupTest), "test1")] public string TestPopup1;

    public static List<string> test2 = new List<string>();
    [ListToPopup(typeof(ListToPopupTest), "test2")] public string TestPopup2;
    [HideInInspector] public List<string> TMPList = new List<string>() { "A", "B", "C" };

    public void OnBeforeSerialize()
    {
        test2 = TMPList;
    }

    public void OnAfterDeserialize() { }
}
