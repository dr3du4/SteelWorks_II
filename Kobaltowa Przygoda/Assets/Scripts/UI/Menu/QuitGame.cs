using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {
    public void EndGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
