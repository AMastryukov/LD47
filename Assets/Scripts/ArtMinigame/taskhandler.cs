using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class taskhandler : MonoBehaviour
{

    public Texture2D colorwheel;

    public Image color1;
    public Image color2;
    public Image color3;

    public Color getcolor1;
    public Color getcolor2;
    public Color getcolor3;

    int rand1x;
    int rand1y;
    int rand2x;
    int rand2y;
    int rand3x;
    int rand3y;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;

    GameObject selectedcolor;
    colorwheelscript selectedcolorscript;

    GameObject drawing;
    Drawing drawingscript;

    public float score;

    public GameObject brushparent;

    // Start is called before the first frame update
    void Start()
    {
        selectedcolor = GameObject.FindWithTag("colorselected");
        selectedcolorscript = selectedcolor.GetComponent<colorwheelscript>();

        drawing = GameObject.FindWithTag("drawing");
        drawingscript = drawing.GetComponent<Drawing>();

        selectColors();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider4.value >= 0.99 || (slider1.value >=0.32 && slider2.value >= 0.32 && slider3.value >= 0.32))
        {
            slider4.value = 0;
            restart();
        }
    }

    void restart()
    {
        score = (int)System.Math.Ceiling((slider1.value * 3.3) + (slider2.value*3.3) + (slider3.value*3.3));
        drawingscript.Save();
        selectColors();
        slider1.value = 0;
        slider2.value = 0;
        slider3.value = 0;
        slider4.value = 0;
        foreach (Transform child in brushparent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void selectColors()
    {
        while(true)
        {
            rand1x = Random.Range(0, 1000);
            rand1y = Random.Range(0, 1000);
            getcolor1 = colorwheel.GetPixel(rand1x, rand1y);
            //if color isnt transparent or white
            if (getcolor1.a == 1 && getcolor1.r < 0.95 && getcolor1.g < 0.95 && getcolor1.b < 0.95)
            {
                getcolor1.a = 1;
                color1.GetComponent<Image>().color = getcolor1;
                (slider1 as Slider).GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill").color = getcolor1;
                break;
            }
        }
        while (true)
        {
            rand2x = Random.Range(0, 1000);
            rand2y = Random.Range(0, 1000);
            getcolor2 = colorwheel.GetPixel(rand2x, rand2y);
            //if color isnt transparent or white, or similar to first color
            if (getcolor2.a == 1 && getcolor2.r < 0.95 && getcolor2.g < 0.95 && getcolor2.b < 0.95 && ((getcolor2.r > (getcolor1.r + 0.15)) || (getcolor2.r < (getcolor1.r - 0.15))) && ((getcolor2.g  > (getcolor1.g + 0.15)) || (getcolor2.g  < (getcolor1.g - 0.15))) && ((getcolor2.b > (getcolor1.b + 0.15)) || (getcolor2.b < (getcolor1.b - 0.15))))
            {
                getcolor2.a = 1;
                color2.GetComponent<Image>().color = getcolor2;
                (slider2 as Slider).GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill").color = getcolor2;
                break;
            }
        }
        while (true)
        {
            rand3x = Random.Range(0, 1000);
            rand3y = Random.Range(0, 1000);
            getcolor3 = colorwheel.GetPixel(rand3x, rand3y);
            //if color isnt transparent or white, or similar to second color
            if (getcolor3.a == 1 && getcolor3.r < 0.95 && getcolor3.g < 0.95 && getcolor3.b < 0.95 && ((getcolor3.r > (getcolor2.r + 0.15)) || (getcolor3.r < (getcolor2.r - 0.15))) && ((getcolor3.g > (getcolor2.g + 0.15)) || (getcolor3.g < (getcolor2.g - 0.15))) && ((getcolor3.b > (getcolor2.b + 0.15)) || (getcolor3.b < (getcolor2.b - 0.15))))
            {
                getcolor3.a = 1;
                color3.GetComponent<Image>().color = getcolor3;
                (slider3 as Slider).GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill").color = getcolor3;
                break;
            }
        }
    }

    public void fillSliders()
    {
        //if color currently selected is similar enough to 1st random color, etc
        if (((selectedcolorscript.selectedcolor.color.r > (getcolor1.r - 0.15)) && (selectedcolorscript.selectedcolor.color.r < (getcolor1.r + 0.15))) && ((selectedcolorscript.selectedcolor.color.g > (getcolor1.g - 0.15)) && (selectedcolorscript.selectedcolor.color.g < (getcolor1.g + 0.15))) && ((selectedcolorscript.selectedcolor.color.b > (getcolor1.b - 0.15)) && (selectedcolorscript.selectedcolor.color.b < (getcolor1.b + 0.15))))
        {
            slider1.value += 0.001f;
        }
        if (((selectedcolorscript.selectedcolor.color.r > (getcolor2.r - 0.15)) && (selectedcolorscript.selectedcolor.color.r < (getcolor2.r + 0.15))) && ((selectedcolorscript.selectedcolor.color.g > (getcolor2.g - 0.15)) && (selectedcolorscript.selectedcolor.color.g < (getcolor2.g + 0.15))) && ((selectedcolorscript.selectedcolor.color.b > (getcolor2.b - 0.15)) && (selectedcolorscript.selectedcolor.color.b < (getcolor2.b + 0.15))))
        {
            slider2.value += 0.001f;
        }
        if (((selectedcolorscript.selectedcolor.color.r > (getcolor3.r - 0.15)) && (selectedcolorscript.selectedcolor.color.r < (getcolor3.r + 0.15))) && ((selectedcolorscript.selectedcolor.color.g > (getcolor3.g - 0.15)) && (selectedcolorscript.selectedcolor.color.g < (getcolor3.g + 0.15))) && ((selectedcolorscript.selectedcolor.color.b > (getcolor3.b - 0.15)) && (selectedcolorscript.selectedcolor.color.b < (getcolor3.b + 0.15))))
        {
            slider3.value += 0.001f;
        }
        slider4.value += 0.001f;
    }
}