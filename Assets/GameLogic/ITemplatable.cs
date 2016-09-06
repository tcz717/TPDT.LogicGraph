using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

interface ITemplatable
{
    GameObject Clone(Vector3 position, Quaternion rotation);
}
static class GameObjectExtension
{
    public static GameObject Clone<T>(this GameObject gameObject, Vector3 position, Quaternion rotation)
        where T : ITemplatable
    {
        T temp = gameObject.GetComponent<T>();
        return temp.Clone(position, rotation);
    }
    public static GameObject Clone<T>(this GameObject gameObject, Vector3 position)
        where T : ITemplatable
    {
        return gameObject.Clone<T>(position, gameObject.transform.rotation);
    }
    public static GameObject Clone<T>(this GameObject gameObject)
        where T : ITemplatable
    {
        return gameObject.Clone<T>(gameObject.transform.position, gameObject.transform.rotation);
    }
}
