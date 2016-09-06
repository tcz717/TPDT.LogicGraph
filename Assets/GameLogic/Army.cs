using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeathEventArgs : EventArgs
{
    public List<Army> Attacker { get; set; }
    public Army Decedent { get; set; }
    public DeathEventArgs(Army decedent)
    {
        Attacker = new List<Army>();
        Decedent = decedent;
    }
    public DeathEventArgs(Army decedent, List<Army> attacker)
    {
        Attacker = attacker;
        Decedent = decedent;
    }
}

public delegate void DeathEvent(object sender, DeathEventArgs args);

public class Army : MonoBehaviour, ITemplatable
{
    static int seed = 0;
    public int id;
    public Node Node { get; set; }
    public Player Owner { get; set; }
    public float velocity;
    public float attackRange;
    public float attackPoint;
    public float defencePoint;
    protected List<GameObject> ColorPart;

    public bool draged;
    public bool Pined { get; private set; }

    public bool IsDeath { get; private set; }

    public event DeathEvent OnDeath;

    // Use this for initialization
    protected virtual void Start () 
    {
        ColorPart = new List<GameObject>();
        draged = false;
        Pined = false;

        IsDeath = false;
    }

    // Update is called once per frame
    protected virtual void Update ()
    {
        if (IsDeath)
        {
            Destroy(gameObject);
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            transform.position = Node.transform.position;
            draged = false;
        }
        //if (Input.GetMouseButton(0) && Node.selected)
        //{
        //    transform.position = Global.GetMouseInGround3d() + Vector3.up * 0.5f;
        //}
    }
    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public void OnMouseDrag()
    {
        if(Node.selected)
        {
            if (!Pined)
                transform.position = Global.GetMouseInGround3d() + Vector3.up * 0.8f;
            draged = true;
            Debug.Log("Draged");
        }
    }

    void OnMouseEnter()
    {
        Node.OnMouseEnter();
    }
    void OnMouseExit()
    {
        Node.OnMouseExit();
    }

    public GameObject Clone(Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        obj = Instantiate(gameObject, position, rotation) as GameObject;
        obj.GetComponent<Army>().id = seed++;
        return obj;
    }

    public void MoveTo(Node nnode)
    {
        Debug.Log("MoveArmy", this);
        Node.Army = null;
        Node = nnode;
        Node.Army = this;

        transform.position = Node.transform.position;
    }

    public void Pin(Vector3 pos)
    {
        Pined = true;
        transform.position = pos + Vector3.up * 0.8f;
    }
    public void Unpin()
    {
        Pined = false;
        //if (!draged)
        //    transform.position = Node.transform.position;
    }

    public virtual int TryAttack(Army defender)
    {
        return 1;
    }

    public virtual void Attack(Army defender)
    {
        
    }

    public virtual void DieForAttack(List<Army> attackers)
    {
        Die();

        if (OnDeath != null)
            OnDeath(this, new DeathEventArgs(this, attackers));
    }

    public void Die()
    {
        Node.Army = null;
        Owner.Armys.Remove(this);
        MainGame.Current.Armys.Remove(id);

        Node = null;
        Owner = null;

        IsDeath = true;
    }
}
