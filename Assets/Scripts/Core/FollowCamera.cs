using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, this.transform.position.y, _player.transform.position.z);
    }
}
