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
        if (waypoints.Length == 0) return; // Sprawdzenie, czy s� jakiekolwiek punkty kontrolne

        // Oblicz dystans mi�dzy bie��c� pozycj� platformy a pozycj� docelowego punktu
        float distance = Vector2.Distance(transform.position, waypoints[currentWaypoint].transform.position);

        // Sprawdzenie, czy platforma zbli�y�a si� wystarczaj�co blisko do punktu kontrolnego
        if (distance < 0.1f)
        {
            // Zwi�ksz indeks punktu docelowego, modulo liczba punkt�w kontrolnych
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }

        // Przesu� platform� w kierunku bie��cego punktu docelowego
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypoint].transform.position,
            speed * Time.deltaTime
        );
    }
}

