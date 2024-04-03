using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletActivationTrigger : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPointBullet;
    public float bulletSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbleObject = GetComponent<XRGrabInteractable>();
        grabbleObject.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireBullet(ActivateEventArgs args)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPointBullet.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPointBullet.forward * bulletSpeed;
        Destroy(spawnedBullet, 5);
    }
}
