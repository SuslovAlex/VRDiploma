using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BulletActivationTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPointBullet;
    [SerializeField] private float bulletSpeed = 30;
   
    public void FireBullet(ActivateEventArgs args)
    {
        GameObject spawnedBullet = Instantiate(bullet, spawnPointBullet.position, spawnPointBullet.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPointBullet.forward * bulletSpeed;
        Destroy(spawnedBullet, 5f);
    }
}
