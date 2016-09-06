using UnityEngine;
using System.Collections;

public class Infantry : Army {

    // Use this for initialization
    protected override void Start () 
    {
        base.Start();

        var meshs = GetComponentsInChildren<MeshFilter>();
        //Debug.Log("Start Change Color");
        foreach (var m in meshs)
        {
            if (m.name == "Body")
            {
                ColorPart.Add(m.gameObject);
                m.gameObject.GetComponent<MeshRenderer>().material.color = Owner.chiefColor;
                //Debug.Log("Changed Color");
                break;
            }
        }
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
