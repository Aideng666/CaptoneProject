using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Changes to any scene
    public void ChangeToNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
