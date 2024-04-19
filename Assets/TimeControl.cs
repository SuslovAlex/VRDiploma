using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TimeController : MonoBehaviour
{
    public InputActionProperty slowTime;  // �������� ����� ��� ���������� �������
    public float slowMotionFactor = 0.5f; // ������ ���������� �������
    public float slowMotionDuration = 2f; // ����������������� ���������� �������

    private void OnEnable()
    {
        // ������������ callback ��� ������� 'performed' ������� �����������, ����� ������ ������
        slowTime.action.performed += OnSlowTimePerformed;
    }

    private void OnDisable()
    {
        // ������� callback ��� ���������� �������, ����� �������� ������ ������
        slowTime.action.performed -= OnSlowTimePerformed;
    }

    private void OnSlowTimePerformed(InputAction.CallbackContext context)
    {
        // ��������� ��������, ������� ��������� ���������� �������
        StartCoroutine(SlowMotionEffect());
    }

    private IEnumerator SlowMotionEffect()
    {
        Time.timeScale = slowMotionFactor;  // ��������� �����
        yield return new WaitForSecondsRealtime(slowMotionDuration); // ���� � �������� �������
        Time.timeScale = 1.0f;  // ���������� ����� � ���������� ���������
    }
}