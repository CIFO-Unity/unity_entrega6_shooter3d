using UnityEngine;


public class BackToMainMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
