using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMissilesUI : MonoBehaviour
{
    private GridLayoutGroup _gridLayoutGroup;
    [SerializeField]
    private Image[] _missileIcons;


    void Start()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        if(_gridLayoutGroup == null)
        {
            Debug.LogError("Grid Layout Group is Null!");
            return;
        }
        else
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = 3;
        }
    }


    void Update()
    {
        
    }
    
    public void SetMissiles(int currentMissiles)
    {
        DisplayMissiles(currentMissiles);
    }

    private void DisplayMissiles(int currentMissiles)
    {
        // Turn off all missile sprites
        for(int i = 0; i < _missileIcons.Length; i++)
        {
            _missileIcons[i].enabled = false;
        }

        // Turn on active missile sprites
        for (int i = 0; i < currentMissiles; i++)
        {
            _missileIcons[i].enabled = true;
        }
    }

}
