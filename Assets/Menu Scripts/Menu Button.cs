using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButton : MonoBehaviour
{

    public void ChangeScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void ConfirmExit(GameObject Menu)
    {
        Menu.SetActive(true);
    }

    public void Closemenu(GameObject Menu)
    {
        Menu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
