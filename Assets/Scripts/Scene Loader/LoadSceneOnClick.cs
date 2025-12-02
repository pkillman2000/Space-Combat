using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private GamePlay _playerInputActions;
    private SceneLoader _sceneLoader;

    [SerializeField]
    private string _sceneName;
    private void Start()
    {
        _playerInputActions = new GamePlay();

        if (_playerInputActions == null)
        {
            Debug.LogError("Player Input Actions is Null!");
        }
        else
        {
            _playerInputActions.UI.Enable();
        }

        _playerInputActions.UI.Cancel.performed += Select_Performed;

        _sceneLoader = GetComponent<SceneLoader>();
        if (_sceneLoader == null)
        {
            Debug.LogError("Scene Loader is Null!");
        }
    }

    private void Select_Performed(InputAction.CallbackContext obj)
    {
        _sceneLoader.LoadScene(_sceneName);
    }
}
