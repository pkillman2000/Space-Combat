using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
     * 0 = Easy
     * 1 = Medium
     * 2 = Hard
    */
    private int _gameDifficulty = 1;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetDifficulty(int gameDifficulty)
    {
        _gameDifficulty = gameDifficulty;
    }

    public int GetDifficulty()
    {
        return _gameDifficulty;
    }
}
