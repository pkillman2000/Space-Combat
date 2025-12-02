using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    /*
     * Main Menu = 0
     * Settings = 1
     * Game Play = 2
     * Start Game = 3
     * Leaderboard = 4
     * Controls = 5
     * How to Play = 6
    */
    [SerializeField]
    private GameObject[] _panels;

    void Start()
    {
        DisablePanels();
        _panels[0].SetActive(true); // Main Menu Panel
    }

    
    void Update()
    {
        
    }

    private void DisablePanels()
    {
        for(int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(false);
        }
    }

    public void DisplayPanel(int panelIndex)
    {
        DisablePanels();
        _panels[panelIndex].SetActive(true);
    }
}
