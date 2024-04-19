using UnityEngine;

[CreateAssetMenu (fileName = "Shoot Configuration", menuName = "Guns/Shoot Configuration", order = 2)]
public class ShootConfigurationObject : ScriptableObject
{
    public LayerMask HitMask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float FireRate = 0.15f;
}
