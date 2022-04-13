using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAnim : MonoBehaviour
{
    private RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        rt.Rotate(new Vector3(0, .2f, 0));
    }
}
