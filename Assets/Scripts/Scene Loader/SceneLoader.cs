using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private Fader _fader;

    public void LoadScene(string sceneName)
    {
        _fader.FadeOut();
        SceneManager.LoadScene(sceneName);
    }
}
