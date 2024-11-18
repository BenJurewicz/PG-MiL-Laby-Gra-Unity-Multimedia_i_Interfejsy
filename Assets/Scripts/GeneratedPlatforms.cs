using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    const int PLATFORMSS_NUM = 5;
    private Vector3[] positions;
    private GameObject[] platforms;
    [SerializeField] private Vector3[] platformsVectors;
    [SerializeField] private float radius = 1.0f; // Promieñ okrêgu
    [SerializeField] private float speed = 3.0f; //

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        platforms = new GameObject[PLATFORMSS_NUM];
        positions = new Vector3[PLATFORMSS_NUM];
        for (int i = 0;i<PLATFORMSS_NUM;i++)
        {

            float angle = i * Mathf.PI * 2 / PLATFORMSS_NUM; // K¹t dla ka¿dej platformy
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, y, 0);

            // Tworzenie instancji platformy w wyznaczonej pozycji
            platforms[i] = Instantiate(platformPrefab, positions[i], Quaternion.identity);
        }
    }
}
