using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShields : MonoBehaviour
{
    [SerializeField]
    private Material _shieldMaterial;
    [SerializeField]
    private ParticleSystem _shieldVFX;
    [SerializeField]
    private Color[] _shieldColors;
    private Color _currentShieldColor;
    private float _currentShieldStrengthPercentage = 100f;
    private int _maxShieldStrength;
    private int _currentShieldStrength;

    private void Start()
    {
        _currentShieldStrength = _maxShieldStrength;
        _currentShieldColor = _shieldColors[0];
        _shieldMaterial.SetColor("_TintColor", _currentShieldColor);
    }

    public void SetShieldStrength(int strength, int  maxStrength)
    {
        _currentShieldStrength = strength;
        _maxShieldStrength = maxStrength;

        if (_currentShieldStrength > 0)
        {
            if (!_shieldVFX.gameObject.activeSelf)
            {
                _shieldVFX.gameObject.SetActive(true);
            }
        }
        else
        {
            _shieldVFX.gameObject.SetActive(false);
        }

        UpdateShieldColor();
    }

    private void UpdateShieldColor()
    {
        _currentShieldStrengthPercentage = (_currentShieldStrength / _maxShieldStrength) * 100;
        _currentShieldStrengthPercentage = Mathf.Clamp( _currentShieldStrengthPercentage, 0.0f, 100.0f);

        if (_currentShieldStrengthPercentage > 50)
        {
            // Lerp from green to blue (100 to 51)
            float t = (_currentShieldStrengthPercentage - 51f) / (100f - 51f); // normalize between 1 and 0
            _currentShieldColor = Color.Lerp(_shieldColors[1], _shieldColors[0], t);
        }
        else
        {
            // Lerp from blue to red (50 to 0)
            float t = (_currentShieldStrengthPercentage - 1f) / (50f - 0f); // normalize between 1 and 0
            _currentShieldColor = Color.Lerp(_shieldColors[2], _shieldColors[1], t);
        }

        _shieldMaterial.SetColor("_TintColor", _currentShieldColor);
    }

}
