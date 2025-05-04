using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [Header("Настройки движения")]
    public Transform[] waypoints;           // Массив вейпоинтов
    public float moveSpeed = 3f;           // Скорость движения
    public float minDistanceToWaypoint = 0.1f; // Дистанция для перехода к следующей точке

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Получаем текущий вейпоинт .
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Поворачиваем объект к точке (мгновенно)
        transform.LookAt(targetWaypoint);

        // Двигаемся вперед
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

        // Проверяем достижение точки
        if (Vector3.Distance(transform.position, targetWaypoint.position) < minDistanceToWaypoint)
        {
            // Переключаемся на следующий вейпоинт
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}