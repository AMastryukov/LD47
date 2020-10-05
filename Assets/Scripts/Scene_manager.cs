using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Scene_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public void Start_game()
    {
        SceneManager.LoadScene(2);
    }

}
