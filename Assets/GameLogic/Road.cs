using UnityEngine;
using System.Collections;
using TPDT.LogicGraph.Base;
using System;

public class Road : MonoBehaviour, ITemplatable
{
    static int seed = 0;
    public int id;

    public float MidDistance;
    public Node Node1;
    public Node Node2;
    public RoadDisplayMode RoadMode;

    RoadRender render;
    bool setmid;
    private void SetLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3 p;
        switch (RoadMode)
        {
            case RoadDisplayMode.Direct:
                render.SetVertexCount(2);
                render.SetPosition(0, pos1);
                render.SetPosition(1, pos2);
                break;
            case RoadDisplayMode.UpTurning:
                if (pos1.z > pos2.z)
                    p = new Vector3(pos2.x, 0, pos1.z);
                else
                    p = new Vector3(pos1.x, 0, pos2.z);
                render.SetVertexCount(3);
                render.SetPosition(0, pos1);
                render.SetPosition(1, p);
                render.SetPosition(2, pos2);
                break;
            case RoadDisplayMode.DownTuring:
                if (pos1.z < pos2.z)
                    p = new Vector3(pos2.x, 0, pos1.z);
                else
                    p = new Vector3(pos1.x, 0, pos2.z);
                render.SetVertexCount(3);
                render.SetPosition(0, pos1);
                render.SetPosition(1, p);
                render.SetPosition(2, pos2);
                break;
            case RoadDisplayMode.VerticalSurround:
                render.SetVertexCount(4);
                render.SetPosition(0, pos1);
                render.SetPosition(1, new Vector3(pos1.x, 0, MidDistance));
                render.SetPosition(2, new Vector3(pos2.x, 0, MidDistance));
                render.SetPosition(3, pos2);
                break;
            case RoadDisplayMode.HorizontalSurround:
                render.SetVertexCount(4);
                render.SetPosition(0, pos1);
                render.SetPosition(1, new Vector3(MidDistance, 0, pos1.z));
                render.SetPosition(2, new Vector3(MidDistance, 0, pos2.z));
                render.SetPosition(3, pos2);
                break;
            default:
                break;
        }
        render.Flush();
    }

    // Use this for initialization
    void Start()
    {
        render = gameObject.GetComponent<RoadRender>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Node1 != null)
        {
            if (Node2 == null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    RoadMode = RoadDisplayMode.Direct;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    RoadMode = RoadDisplayMode.UpTurning;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    RoadMode = RoadDisplayMode.DownTuring;
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                    RoadMode = RoadDisplayMode.VerticalSurround;
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                    RoadMode = RoadDisplayMode.HorizontalSurround;

                Vector3 pos = Global.GetMouseInGround3d();

                if (Input.GetKeyUp(KeyCode.Return))
                    if (RoadMode == RoadDisplayMode.VerticalSurround)
                        MidDistance = pos.z;
                    else if (RoadMode == RoadDisplayMode.HorizontalSurround)
                        MidDistance = pos.x;
                SetLine(Node1.transform.position, pos);
            }
            else
            {
                SetLine(Node1.transform.position, Node2.transform.position);
            }
        }
    }

    internal float GeAttacktCost(Army army, Node from)
    {
        return 1.0f;
    }

    public virtual float GetCost(Army army, Node from)
    {
        return 1.0f;
    }
    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        obj = Instantiate(gameObject, position, rotation) as GameObject;
        obj.GetComponent<Road>().id = seed++;
        return obj;
    }
}
