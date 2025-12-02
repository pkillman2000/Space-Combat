using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Level : MonoBehaviour
{
    public int level;
    public string sceneName;
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;
    public int numberOfStars;

    [Header("Player")]
    public int primaryFireRate;
    public int primaryDamage;
    public int shipSpeed;
    public int shieldStrength;
    public int armor;

    [Header("Resupply")]
    public int armorWeight;
    public int shieldWeight;
    public int missileWeight;

    [Header("Obstacles")]
    public int asteroidFieldWeight;
    public int debrisFieldWeight;
    public int gravimetricAnchorWeight;
    public int quasarWeight;
    public int voidGateWeight;
    public int gasCloudWeight;
    public int blackHoleWeight;
    public int nebulaWeight;

    [Header("Enemies")]
    public int gruntWeight;
    public int skirmisherWeight;
    public int bomberWeight;
    public int sentinelWeight;
    public int spreadShooterWeight;
    public int mineLayerWeight;
    public int predatorWeight;
    public int pulsarWeight;
    public int interceptorWeight;
    public int dreadnoughtWeight;
}
