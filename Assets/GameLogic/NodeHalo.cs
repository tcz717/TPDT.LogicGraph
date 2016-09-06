using UnityEngine;

public class NodeHalo : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1.3f, 1.3f);
    }

    public void SetSelected()
    {
        SetColor(Color.white);
    }
    public void SetAccessible()
    {
        SetColor(Color.green);
    }
    public void SetAttackable()
    {
        SetColor(Color.red);
    }
}