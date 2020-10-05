using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchingcolorwheel : MonoBehaviour
{

    public bool touching;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onMouseOver()
    {
        touching = true;
    }

    void OnMouseExit()
    {
        touching = false;
    }
}
