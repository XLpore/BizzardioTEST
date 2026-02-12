using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    [SerializeField]
    GameObject _pauseMenu;

    private void Update()
    {
        if ( Input.GetKeyUp(KeyCode.Escape) )
        {
            Pause();
        }
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }

    public void UnPause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

}
