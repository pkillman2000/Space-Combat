using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadSceneOnTimer : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [SerializeField]
    private string _sceneName;
    [SerializeField]
    private float _timeToWait = 5.0f;

    private float _timer = 0f;
    private void Start()
    {
        _sceneLoader = GetComponent<SceneLoader>();
        if (_sceneLoader == null)
        {
            Debug.LogError("Scene Loader is Null!");
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer > _timeToWait )
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        _sceneLoader.LoadScene(_sceneName);
    }
}
