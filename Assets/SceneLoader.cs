using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() 
    {
        Debug.Log("Click");
        SceneManager.LoadScene("Level 1");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
}
