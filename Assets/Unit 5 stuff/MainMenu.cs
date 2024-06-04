using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  [SerializeField] GameObject component;
   public void PlayGame()
    {
       SceneManager.LoadScene(1);
       DontDestroyOnLoad(component);  
       gameObject.SetActive(false);
    }
    
    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }


}
