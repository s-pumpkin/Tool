using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Attribute.ListToPopup;
using System;

namespace Attribute.ListToPopup
{
    public enum PopupModes { SceneName, StaticList }


    public class ListToPopupAttribute : PropertyAttribute
    {
        public PopupModes MyMobde;
        public Type MyType;
        public string PropertyName;

        /// <summary>
        /// SceneName Mode
        /// </summary>
        public ListToPopupAttribute() { MyMobde = PopupModes.SceneName; }

        /// <summary>
        /// staticList
        /// </summary>
        /// <param name="_MyType"></param>
        /// <param name="_PropertyName"></param>
        public ListToPopupAttribute(Type _MyType, string _PropertyName)
        {
            MyMobde = PopupModes.StaticList;
            MyType = _MyType;
            PropertyName = _PropertyName;
        }
    }
}

[CustomPropertyDrawer(typeof(ListToPopupAttribute))]
public class ListToPopupAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ListToPopupAttribute trager = attribute as ListToPopupAttribute;
        List<string> stringList = new List<string>();

        switch (trager.MyMobde)
        {
            case PopupModes.SceneName:
                stringList = SceneName();
                break;
            case PopupModes.StaticList:
                stringList = StringList(trager);
                break;
        }

        if (stringList != null && stringList.Count != 0)
        {
            int selectedIndex = Mathf.Max(stringList.IndexOf(property.stringValue), 0);
            selectedIndex = EditorGUI.Popup(position, property.name, selectedIndex, stringList.ToArray());
            property.stringValue = stringList[selectedIndex];
        }
        else EditorGUI.PropertyField(position, property, label);
    }

    public List<string> SceneName()
    {
        List<string> sceneNameList = new List<string>();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
            sceneNameList.Add(sceneName);
        }

        return sceneNameList;
    }

    public List<string> StringList(ListToPopupAttribute trager)
    {
        List<string> stringList = new List<string>();

        if (trager.MyType.GetField(trager.PropertyName) != null)
        {
            stringList = trager.MyType.GetField(trager.PropertyName).GetValue(trager.MyType) as List<string>;
        }

        return stringList;
    }
}