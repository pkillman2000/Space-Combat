using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    private Image _primaryAmmo;
    [SerializeField]
    private PlayerMissilesUI _secondaryAmmo;
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

        float shieldPercentage = (float)strength / (float)maxStrength;
        _shields.fillAmount = shieldPercentage;
    }

    public void SetArmorStrength(int armor, int maxArmor)
    {
        // Health background fill
        float healthPercentage = (float)armor / (float)maxArmor;
        _armor.fillAmount = 1 - healthPercentage;
    }

    public void SetPrimaryAmmo(int ammo, int maxAmmo)
    {
        float ammoPercentage = (float)ammo / (float)maxAmmo;
        _primaryAmmo.fillAmount = ammoPercentage;
    }

    public void SetSecondaryAmmo(int ammo)
    {
        _secondaryAmmo.SetMissiles(ammo);
    }

    public void SetEnemyRemaining(int enemyRemaining, int enemyStart)
    {
        _enemiesRemaining.text = enemyRemaining.ToString();
    }

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }
}
