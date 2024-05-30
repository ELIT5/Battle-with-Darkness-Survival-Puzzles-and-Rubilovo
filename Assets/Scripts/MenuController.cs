using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas doYouWant;
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        mainMenu.enabled = false;
        doYouWant.enabled = true;
    }
    public void No()
    {
        mainMenu.enabled = true;
        doYouWant.enabled = false;
    }
}
