using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float speed = 5.0f; // —корость движени€ куба
    public float moveDistance = 10.0f; // ћаксимальное рассто€ние перемещени€ в одном направлении

    private Vector3 startPosition;
    private bool movingRight = true; // Ќачальное направление движени€

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ѕровер€ем, как далеко куб от начальной позиции
        if (movingRight && transform.position.x - startPosition.x > moveDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && startPosition.x - transform.position.x > moveDistance)
        {
            movingRight = true;
        }

        // ƒвигаем куб влево или вправо
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}

