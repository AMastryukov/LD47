using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorwheelscript : MonoBehaviour
{
    public Button exit;
    public Button colorwheel;
    public Image selectedcolor;
    public GameObject colorwheelfocusedcanvas;
    public GameObject colorwheelfocused;
    touchingcolorwheel touchingcolorwheelscript;
    public Texture2D colorwheeltexture;
    Vector2 mousePos = new Vector2();
    RectTransform rect;
    Texture2D t2D;
    int width = 0;
    int height = 0;
    public static Color getcolor;

    public bool touchingcolorwheelfocused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //colorwheelfocused = GameObject.FindWithTag("colorwheelfocused");
        //touchingcolorwheelscript = colorwheelfocused.GetComponent<touchingcolorwheel>();

        t2D = colorwheelfocused.GetComponent<RawImage>().texture as Texture2D;
        
        width = (int)colorwheelfocused.GetComponent<RectTransform>().rect.width;
        height = (int)colorwheelfocused.GetComponent<RectTransform>().rect.height;

        colorwheelfocusedcanvas.SetActive(false);

        colorwheel.onClick.AddListener(delegate{colorwheelfocus("true");});
        exit.onClick.AddListener(delegate{colorwheelfocus("false");});
    }

    // Update is called once per frame
    void Update()
    {
        if (colorwheelfocusedcanvas.activeSelf)
        {
            if (touchingcolorwheelfocused)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(colorwheelfocused.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out mousePos);

                mousePos.x = width - (width / 2 - mousePos.x);
                mousePos.y = Mathf.Abs((height / 2 - mousePos.y) - height);

                //Debug.Log("Mouse: " + mousePos.x + "," + mousePos.y);

                if (Input.GetMouseButton(0))
                {
                    getcolor = colorwheeltexture.GetPixel((int)mousePos.x, (int)mousePos.y);
                    if (getcolor.a == 1)
                    {
                        selectedcolor.color = colorwheeltexture.GetPixel((int)mousePos.x, (int)mousePos.y);
                        //Debug.Log(selectedcolor.color);
                    }
                    else
                    {
                        selectedcolor.color = Color.white;
                    }
                }
            }
        }
    }

    void colorwheelfocus(string active)
    {
        if (active == "true")
        {
            colorwheelfocusedcanvas.SetActive(true);
        }
        if (active == "false")
        {
            colorwheelfocusedcanvas.SetActive(false);
        }
    }

    public void mouseentercolorwheelfocused()
    {
        touchingcolorwheelfocused = true;
    }

    public void mouseexitcolorwheelfocused()
    {
        touchingcolorwheelfocused = false;
    }
}