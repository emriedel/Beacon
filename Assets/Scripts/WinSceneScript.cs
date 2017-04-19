using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinSceneScript : MonoBehaviour
{

    void Start()
    {
        Invoke("startGame", 10f);
    }

    void startGame()
    {
        SceneManager.LoadScene("__StartScene");
    }

}
