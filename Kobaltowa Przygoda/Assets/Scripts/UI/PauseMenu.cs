using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseButton;
    // Start is called before the first frame update
    public void Pause(){
      
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            //Debug.Log("Sie zapauzowali ");
            PauseButton.SetActive(false);

    }

    public void Continue(){

        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Home(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
