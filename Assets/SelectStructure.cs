using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStructure : MonoBehaviour
{

    public Transform associated;

	void Start () {
		GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
        PlaceStructureOnMap.ChangeStructure(associated.gameObject);
        Debug.Log("Structure Changed");
	}
}
