using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Primary Weapon Settings")]
    private GameObject[] _primaryWeaponPrefab;
    [SerializeField]
    private Transform[] _primaryWeaponSpawnPoints;
    [SerializeField]
    private bool[] _primaryWeaponActive;
    [SerializeField]
    private float _primaryWeaponFireRate = 0.5f;
    private float _nextPrimaryFireTime = 0f;
    private int _primaryWeaponIndex = 0;
    private GameObject _primaryWeaponsParentObject;


    [Header("Secondary Weapon Settings")]
    private GameObject[] _secondaryWeaponPrefab;
    [SerializeField]
    private Transform[] _secondaryWeaponSpawnPoints;
    [SerializeField]
    private bool[] _secondaryWeaponActive;
    [SerializeField]
    private float _secondaryWeaponFireRate = 1.5f;
    private float _nextSecondaryFireTime = 0f;
    private int _secondaryWeaponIndex = 0;
    private GameObject _secondaryWeaponsParentObject;

    private GamePlay _inputActions;
    private PlayerManager _playerManager;

    private void OnEnable()
    {        
        _inputActions = new GamePlay();
        if (_inputActions == null)
        {
            Debug.LogWarning("Player Input Action is Null!");
        }
        else
        {
            _inputActions.Movement.Enable();
            _inputActions.Movement.FirePrimary.performed += FirePrimaryWeapons;
            _inputActions.Movement.FireSecondary.performed += FireSecondaryWeapons;
        }

        _nextPrimaryFireTime = _primaryWeaponFireRate + Time.time;
        _nextSecondaryFireTime = _secondaryWeaponFireRate + Time.time;        
    }

    private void OnDisable()
    {
        _inputActions.Movement.Disable();
    }

    private void Start()
    {
        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.LogError("Player Manager is NULL.");
        }

        _primaryWeaponsParentObject = GameObject.Find("PrimaryWeaponsParentObject");
        if (_primaryWeaponsParentObject == null)
        {
            Debug.LogError("Primary Weapons Parent Object is NULL.");
        }
        else
        {
            int childCount = _primaryWeaponsParentObject.transform.childCount;
            _primaryWeaponPrefab = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                _primaryWeaponPrefab[i] = _primaryWeaponsParentObject.transform.GetChild(i).gameObject;
            }
        }

        _secondaryWeaponsParentObject = GameObject.Find("SecondaryWeaponsParentObject");
        if (_secondaryWeaponsParentObject == null)
        {
            Debug.LogError("Secondary Weapons Parent Object is NULL.");
        }
        else
        {
            int childCount = _secondaryWeaponsParentObject.transform.childCount;
            _secondaryWeaponPrefab = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                _secondaryWeaponPrefab[i] = _secondaryWeaponsParentObject.transform.GetChild(i).gameObject;
            }
        }
    }

    // Primary Weapons
    private void FirePrimaryWeapons(InputAction.CallbackContext context)
    {
        if (Time.time >= _nextPrimaryFireTime && _playerManager.CanFirePrimary())
        {
            int currentIndex;
            for (int i = 0; i < _primaryWeaponSpawnPoints.Length; i++)
            {
                if (_primaryWeaponActive[i])
                {
                    currentIndex = GetNextPrimaryWeaponIndex();
                    _primaryWeaponPrefab[currentIndex].transform.position = _primaryWeaponSpawnPoints[i].position;
                    _primaryWeaponPrefab[currentIndex].transform.rotation = _primaryWeaponSpawnPoints[i].rotation;
                    _primaryWeaponPrefab[currentIndex].SetActive(true);
                }
            }  
            
            _nextPrimaryFireTime = Time.time + _primaryWeaponFireRate;
            _playerManager.PrimaryFired();
        }
    }

    private int GetNextPrimaryWeaponIndex()
    {
        _primaryWeaponIndex++;
        if (_primaryWeaponIndex >= _primaryWeaponPrefab.Length)
        {
            _primaryWeaponIndex = 0;
        }
        return _primaryWeaponIndex;
    }

    public void SetPrimaryWeaponActive(bool[] weaponArray)
    {
        if (weaponArray.Length != _primaryWeaponActive.Length)
        {
            Debug.LogError("Weapon Array length mismatch!");
            return;
        }

        _primaryWeaponActive = weaponArray;
    }

    public void SetPrimaryWeaponFireRate(float newFireRate)
    {
        _primaryWeaponFireRate = newFireRate;
    }

    // Secondary Weapons
    private void FireSecondaryWeapons(InputAction.CallbackContext context)
    {
        if (Time.time >= _nextSecondaryFireTime && _playerManager.CanFireSecondary())
        {
            int currentIndex;

            for(int i = 0; i < _secondaryWeaponSpawnPoints.Length; i++)
            {
                if (_secondaryWeaponActive[i])
                {
                    currentIndex = GetNextSecondaryWeaponIndex();
                    _secondaryWeaponPrefab[currentIndex].transform.position = _secondaryWeaponSpawnPoints[i].position;
                    _secondaryWeaponPrefab[currentIndex].transform.rotation = _secondaryWeaponSpawnPoints[i].rotation;
                    _secondaryWeaponPrefab[currentIndex].SetActive(true);
                }
            }

            _nextSecondaryFireTime = Time.time + _secondaryWeaponFireRate;
            _playerManager.SecondaryFired();
        }
    }

    private int GetNextSecondaryWeaponIndex()
    {
        _secondaryWeaponIndex++;
        if (_secondaryWeaponIndex >= _secondaryWeaponPrefab.Length)
        {
            _secondaryWeaponIndex = 0;
        }
        return _secondaryWeaponIndex;
    }

    public void SetSecondaryWeaponActive(bool[] weaponArray)
    {
        if (weaponArray.Length != _primaryWeaponActive.Length)
        {
            Debug.LogError("Weapon Array length mismatch!");
            return;
        }

        _secondaryWeaponActive = weaponArray;
    }

    public void SetSecondaryWeaponFireRate(float newFireRate)
    {
        _secondaryWeaponFireRate = newFireRate;
    }
}
