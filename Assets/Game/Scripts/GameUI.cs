using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject changeJudge;
    public GameObject endTurn;

    public GameObject infantry;
    public GameObject archer;
    public GameObject normal;
    public GameObject player;
    public GameObject playerText;
    public GameObject rode;
    
    public GameObject selected;

    public UIState state;

    GameObject temp;

    bool ui_active;

    public enum UIState
    {
        None,
        AddNode,
        AddRode,
        RemoveNode,
        RemoveRoad,
        AddInfantry,
        AddArcher,
    }

    public static GameUI MainUI { get; set; }
    public void AddInfantry()
    {
        RequireNodeWithoutArmy((node) =>
        {
            AddArmy(infantry, node);
            return true;
        });
        Debug.Log("Clicked AddInfantry");
    }
    public void AddArcher()
    {
        RequireNodeWithoutArmy((node) =>
        {
            AddArmy(archer, node);
            return true;
        });
        Debug.Log("Clicked AddInfantry");
    }

    public void AddNode()
    {
        state = UIState.AddNode;
        ui_active = true;
        Debug.Log("Clicked AddNode");
    }

    public void AddRoad()
    {
        state = UIState.AddRode;
        ui_active = true;
        temp = null;
        Debug.Log("Clicked AddRoad");
    }

    public void ChangeJudge()
    {
        MainGame.Current.enableJudge = !MainGame.Current.enableJudge;
        if (selected != null)
            OnSelect(selected);
        Debug.Log("Clicked Change Judge");
    }

    public void ChangePlayer()
    {
        MainGame.Current.NextPlayer();
        Debug.Log("Clicked Change Player");
    }

    public void DrawAccessibleNodes(Army army)
    {
        var nodes = MainGame.Current.Graph.GetAccessibleNodes(army);

        foreach (var n in nodes.Keys)
        {
            if (n == army.Node)
                continue;
            Debug.Log("DrawAccessibleNodes", n);
            n.SetAccessible();
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    private Func<Node, bool> requireNodeCallback;
    private Func<Node, bool> checkNodeCallback;
    public void RequireNode(Func<Node, bool> callback)
    {
        requireNodeCallback = callback;
        checkNodeCallback = (node) => true;
    }
    public void RequireNodeWithoutArmy(Func<Node, bool> callback)
    {
        requireNodeCallback = callback;
        checkNodeCallback = (node) => node.Army == null;
    }

    public void Leave()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadTestMap()
    {
        GameObject[,] map = new GameObject[5, 5];

        Debug.Log("LoadTestMap");

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                state = UIState.AddNode;
                map[i, j] = DoAddNode(new Vector3(i * 2.0f - 4.0f, 0, j * 2.0f - 3.0f));
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                state = UIState.AddRode;
                temp = null;
                DoAddRoad(map[i, j]);
                DoAddRoad(map[i, j + 1]);

                state = UIState.AddRode;
                temp = null;
                DoAddRoad(map[j, i]);
                DoAddRoad(map[j + 1, i]);
            }
        }
        
        AddArmy(infantry, map[2, 2]);
    }

    public void AddArmy(GameObject template, Node position)
    {
        GameObject army = template.Clone<Army>(position.gameObject.transform.position, template.transform.rotation);
        MainGame.Current.AddArmy(army.GetComponent<Army>(), position);
    }
    public void AddArmy(GameObject template, GameObject position)
    {
        AddArmy(template, position.GetComponent<Node>());
    }

    public void OnSelect(GameObject obj)
    {
        if (obj.GetComponent<Node>() != null)
        {
            if (selected != null && selected.GetComponent<Node>().Army != null)
            {
                var army = selected.GetComponent<Node>().Army;
                if (MainGame.Current.enableJudge && army.draged)
                    MainGame.Current.TryMoveArmy(army, obj.GetComponent<Node>());
                else if(army.draged)
                    MainGame.Current.MoveArmy(army, obj.GetComponent<Node>());
            }

            SelectNode(obj);
        }
        else if (obj.GetArmy() != null)
        {
            SelectNode(obj.GetComponent<Army>().Node.gameObject);
        }
    }

    public void RemoveNode()
    {
        state = UIState.RemoveNode;
        ui_active = true;
        Debug.Log("Clicked RemoveNode");
    }

    public void RemoveRoad()
    {
        state = UIState.RemoveRoad;
        ui_active = true;
        temp = null;
        Debug.Log("Clicked RemoveRoad");
    }

    public void ScreenClick()
    {
        Debug.Log(Input.mousePosition);
    }

    private static void DoRemoveNode(GameObject obj)
    {
        MainGame.Current.RemoveNode(obj.GetComponent<Node>());
    }

    void Current_PlayerChanged(object sender, System.EventArgs e)
    {
        if (MainGame.Current.CurrentPlayer != null)
        {
            Debug.Log("change", MainGame.Current.CurrentPlayer);
            var ptext = playerText.GetComponent<UnityEngine.UI.Text>();
            ptext.color = MainGame.Current.CurrentPlayer.chiefColor;
            ptext.text = MainGame.Current.CurrentPlayer.name;

            if (selected != null)
                OnSelect(selected);
        }
    }

    private GameObject DoAddNode(Vector3 pos)
    {
        if (state == UIState.AddNode)
        {
            GameObject node_obj = normal.Clone<Node>(pos);
            Node node = node_obj.GetComponent<Node>();
            MainGame.Current.AddNode(node);
            state = UIState.None;

            node.OnHoverEnter += Node_OnHoverEnter;
            node.OnHoverExit += Node_OnHoverExit;

            return node_obj;
        }
        return null;
    }

    private void DoAddRoad(GameObject obj)
    {
        if (temp == null)
        {
            temp = rode.Clone<Road>();
            temp.GetComponent<Road>().Node1 = obj.GetComponent<Node>();
            Debug.Log("Added Road Node1", obj);
        }
        else
        {
            temp.GetComponent<Road>().Node2 = obj.GetComponent<Node>();
            state = UIState.None;
            MainGame.Current.AddRoad(temp.GetComponent<Road>());
            temp = null;
            Debug.Log("Added Road Node2", obj);
        }
    }

    private void DoRemoveRoad(GameObject obj)
    {
        if (temp == null)
        {
            temp = obj;
            Debug.Log("Select One Node");
        }
        else
        {
            Road r = MainGame.Current.GetRoad(temp.GetComponent<Node>(), obj.GetComponent<Node>());
            if (r != null)
            {
                MainGame.Current.RemoveRoad(r);
                temp = null;
            }
            else
                Debug.Log("Road Not Find");
        }
    }

    private void DrawAttackableNodes(Army army)
    {
        var nodes = MainGame.Current.Graph.GetAttackableNodes(army);

        foreach (var n in nodes)
        {
            Debug.Log("DrawAttackableNodes", n);
            n.SetAttackable();
        }
    }

    void MainGame_InitFinished(object sender, System.EventArgs e)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        MainGame.Current.PlayerChanged += Current_PlayerChanged;
        foreach (var p in players)
        {
            Debug.Log(p);
            MainGame.Current.AddPlayer(p.GetComponent<Player>());
        }
    }

    private void Node_OnHoverEnter(Node sender, EventArgs e)
    {
        if (selected == null)
            return;

        Node s = selected.GetNode();

        if (s.Army == null)
            return;

        if (s.Army.draged)
        {
            selected.GetNode().Army.Pin(sender.transform.position);
            Debug.Log("OnHoverEnter");
        }
    }

    private void Node_OnHoverExit(Node sender, EventArgs e)
    {
        if (selected == null)
            return;

        Node s = selected.GetNode();

        if (s.Army == null)
            return;

        if (s.Army.draged)
            selected.GetNode().Army.Unpin();
    }

    private void SelectArmy(Army army)
    {
        if (MainGame.Current.enableJudge)
        {
            if (army.Owner.leftMoveTimes > 0)
                DrawAccessibleNodes(army);
            DrawAttackableNodes(army);
        }
    }

    private void SelectNode(GameObject obj)
    {
        Node node = obj.GetComponent<Node>();
        foreach (var n in MainGame.Current.Nodes.Values)
        {
            n.HideHalo();
            n.selected = false;
        }
        selected = obj;
        node.Select();

        if (node.Army != null)
            SelectArmy(node.Army);

        if (requireNodeCallback != null && checkNodeCallback(node))
        {
            bool result = requireNodeCallback(node);
            if (result)
                requireNodeCallback = null;
        }
        if (!ui_active)
        {
            switch (state)
            {
                case UIState.RemoveNode:
                    DoRemoveNode(obj);
                    break;
                case UIState.AddRode:
                    DoAddRoad(obj);
                    break;
                case UIState.RemoveRoad:
                    DoRemoveRoad(obj);
                    break;
                default:
                    break;
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        state = UIState.None;
        ui_active = false;
        temp = null;

        MainUI = this;

        if (changeJudge == null)
            changeJudge = GameObject.Find("ChangeJudge");
        if (endTurn == null)
            endTurn = GameObject.Find("EndTurn");

        MainGame.InitFinished += MainGame_InitFinished;
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && !ui_active)
        {
            DoAddNode(Global.GetMouseInGround3d());
        }
        if (Input.GetMouseButtonDown(1))
        {
            switch (state)
            {
                case UIState.AddNode:
                    break;
                case UIState.AddRode:
                    Destroy(temp);
                    temp = null;
                    break;
                case UIState.RemoveNode:
                    break;
                default:
                    break;
            }
            state = UIState.None;
        }
        ui_active = false;

        changeJudge.GetComponentInChildren<UnityEngine.UI.Text>().text = MainGame.Current.enableJudge ? "暂停游戏" : "运行游戏";
        endTurn.GetComponentInChildren<UnityEngine.UI.Text>().text = MainGame.Current.enableJudge ? "结束回合" : "切换玩家";
    }
}
