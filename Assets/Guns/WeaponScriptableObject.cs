using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
public class WeaponScriptableObject : ScriptableObject
{
    public ImpactType ImpactType;
    public WeaponType type;
    public string Name;
    public GameObject modelPrefab;
    public Vector3 spawnPoint;
    public Vector3 spawnRotation;

    public ShootConfigurationObject shootConfiguration;
    public TrailConfigurationObject trailConfiguration;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject model;
    private float lastShootTime;
    private ParticleSystem shootSystem;
    private ObjectPool<TrailRenderer> trailPool;

    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this.activeMonoBehaviour = activeMonoBehaviour;
        lastShootTime = 0;
        trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        model =  Instantiate(modelPrefab);
        model.transform.SetParent(Parent, false);
        model.transform.localPosition = spawnPoint;
        model.transform.localRotation = Quaternion.Euler(spawnRotation);

        shootSystem = model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        if (Time.time > shootConfiguration.FireRate + lastShootTime) 
        {
            lastShootTime = Time.time;
            shootSystem.Play();
            Vector3 shootDirection = shootSystem.transform.forward
                + new Vector3(
                    Random.Range(
                        -shootConfiguration.Spread.x,
                        shootConfiguration.Spread.x
                        ),
                    Random.Range(
                        -shootConfiguration.Spread.y,
                        shootConfiguration.Spread.y
                        ),
                    Random.Range(
                        -shootConfiguration.Spread.z,
                        shootConfiguration.Spread.z
                        )
                    );

            shootDirection.Normalize();
            if (Physics.Raycast(
                shootSystem.transform.position,
                shootDirection,
                out RaycastHit hit,
                float.MaxValue,
                shootConfiguration.HitMask
                   )
               )
            {
                activeMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        shootSystem.transform.position,
                        hit.point,
                        hit
                        )
                    );
            }

            else
            {
                activeMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        shootSystem.transform.position,
                        shootSystem.transform.position + (shootDirection * trailConfiguration.MissDistance),
                        new RaycastHit()
                        )
                    );
            }

        }
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {

        TrailRenderer instance = trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                StartPoint,
                EndPoint,
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= trailConfiguration.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = EndPoint;

        yield return new WaitForSeconds(trailConfiguration.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        trailPool.Release(instance);
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.material = trailConfiguration.Material;
        trail.colorGradient = trailConfiguration.Color;
        trail.widthCurve = trailConfiguration.WidthCurve;
        trail.minVertexDistance = trailConfiguration.MinVertexDistance;
        trail.time = trailConfiguration.Duration;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }
}
