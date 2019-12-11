using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSetter : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        InputHelper.GetInput();
        if(InputHelper.menu)
        {
            SceneManager.LoadScene("MainMenuTest");
        }
    }
}
