using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float speed = 5.0f; // �������� �������� ����
    public float moveDistance = 10.0f; // ������������ ���������� ����������� � ����� �����������

    private Vector3 startPosition;
    private bool movingRight = true; // ��������� ����������� ��������

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ���������, ��� ������ ��� �� ��������� �������
        if (movingRight && transform.position.x - startPosition.x > moveDistance)
        {
            movingRight = false;
        }
        else if (!movingRight && startPosition.x - transform.position.x > moveDistance)
        {
            movingRight = true;
        }

        // ������� ��� ����� ��� ������
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

