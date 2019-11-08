using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public int levelNum = -1;
    public List<ItemInfo> items = new List<ItemInfo>();

    public LevelInfo() { }

    public LevelInfo(int num, GameObject rootObject)
    {
        levelNum = num;
        items = new List<ItemInfo>();

        foreach(Transform child in rootObject.transform)
        {
            items.Add(new ItemInfo(child));
        }
    }
}

[Serializable]
public class LevelInfoCollection
{
    public Dictionary<int, LevelInfo> levels = new Dictionary<int, LevelInfo>();

    public LevelInfoCollection() { }
    public LevelInfoCollection(Dictionary<int, LevelInfo> levels)
    {
        this.levels = levels;
    }
    
}

[Serializable]
public class ItemInfo
{
    public string prefabName;
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;

    public ItemInfo() { }

    public ItemInfo(Transform item)
    {
        int index = item.name.IndexOf('(');
        if(index > 1 && item.name[index - 1].Equals(' ')) index -= 1;

        prefabName = index > 0 ? item.name.Substring(0, index) : item.name;
        position = item.position;
        scale = item.localScale;
        rotation = item.rotation;
    }
}
