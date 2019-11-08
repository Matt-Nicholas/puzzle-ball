using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class ObjectData:MonoBehaviour
{
    int id;


    float xPos;
    float yPos;
    float xRot;
    float yRot;
    float xScale;
    float yScale;



    public GameObject CubeContainer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class ItemInfo
    {
        public string prefabName;
        public Vector3 position;
        public Vector3 scale;
        public Quaternion rotation;

        public ItemInfo()
        {
        }

        public ItemInfo(Transform item)
        {

            int index = item.name.IndexOf('(') - 1;
            prefabName = index > 0 ? item.name.Substring(0, index) : item.name;
            position = item.position;
            scale = item.localScale;
            rotation = item.rotation;
        }
    }


    public class LevelInfo
    {
        public List<ItemInfo> itemList;

        public LevelInfo(){}

        public LevelInfo(GameObject rootObject)
        {
            itemList = new List<ItemInfo>();

            foreach(Transform child in rootObject.transform)
            {
                itemList.Add(new ItemInfo(child));
            }
        }

        public void Reload(GameObject rootObject)
        {
            // Rebuild the cubes after loading building info:
            foreach(var cubeInfo in itemList)
            {
                GameObject cube = Instantiate(Resources.Load(cubeInfo.prefabName), cubeInfo.position, Quaternion.identity) as GameObject;
                cube.transform.parent = rootObject.transform;
            }
        }
    }


    void Save(GameObject rootObject, string filename)
    {
        LevelInfo buildingInfo = new LevelInfo(rootObject);
        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
        TextWriter writer = new StreamWriter(filename);
        serializer.Serialize(writer, buildingInfo);
        writer.Close();
        print("Objects saved into XML file\n");
    }

    void Load(GameObject rootObject, string filename)
    {
        //        while(rootObject.transform.GetChildCount()>0)
        //        {
        //            GameObject.Destroy(rootObject.transform.GetChild (0));
        //        } 

        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
        TextReader reader = new StreamReader(filename);
        LevelInfo buildingInfo = serializer.Deserialize(reader) as LevelInfo;
        buildingInfo.Reload(rootObject);
        reader.Close();
        print("Objects loaded from XML file\n");
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(30, 60, 150, 30), "Save State"))
        {
            Save(CubeContainer, "savefile.xml");
        }

        if(GUI.Button(new Rect(30, 90, 150, 30), "Load State"))
        {
            Load(CubeContainer, "savefile.xml");
        }
    }
}

