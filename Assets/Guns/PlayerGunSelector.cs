using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField]
    private WeaponType Gun;
    [SerializeField]
    private Transform GunParent;
    [SerializeField]
    private List<WeaponScriptableObject> Guns;

    [Space]
    [Header("Runtime Filled")]
    public WeaponScriptableObject ActiveGun;

    private void Start()
    {
        WeaponScriptableObject gun = Guns.Find(gun => gun.type == Gun);

        if (gun == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
            return;
        }

        ActiveGun = gun;
        gun.Spawn(GunParent, this);

        Transform[] allChildren = GunParent.GetComponentsInChildren<Transform>();
    }
}
