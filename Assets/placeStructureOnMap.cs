using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeStructureOnMap : MonoBehaviour
{

    GameObject nextStructure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nextStructure != null){
            if (Input.GetMouseButtonDown(0)){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit)){
                    Vector3 hitPosition = hit.collider.transform.position;
                    Debug.Log("Mouse Down hit: "+hitPosition);
                    nextStructure.transform.position = new Vector3(hitPosition.x,0,hitPosition.z);
                    // Do something
                    nextStructure = null;
                }
            }
        }
    }
}
