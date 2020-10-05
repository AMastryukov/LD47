using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class painthandler : MonoBehaviour
{

    GameObject drawingplane;
    Drawing drawingplanescript;

    GameObject colorselected;
    colorwheelscript colorselectedscript;

    GameObject taskmenu;
    taskhandler taskmenuscript;

    // Start is called before the first frame update
    void Start()
    {
        drawingplane = GameObject.FindWithTag("drawing");
        drawingplanescript = drawingplane.GetComponent<Drawing>();

        colorselected = GameObject.FindWithTag("colorselected");
        colorselectedscript = colorselected.GetComponent<colorwheelscript>();

        taskmenu = GameObject.FindWithTag("taskmenu");
        taskmenuscript = taskmenu.GetComponent<taskhandler>();

        GetComponent<Renderer>().material.color = colorwheelscript.getcolor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        if (drawingplanescript.erase)
        {
            if (Input.GetMouseButton(0))
            {
                if (((GetComponent<Renderer>().material.color.r > (taskmenuscript.getcolor1.r - 0.15)) && (GetComponent<Renderer>().material.color.r < (taskmenuscript.getcolor1.r + 0.15))) && ((GetComponent<Renderer>().material.color.g > (taskmenuscript.getcolor1.g - 0.15)) && (GetComponent<Renderer>().material.color.g < (taskmenuscript.getcolor1.g + 0.15))) && ((GetComponent<Renderer>().material.color.b > (taskmenuscript.getcolor1.b - 0.15)) && (GetComponent<Renderer>().material.color.b < (taskmenuscript.getcolor1.b + 0.15))))
                {
                    taskmenuscript.slider1.value -= 0.001f;
                }
                if (((GetComponent<Renderer>().material.color.r > (taskmenuscript.getcolor2.r - 0.15)) && (GetComponent<Renderer>().material.color.r < (taskmenuscript.getcolor2.r + 0.15))) && ((GetComponent<Renderer>().material.color.g > (taskmenuscript.getcolor2.g - 0.15)) && (GetComponent<Renderer>().material.color.g < (taskmenuscript.getcolor2.g + 0.15))) && ((GetComponent<Renderer>().material.color.b > (taskmenuscript.getcolor2.b - 0.15)) && (GetComponent<Renderer>().material.color.b < (taskmenuscript.getcolor2.b + 0.15))))
                {
                    taskmenuscript.slider2.value -= 0.001f;
                }
                if (((GetComponent<Renderer>().material.color.r > (taskmenuscript.getcolor3.r - 0.15)) && (GetComponent<Renderer>().material.color.r < (taskmenuscript.getcolor3.r + 0.15))) && ((GetComponent<Renderer>().material.color.g > (taskmenuscript.getcolor3.g - 0.15)) && (GetComponent<Renderer>().material.color.g < (taskmenuscript.getcolor3.g + 0.15))) && ((GetComponent<Renderer>().material.color.b > (taskmenuscript.getcolor3.b - 0.15)) && (GetComponent<Renderer>().material.color.b < (taskmenuscript.getcolor3.b + 0.15))))
                {
                    taskmenuscript.slider3.value -= 0.001f;
                }
                taskmenuscript.slider4.value -= 0.001f;
                Destroy(gameObject);
            }
        }
    }
}
