using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float speed = 4.0f;

    void Update()
    {
        if (waypoints.Length == 0) return; // Sprawdzenie, czy s¹ jakiekolwiek punkty kontrolne

        // Oblicz dystans miêdzy bie¿¹c¹ pozycj¹ platformy a pozycj¹ docelowego punktu
        float distance = Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position);

        // Sprawdzenie, czy platforma zbli¿y³a siê wystarczaj¹co blisko do punktu kontrolnego
        if (distance < 0.1f)
        {
            // Zwiêksz indeks punktu docelowego, modulo liczba punktów kontrolnych
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        // Przesuñ platformê w kierunku bie¿¹cego punktu docelowego
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );
    }
}

