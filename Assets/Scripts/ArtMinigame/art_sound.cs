using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class art_sound : MonoBehaviour, IPointerDownHandler
{
    //reference to ingamesounds
    [SerializeField]
    private GameObject main;
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
                main.GetComponent<InGameSounds>().art_game();
            }
        }
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
        
    }
}
