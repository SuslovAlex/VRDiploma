using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TimeController : MonoBehaviour
{
    public InputActionProperty slowTime;  // Действие ввода для замедления времени
    public float slowMotionFactor = 0.5f; // Фактор замедления времени
    public float slowMotionDuration = 2f; // Продолжительность замедления времени

    private void OnEnable()
    {
        // Регистрируем callback для события 'performed' которое срабатывает, когда кнопка нажата
        slowTime.action.performed += OnSlowTimePerformed;
    }

    private void OnDisable()
    {
        // Убираем callback при выключении объекта, чтобы избежать утечек памяти
        slowTime.action.performed -= OnSlowTimePerformed;
    }

    private void OnSlowTimePerformed(InputAction.CallbackContext context)
    {
        // Запускаем корутину, которая реализует замедление времени
        StartCoroutine(SlowMotionEffect());
    }

    private IEnumerator SlowMotionEffect()
    {
        Time.timeScale = slowMotionFactor;  // Замедляем время
        yield return new WaitForSecondsRealtime(slowMotionDuration); // Ждем в реальном времени
        Time.timeScale = 1.0f;  // Возвращаем время в нормальное состояние
    }
}