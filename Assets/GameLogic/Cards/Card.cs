using UnityEngine;
using System.Collections;
using System;

public class Card : MonoBehaviour, ITemplatable
{
    static int seed = 0;
    public int id;
    public Player Owner { get; set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Use()
    {
    }

    public virtual bool CanUse()
    {
        return false;
    }

    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        obj = Instantiate(gameObject, position, rotation) as GameObject;
        obj.GetComponent<Card>().id = seed++;
        return obj;
    }
}
