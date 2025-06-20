using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    int health;
    int damagePower;
    double movementSpeed;
    double hitSpeed;
    int goldCount;
    int xIndex;
    int yIndex;
    int zIndex;
    
    public int Health { get { return health; } set { health = value; } }
    public int DamagePower { get { return damagePower; } set { damagePower = value; } }
    public double MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public double HitSpeed { get { return hitSpeed; } set { hitSpeed = value; } }
    public int GoldCount { get { return goldCount; } set { goldCount = value; } }
    public int XIndex { get { return xIndex; } set { xIndex = value; } }
    public int YIndex { get { return yIndex; } set { yIndex = value; } }
    public int ZIndex { get { return zIndex; } set { zIndex = value; } }

    public Hero(int health, int damagePower, double movementSpeed, double hitSpeed, int goldCount, int xIndex, int yIndex, int zIndex)
    {
        this.health = health;
        this.damagePower = damagePower;
        this.movementSpeed = movementSpeed;
        this.hitSpeed = hitSpeed;
        this.goldCount = goldCount;
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.zIndex = zIndex;
    }

    public abstract void PickUpItem();
    public abstract void Heal();
    public abstract void Attack();
    public abstract void IncreaseHitSpeed();
    public abstract void SetToDefaultHitSpeed();
    public abstract void IncreaseMovementSpeed();
    public abstract void SetToDefaultMovementSpeed();
    public abstract void DisplayField();

    public override string ToString()
    {
        return $"(SOME SORT OF HERO)This hero has {health}hp, {goldCount} amount of gold, has a hitspeed of {hitSpeed} hit/sec, has a movementspeed of {movementSpeed} step/sec and it's position is [x: {xIndex}, y: {yIndex}, z: {zIndex}";
    }

}
