using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {
  public GameObject buttonPrefab;
  public GameObject countText;

  public List<GameObject> items = new List<GameObject>();

	void Start () {

    for(int i = 0; i < items.Count; i++) {
      GameObject go = Instantiate(buttonPrefab, transform);
      GameObject cText = Instantiate(countText, transform);

      cText.GetComponent<Text>().text = " x" + (i + 1);      

    }
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  public void InitButtons(List<GameObject> items) {

    this.items = items;

    for (int i = 0; i < items.Count; i++) {
      //Button butt = Instantiate(buttonPrefab).GetComponent<Button>();

      //butt.text = items[i].name + " " + i;

    }
  }
}
