using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public GameObject colorwheelfocusedcanvas;
    public GameObject paintbrush;
    public Texture2D texpaintbrush;
    public Texture2D texeraser;
    public float paintSize = 3;
    public bool erase = false;
    public Button eraser;
    public Button brush;
    public Button stroke1;
    public Button stroke2;
    public Button stroke3;
    public Button stroke4;
    public Button stroke5;
    public Button stroke6;
    public Button save;

    public GameObject brushparent;

    public int imagenumber = 1;

    public RenderTexture RTexture;

    GameObject taskmenu;
    taskhandler taskmenuscript;

    GameObject colorselected;
    colorwheelscript colorselectedscript;

    // Start is called before the first frame update
    void Start()
    {

        taskmenu = GameObject.FindWithTag("taskmenu");
        taskmenuscript = taskmenu.GetComponent<taskhandler>();

        colorselected = GameObject.FindWithTag("colorselected");
        colorselectedscript = colorselected.GetComponent<colorwheelscript>();

        eraser.onClick.AddListener(delegate{changeTool("eraser");});
        brush.onClick.AddListener(delegate{changeTool("brush");});
        stroke1.onClick.AddListener(delegate { changebrushsize(1); });
        stroke2.onClick.AddListener(delegate { changebrushsize(2); });
        stroke3.onClick.AddListener(delegate { changebrushsize(3); });
        stroke4.onClick.AddListener(delegate { changebrushsize(4); });
        stroke5.onClick.AddListener(delegate { changebrushsize(5); });
        stroke6.onClick.AddListener(delegate { changebrushsize(6); });
        save.onClick.AddListener(Save);
    }

    // Update is called once per frame
    void Update()
    {
        var v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        //DRAW if colorwheel not focused, holding mouse button, not erasing, color selected isnt white, mouse is moving and mouse is over the draw plane
        if (colorwheelfocusedcanvas.activeSelf == false)
        {
            if (erase == false)
            {
                if (colorselectedscript.selectedcolor.color != Color.white)
                {
                    if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
                    {
                        if (Input.GetMouseButton(0))
                        {
                            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;

                            if (Physics.Raycast(Ray, out hit))
                            {
                                if (((v3.x > -6.2) && (v3.x < -0.6)) && ((v3.y < 2.1) && (v3.y > -3.5)))
                                {
                                    taskmenuscript.fillSliders();
                                    var go = Instantiate(paintbrush, hit.point + Vector3.up * 0.1f, transform.rotation,brushparent.transform);
                                    go.transform.localScale = Vector3.one * paintSize;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void changeTool(string tool)
    {
        if (tool == "eraser")
        {
            erase = true;
            //Cursor.SetCursor(texeraser, Vector2.zero, CursorMode.Auto);
        }
        if (tool == "brush")
        {
            erase = false;
            //Cursor.SetCursor(texpaintbrush, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    void changebrushsize(int size)
    {
        paintSize = size;
    }


    //--------------------SAVE PNG---------------------------

    public void Save()
    {
        StartCoroutine(CoSave());
    }

    private IEnumerator CoSave()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture.active = RTexture;

        var texture2D = new Texture2D(RTexture.width, RTexture.height);

        texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
        texture2D.Apply();

        var data = texture2D.EncodeToPNG();

        File.WriteAllBytes(Application.dataPath + "/savedImage" + imagenumber + ".png", data);
        imagenumber++;
    }
}
