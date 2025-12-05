using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Primary Weapon Settings")]
    [SerializeField]
    private GameObject[] _primaryWeaponPrefab;
    [SerializeField]
    private Transform[] _primaryWeaponSpawnPoints;
    [SerializeField]
    private float _primaryWeaponFireRate = 0.5f;
    private float _nextPrimaryFireTime = 0f;
    private int _primaryWeaponIndex = 0;

    [Header("Secondary Weapon Settings")]
    [SerializeField]
    private GameObject[] _secondaryWeaponPrefab;
    [SerializeField]
    private Transform[] _secondaryWeaponSpawnPoints;
    [SerializeField]
    private float _secondaryWeaponFireRate = 1.5f;
    private float _nextSecondaryFireTime = 0f;
    private int _secondaryWeaponIndex = 0;

    [Header("Misc Settings")]
    [SerializeField]
    private GameObject _weaponsParentObject;

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
    }
    private void FirePrimaryWeapons(InputAction.CallbackContext context)
    {
        if (Time.time >= _nextPrimaryFireTime && _playerManager.CanFirePrimary())
        {
            int currentIndex = GetNextPrimaryWeaponIndex();
            _primaryWeaponPrefab[currentIndex].transform.position = _primaryWeaponSpawnPoints[0].position;
            _primaryWeaponPrefab[currentIndex].transform.rotation = _primaryWeaponSpawnPoints[0].rotation;
            _primaryWeaponPrefab[currentIndex].SetActive(true);

            currentIndex = GetNextPrimaryWeaponIndex();
            _primaryWeaponPrefab[currentIndex].transform.position = _primaryWeaponSpawnPoints[1].position;
            _primaryWeaponPrefab[currentIndex].transform.rotation = _primaryWeaponSpawnPoints[1].rotation;
            _primaryWeaponPrefab[currentIndex].SetActive(true);

            _nextPrimaryFireTime = Time.time + _primaryWeaponFireRate;
            _playerManager.PrimaryFired();
        }
    }

    private void FireSecondaryWeapons(InputAction.CallbackContext context)
    {
        if (Time.time >= _nextSecondaryFireTime && _playerManager.CanFireSecondary())
        {
            int currentIndex = GetNextSecondaryWeaponIndex();
            _secondaryWeaponPrefab[currentIndex].transform.position = _secondaryWeaponSpawnPoints[0].position;
            _secondaryWeaponPrefab[currentIndex].transform.rotation = _secondaryWeaponSpawnPoints[0].rotation;
            _secondaryWeaponPrefab[currentIndex].SetActive(true);

            _nextSecondaryFireTime = Time.time + _secondaryWeaponFireRate;
            _playerManager.SecondaryFired();
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

    private int GetNextSecondaryWeaponIndex()
    {
        _secondaryWeaponIndex++;
        if (_secondaryWeaponIndex >= _secondaryWeaponPrefab.Length)
        {
            _secondaryWeaponIndex = 0;
        }
        return _secondaryWeaponIndex;
    }
}
