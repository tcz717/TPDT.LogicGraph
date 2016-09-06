/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 18:46
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.IO;
using UnityEngine;

	/// <summary>
	/// Description of Global.
	/// </summary>
public static class Global
{
    public static int Version = 1;

    public static Vector3 GetMouseIn2d()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    public static Vector3 GetMouseInGround3d()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground")))
        {
            Vector3 point = new Vector3(hit.point.x, 0, hit.point.z);
            Debug.Log(point);
            return point;
        }
        else return Vector3.zero;
    }

    public static Node GetNode(this GameObject obj)
    {
        return obj.GetComponent<Node>();
    }
    public static Army GetArmy(this GameObject obj)
    {
        return obj.GetComponent<Army>();
    }
}
