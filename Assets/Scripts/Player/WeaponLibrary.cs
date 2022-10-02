using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLibrary : MonoBehaviour
{
    public delegate void Weapon(Quaternion rotation);

    public enum WeaponName { Straight }
    
    private static Weapon[] weapons = { Straight };

    public static Weapon GetWeapon(WeaponName name)
    {
        return weapons[(int)name];
    }

    public static void Straight(Quaternion rotation)
    {
        
    }
}
