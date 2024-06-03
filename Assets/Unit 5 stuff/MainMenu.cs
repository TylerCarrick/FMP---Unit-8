using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  [SerializeField] GameObject gameObject;
   public void PlayGame()
    {
        SceneManager.LoadScene(1);
       DontDestroyOnLoad(gameObject);  
        gameObject.SetActive(false);
    }
    
    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }


}
