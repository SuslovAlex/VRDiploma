using UnityEngine;

[CreateAssetMenu(fileName = "Trail Configuration", menuName = "Guns/Gun Trail Config", order = 4)]
public class TrailConfigurationObject : ScriptableObject
{
    public Material Material;
    public AnimationCurve WidthCurve;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;
    public Gradient Color;

    public float SimulationSpeed = 10f;
    public float MissDistance = 60f;
}
