using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public delegate void NodeEvent(Node sender, EventArgs e);

public class Node : MonoBehaviour,ITemplatable
{
    public bool accessible;
    public bool attackable;
    public int id;
    public GameObject nodeHalo;
    public bool selected;
    static int seed = 0;
    GameObject halo;
    public Node()
    {
        Roads = new List<Road>();
        selected = false;
        accessible = false;
        attackable = false;
    }

    public event NodeEvent OnHoverEnter;

    public event NodeEvent OnHoverExit;

    public Army Army { get; set; }
    public List<Road> Roads { get; set; }
    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        obj = Instantiate(gameObject, position, rotation) as GameObject;
        obj.GetComponent<Node>().id = seed++;
        return obj;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public void HideHalo()
    {
        if (halo != null)
        {
            halo.SetActive(false);
            accessible = false;
            attackable = false;
        }
    }

    public void OnMouseEnter()
    {
        if (!selected)
            SetHovered();

        if (OnHoverEnter != null)
            OnHoverEnter(this, new EventArgs());
    }

    public void OnMouseExit()
    {
        if (accessible)
            SetAccessible();
        else if (attackable)
            SetAttackable();
        else if (!selected)
            HideHalo();

        if (OnHoverExit != null)
            OnHoverExit(this, new EventArgs());
    }

    public void Select()
    {
        ActiveHalo();
        halo.GetComponent<NodeHalo>().SetSelected();
        selected = true;
    }

    public void SetAccessible()
    {
        ActiveHalo();
        halo.GetComponent<NodeHalo>().SetAccessible();
        accessible = true;
    }

    public void SetHovered()
    {
        ActiveHalo();
        halo.GetComponent<NodeHalo>().SetColor(Color.yellow);
    }

    private void ActiveHalo()
    {
        if (halo == null)
        {
            halo = Instantiate(nodeHalo, Vector3.zero, Quaternion.identity) as GameObject;
            halo.transform.SetParent(transform);
        }
        halo.SetActive(true);
    }

    // Use this for initialization
    void Start () 
    {
        if (nodeHalo == null)
        {
            nodeHalo = Resources.Load<GameObject>("NodeHalo");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void SetAttackable()
    {
        ActiveHalo();
        halo.GetComponent<NodeHalo>().SetAttackable();
        attackable = true;
    }
}
