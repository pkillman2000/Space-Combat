using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private int _maxHealthPoints;
    private float _currentHealthPoints;
    [SerializeField]
    private float _healthRegenrationRate;
    [SerializeField]
    private ParticleSystem[] _playerDamageVFX;

    [SerializeField]
    private int _maxShieldPoints;
    private float _currentShieldPoints;
    [SerializeField]
    private float _shieldRegenerationRate;

    [SerializeField]
    private int _maxPrimaryAmmo;
    private float _currentPrimaryAmmo;
    [SerializeField]
    private float _primaryAmmoRegenerationRate;

    [SerializeField]
    private int _maxSecondaryAmmo;
    private float _currentSecondaryAmmo;
    [SerializeField]
    private float _secondaryAmmoRegenerationRate;

    private GameplayUI _gameplayUI;
    private PlayerShields _playerShields;

    void Start()
    {
        
        _gameplayUI = FindFirstObjectByType<GameplayUI>();
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

        TakeDamage(60); // For testing purposes only

        ClearDamage();
        _gameplayUI.SetArmorStrength(Mathf.RoundToInt(_currentHealthPoints), _maxHealthPoints);
        _gameplayUI.SetShieldStrength(Mathf.RoundToInt(_currentShieldPoints), _maxShieldPoints);
        _gameplayUI.SetPrimaryAmmo(Mathf.RoundToInt(_currentPrimaryAmmo), _maxPrimaryAmmo);
        _gameplayUI.SetSecondaryAmmo(Mathf.RoundToInt(_currentSecondaryAmmo));
    }

    
    void Update()
    {
        
        RegenerateHealth();
        RegenerateShields();
        RegeneratePrimaryAmmo();
        RegenerateSecondaryAmmo();
    }

    public void TakeDamage(int damageAmount)
    {
        if(_currentShieldPoints > damageAmount)
        {
            _currentShieldPoints -= damageAmount;
            _gameplayUI.SetShieldStrength(Mathf.RoundToInt(_currentShieldPoints), _maxShieldPoints);
        }
        else
        {
            int remainingDamage = damageAmount - Mathf.RoundToInt(_currentShieldPoints);
            _currentShieldPoints = 0;
            _currentHealthPoints -= remainingDamage;
            DisplayDamage();
            _gameplayUI.SetArmorStrength(Mathf.RoundToInt(_currentHealthPoints), _maxHealthPoints);
        }

        if(_currentHealthPoints <= 0)
        {
            Debug.Log("Player has died.");
        }
    }

    private void DisplayDamage()
    {
        ClearDamage();

        float healthPercentage = ((float)_currentHealthPoints / (float)_maxHealthPoints) * 100f;
        /*
         * This was written this way because there is a bug
         * that causes crashes when trying to manipulate
         * ParticleSystem arrays in loops.
         */
        switch (healthPercentage)
        {            
            case float n when n > 80 && n < 100:
                _playerDamageVFX[0].Play();
            break;

            case float n when n > 60 && n <= 80:
                _playerDamageVFX[0].Play();
                _playerDamageVFX[1].Play();
                break;
            
            case float n when n > 40 && n <= 60:
                _playerDamageVFX[0].Play();
                _playerDamageVFX[1].Play();
                _playerDamageVFX[2].Play();
                break;
            
            case float n when n > 20 && n <= 40:
                _playerDamageVFX[0].Play();
                _playerDamageVFX[1].Play();
                _playerDamageVFX[2].Play();
                _playerDamageVFX[3].Play();
                break;
            
            case float n when n > 0 && n <= 20:
                _playerDamageVFX[0].Play();
                _playerDamageVFX[1].Play();
                _playerDamageVFX[2].Play();
                _playerDamageVFX[3].Play();
                _playerDamageVFX[4].Play();
            break;
        }
    }

    private void ClearDamage()
    {
        _playerDamageVFX[0].Stop();
        _playerDamageVFX[1].Stop(); 
        _playerDamageVFX[2].Stop(); 
        _playerDamageVFX[3].Stop(); 
        _playerDamageVFX[4].Stop();
    }

    private void RegenerateShields()
    {
        if(_currentShieldPoints < _maxShieldPoints)
        {
            _currentShieldPoints += _shieldRegenerationRate * Time.deltaTime;
            _currentShieldPoints = Mathf.Clamp(_currentShieldPoints, 0, _maxShieldPoints);

            _playerShields.SetShieldStrength(Mathf.RoundToInt(_currentShieldPoints), _maxShieldPoints);
            _gameplayUI.SetShieldStrength(Mathf.RoundToInt(_currentShieldPoints), _maxShieldPoints);
        }
    }

    private void RegenerateHealth()
    {
        if(_currentHealthPoints < _maxHealthPoints)
        {
            _currentHealthPoints += _healthRegenrationRate * Time.deltaTime;
            _currentHealthPoints = Mathf.Clamp(_currentHealthPoints, 0, _maxHealthPoints); 
            _gameplayUI.SetArmorStrength(Mathf.RoundToInt(_currentHealthPoints), _maxHealthPoints);
        }
    }

    private void RegeneratePrimaryAmmo()
    {
        if(_currentPrimaryAmmo < _maxPrimaryAmmo)
        {
            _currentPrimaryAmmo += _primaryAmmoRegenerationRate * Time.deltaTime;
            _currentPrimaryAmmo = Mathf.Clamp(_currentPrimaryAmmo, 0, _maxPrimaryAmmo);
            _gameplayUI.SetPrimaryAmmo(Mathf.FloorToInt(_currentPrimaryAmmo), _maxPrimaryAmmo);
        }
    }   

    private void RegenerateSecondaryAmmo()
    {
        if(_currentSecondaryAmmo < _maxSecondaryAmmo)
        {
            _currentSecondaryAmmo += _secondaryAmmoRegenerationRate * Time.deltaTime;
            _currentSecondaryAmmo = Mathf.Clamp(_currentSecondaryAmmo, 0, _maxSecondaryAmmo);
            _gameplayUI.SetSecondaryAmmo(Mathf.FloorToInt(_currentSecondaryAmmo));
        }
    }

    public bool CanFirePrimary()
    {
        return Mathf.FloorToInt(_currentPrimaryAmmo) > 0;
    }

    public void PrimaryFired()
    {
        _currentPrimaryAmmo--;
    }

    public bool CanFireSecondary()
    {
        return Mathf.FloorToInt(_currentSecondaryAmmo) > 0;
    }

    public void SecondaryFired()
    {
        _currentSecondaryAmmo--;
    }
}
