using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour, ITemplatable
{
    static int seed = 0;
    public int id;
    public string playerName;
    public List<Army> Armys { get; private set; }
    public bool Activited { get; private set; }
    public Color chiefColor;
    public int leftMoveTimes;

    // Use this for initialization
    void Start () {
        Armys = new List<Army>();
        leftMoveTimes = 0;
        Activited = false;
    }
	
	// Update is called once per frame
	void Update () {

    }
    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        obj = Instantiate(gameObject, position, rotation) as GameObject;
        obj.GetComponent<Player>().id = seed++;
        return obj;
    }

    internal void EndTurn()
    {
        leftMoveTimes = 0;
        Activited = false;
    }

    internal void StartTurn()
    {
        leftMoveTimes = 1;
        Activited = true;
    }
}
