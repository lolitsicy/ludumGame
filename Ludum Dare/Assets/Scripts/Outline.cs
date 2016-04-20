using UnityEngine;
using System.Collections;

public class Outline : MonoBehaviour
{
    public float offsetX;
    private GameObject above;
    private GameObject below;
    private GameObject right;
    private GameObject left;

    void SetText(GameObject obj)
    {
        obj.GetComponent<TextMesh>().text = GetComponent<TextMesh>().text;
    }

    string GetText(GameObject obj)
    {
        return obj.GetComponent<TextMesh>().text;
    }

    void Start()
    {
        if (offsetX != 0)
        {
            // this moves the child objects as well
            transform.Translate(new Vector3(offsetX, 0, 0));
        }

        above = transform.Find("Above").gameObject;
        below = transform.Find("Below").gameObject;
        right = transform.Find("Right").gameObject;
        left = transform.Find("Left").gameObject;

        SetText(above);
        SetText(below);
        SetText(right);
        SetText(left);
    }

    void Update()
    {
        if (!GetText(above).Equals(GetComponent<TextMesh>().text))
        {
            SetText(above);
            SetText(below);
            SetText(right);
            SetText(left);
        }
    }
}
