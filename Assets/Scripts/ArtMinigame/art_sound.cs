using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class art_sound : MonoBehaviour, IPointerEnterHandler
{
    //reference to ingamesounds
    [SerializeField]
    private InGameSounds main;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("yes");
                main.art_game();
            }
        }
        
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        

    }
}
