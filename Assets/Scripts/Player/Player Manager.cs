using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int _maxHealthPoints;
    public int _currentHealthPoints;
    private float _healthRegenrationRate;
    [SerializeField]
    private GameObject[] _playerDamageVFX;

    [SerializeField]
    private int _maxShieldPoints;
    public int _currentShieldPoints;
    private float _shieldRegenerationRate;

    [SerializeField]
    private int _maxPrimaryAmmo;
    private int _currentPrimaryAmmo;
    private int _primaryAmmoRegenerationRate;

    [SerializeField]
    private int _maxSecondaryAmmo;
    private int _currentSecondaryAmmo;
    private int _secondaryAmmoRegenerationRate;

    private GameplayUI _gameplayUI;
    private PlayerShields _playerShields;

    void Start()
    {
        _gameplayUI = FindObjectOfType<GameplayUI>();
        if(_gameplayUI == null)
        {
            Debug.LogError("GameplayUI not found in the scene.");
        }

        _playerShields = GetComponent<PlayerShields>();
        if(_playerShields == null)
        {
            Debug.LogError("PlayerShields component not found on the player.");
        }

        _currentHealthPoints = _maxHealthPoints;
        _currentShieldPoints = _maxShieldPoints;
        _currentPrimaryAmmo = _maxPrimaryAmmo;
        _currentSecondaryAmmo = _maxSecondaryAmmo;
    }

    
    void Update()
    {
        /*
        RegenerateHealth();
        RegenerateShields();
        RegeneratePrimaryAmmo();
        RegenerateSecondaryAmmo();
        */

        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        if(_currentShieldPoints > damageAmount)
        {
            _currentShieldPoints -= damageAmount;
        }
        else
        {
            int remainingDamage = damageAmount - _currentShieldPoints;
            _currentShieldPoints = 0;
            _currentHealthPoints -= remainingDamage;
        }

        if(_currentHealthPoints <= 0)
        {
            Debug.Log("Player has died.");
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        //_playerShields.SetShieldStrength(_currentShieldPoints, _maxShieldPoints);
        DisplayDamage();

        _gameplayUI.SetShieldStrength(_currentShieldPoints, _maxShieldPoints);
        _gameplayUI.SetArmorStrength(_currentHealthPoints, _maxHealthPoints);
    }

    private void DisplayDamage()
    {
        ClearDamage();

        float healthPercentage = ((float)_currentHealthPoints / (float)_maxHealthPoints) * 100f;

        switch (healthPercentage)
        {            
            case float n when n > 80 && n < 100:
                _playerDamageVFX[0].SetActive(true);
            break;

            case float n when n > 60 && n <= 80:
                _playerDamageVFX[0].SetActive(true);
                _playerDamageVFX[1].SetActive(true);
            break;
            
            case float n when n > 40 && n <= 60:
                _playerDamageVFX[0].SetActive(true);
                _playerDamageVFX[1].SetActive(true);
                _playerDamageVFX[2].SetActive(true);
            break;
            
            case float n when n > 20 && n <= 40:
                _playerDamageVFX[0].SetActive(true);
                _playerDamageVFX[1].SetActive(true);
                _playerDamageVFX[2].SetActive(true);
                _playerDamageVFX[3].SetActive(true);
            break;
            
            case float n when n > 0 && n <= 20:
                _playerDamageVFX[0].SetActive(true);
                _playerDamageVFX[1].SetActive(true);
                _playerDamageVFX[2].SetActive(true);
                _playerDamageVFX[3].SetActive(true);
                _playerDamageVFX[4].SetActive(true);
            break;
        }
    }

    private void ClearDamage()
    {
        for (int i = 0; i < _playerDamageVFX.Length; i++)
        {
            _playerDamageVFX[i].SetActive(false);
        }
        
        UpdateUI();
    }

    private void RegenerateShields()
    {
        if(_currentShieldPoints < _maxShieldPoints)
        {
            _currentShieldPoints += Mathf.FloorToInt(_shieldRegenerationRate * Time.deltaTime);
            _currentShieldPoints = Mathf.Clamp(_currentShieldPoints, 0, _maxShieldPoints);
            UpdateUI();
        }
    }

    private void RegenerateHealth()
    {
        if(_currentHealthPoints < _maxHealthPoints)
        {
            _currentHealthPoints += Mathf.FloorToInt(_healthRegenrationRate * Time.deltaTime);
            _currentHealthPoints = Mathf.Clamp(_currentHealthPoints, 0, _maxHealthPoints); 
            UpdateUI();
        }
    }

    private void RegeneratePrimaryAmmo()
    {
        if(_currentPrimaryAmmo < _maxPrimaryAmmo)
        {
            _currentPrimaryAmmo += Mathf.FloorToInt(_primaryAmmoRegenerationRate * Time.deltaTime);
            _currentPrimaryAmmo = Mathf.Clamp(_currentPrimaryAmmo, 0, _maxPrimaryAmmo);
            UpdateUI();
        }
    }   

    private void RegenerateSecondaryAmmo()
    {
        if(_currentSecondaryAmmo < _maxSecondaryAmmo)
        {
            _currentSecondaryAmmo += Mathf.FloorToInt(_secondaryAmmoRegenerationRate * Time.deltaTime);
            _currentSecondaryAmmo = Mathf.Clamp(_currentSecondaryAmmo, 0, _maxSecondaryAmmo);
            UpdateUI();
        }
    }

    public bool CanFirePrimary()
    {
        return _currentPrimaryAmmo > 0;
    }

    public void PrimaryFired()
    {
        _currentPrimaryAmmo--;
    }

    public bool CanFireSecondary()
    {
        return _currentSecondaryAmmo > 0;
    }

    public void SecondaryFired()
    {
        _currentSecondaryAmmo--;
    }
}
