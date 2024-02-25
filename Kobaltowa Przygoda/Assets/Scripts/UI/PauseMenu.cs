using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject BackButton;


    // Start is called before the first frame update
    public void Pause(){
      
            PausePanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Sie zapauzowali ");
            PauseButton.SetActive(false);
            ControlsPanel.SetActive(false);
            BackButton.SetActive(false);
    }

    public void Continue(){

        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void Controlsy(){
        //Debug.Log("Sie zapauzowali ");
        ControlsPanel.SetActive(true);
        BackButton.SetActive(true);
        PausePanel.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void Home(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
