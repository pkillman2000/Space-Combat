using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Image _ammo;
    [SerializeField]
    private PlayerMissilesUI _missiles;
    [SerializeField]
    private Image _shields;
    [SerializeField]
    private Image _armor;
    [SerializeField]
    private PlayerShields _shieldsVFX;
    [SerializeField]
    private GameObject[] _damageVFX;
    [SerializeField]
    private TMP_Text _score;
    [SerializeField]
    private TMP_Text _enemiesRemaining;


    void Start()
    {

    }


    void Update()
    {

    }

    public void SetShieldStrength(int strength, int maxStrength)
    {
        _shieldsVFX.SetShieldStrength(strength, maxStrength);

        float shieldPercentage = strength / maxStrength;
        _shields.fillAmount = shieldPercentage;
    }

    public void SetArmorStrength(int armor, int maxArmor)
    {
        // Health background fill
        float healthPercentage = armor / maxArmor;
        _armor.fillAmount = healthPercentage;

        // Clear all player damage VFX
        for (int i = 0; i < _damageVFX.Length; i++)
        {
            _damageVFX[i].SetActive(false);
        }

        // Set damage as appropriate
        if(healthPercentage < 83)
        {
            _damageVFX[0].SetActive(true);
        }
        if(healthPercentage < 66)
        {
            _damageVFX[1].SetActive(true);
        }
        if(healthPercentage < 50)
        {
            _damageVFX[2].SetActive(true);
        }
        if(healthPercentage < 33)
        {
            _damageVFX[3].SetActive(true);
        }
        if (healthPercentage < 16)
        {
            _damageVFX[4].SetActive(true);
        }
    }

    public void SetAmmoAmount(int ammo, int maxAmmo)
    {
        float ammoPercentage = ammo / maxAmmo;
        _ammo.fillAmount = ammoPercentage;
    }

    public void SetEnemyRemaining(int enemyRemaining)
    {
        _enemiesRemaining.text = enemyRemaining.ToString();
    }

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }

    public void SetMissiles(int missiles, int maxMissiles)
    {
        _missiles.SetMissiles(missiles);
    }
}
