using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //управление меню либо начать игру либо выход
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClickStart();
        }
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
