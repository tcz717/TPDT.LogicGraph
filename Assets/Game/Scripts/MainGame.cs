using System;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public bool enableJudge;
    private int pindex = 0;

    public static event EventHandler InitFinished;

    public event EventHandler PlayerChanged;

    public static MainGame Current { get; set; }

    public Dictionary<int, Army> Armys { get; private set; }
    public Player CurrentPlayer;
    public Dictionary<int, Node> Nodes { get; private set; }
    public Dictionary<int, Player> Players { get; private set; }
    public Dictionary<int, Road> Roads { get; private set; }

    public Graph Graph { get; private set; }

    public void AddArmy(Army army, Node node)
    {
        Assert.Test(army != null);
        Assert.Test(node != null);
        Assert.Test(CurrentPlayer != null);

        Armys.Add(army.id, army);
        army.Node = node;
        node.Army = army;
        army.Owner = CurrentPlayer;
        CurrentPlayer.Armys.Add(army);

        Debug.Log(Armys);
    }

    public void AddNode(Node node)
    {
        Nodes.Add(node.id, node);
    }

    public void AddPlayer(Player player)
    {
        Assert.Test(player != null);

        Players.Add(player.id, player);
        if (CurrentPlayer == null)
            ChangePlayer(player);
    }

    public void AddRoad(Road road)
    {
        Roads.Add(road.id, road);
        road.Node1.Roads.Add(road);
        road.Node2.Roads.Add(road);
    }

    public void ChangePlayer(Player player)
    {
        Assert.Test(player != null);

        if (CurrentPlayer != null)
            CurrentPlayer.EndTurn();

        CurrentPlayer = player;

        CurrentPlayer.StartTurn();

        if (PlayerChanged != null)
            PlayerChanged(this, new EventArgs());
    }

    public Road GetRoad(Node n1, Node n2)
    {
        foreach (var r in n1.Roads)
        {
            if (r.Node1 == n2 || r.Node2 == n2)
                return r;
        }
        return null;
    }

    public void NextPlayer()
    {
        if (Players.Count != 0)
        {
            pindex = (pindex + 1) % Players.Count;
            ChangePlayer(Players[pindex]);
        }
    }

    public bool TryMoveArmy(Army army,Node nnode)
    {
        if (army.Owner.leftMoveTimes <= 0)
            return false;

        var nodes = Graph.GetAccessibleNodes(army);
        if (!nodes.ContainsKey(nnode))
            return false;

        MoveArmy(army, nnode);

        return true;
    }

    public void MoveArmy(Army army, Node nnode)
    {
        army.MoveTo(nnode);

        if (enableJudge)
        {
            army.Owner.leftMoveTimes--;
            JudgeIndependentAttck(CurrentPlayer);
        }
    }

    public void JudgeIndependentAttck(Player player)
    {
        Dictionary<Army, List<Army>> result = FindAttck(player);

        foreach (var atk in result)
        {
            JudgeAttckOfArmy(atk.Key, atk.Value);
        }
    }

    private Dictionary<Army, List<Army>> FindAttck(Player player)
    {
        Dictionary<Army, List<Army>> result = new Dictionary<Army, List<Army>>();
        foreach (var army in player.Armys)
        {
            var atknode = Graph.GetAttackableNodes(army);
            foreach (var n in atknode)
            {
                if (!result.ContainsKey(n.Army))
                    result.Add(n.Army, new List<Army>());
                result[n.Army].Add(army);
            }
        }
        return result;
    }

    private void JudgeAttckOfArmy(Army defender, List<Army> attackers)
    {
        int total = 0;
        Debug.Log("JudgeAttckOfArmy", defender);
        foreach (var atker in attackers)
        {
            total += atker.TryAttack(defender);
        }

        if (total >= defender.defencePoint)
        {
            Debug.Log("Attack Ouccer");

            foreach (var atker in attackers)
            {
                atker.Attack(defender);
            }

            defender.DieForAttack(attackers);
        }
    }

    public void RemoveArmy(Army army)
    {
        Assert.Test(army != null);

        army.Node.Army = null;
        army.Owner.Armys.Remove(army);
        Armys.Remove(army.id);
        Destroy(army.gameObject);
    }

    public void RemoveNode(Node node)
    {
        Assert.Test(node != null);

        Nodes.Remove(node.id);
        foreach (var r in node.Roads)
        {
            if (r.Node2 == node)
                r.Node1.Roads.Remove(r);
            else
                r.Node2.Roads.Remove(r);
            Roads.Remove(r.id);

            r.Node1 = null;
            r.Node2 = null;

            Destroy(r.gameObject);
        }

        if (node.Army != null)
            RemoveArmy(node.Army);
        node.Roads.Clear();
        Destroy(node.gameObject);
    }

    public void RemoveRoad(Road road)
    {
        road.Node1.Roads.Remove(road);
        road.Node2.Roads.Remove(road);
        road.Node1 = null;
        road.Node2 = null;
        Roads.Remove(road.id);
        Destroy(road.gameObject);
    }

    public void ScreenClick()
    {
        Debug.Log(Input.mousePosition);
    }

    // Use this for initialization
    private void Start()
    {
        Roads = new Dictionary<int, Road>();
        Nodes = new Dictionary<int, Node>();
        Armys = new Dictionary<int, Army>();
        Players = new Dictionary<int, Player>();

        Current = this;
        if (InitFinished != null)
            InitFinished(this, new EventArgs());

        ChangePlayer(Players[pindex]);

        Graph = new Graph(Nodes, Roads);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}