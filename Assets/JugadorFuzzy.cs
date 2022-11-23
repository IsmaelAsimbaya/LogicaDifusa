using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorFuzzy : MonoBehaviour
{
    public float nivel = 100;
    public Vector3 posicion;

    void Start()
    {
        nivel = 100;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            nivel += 5;
            if (nivel > 100)
                nivel = 100;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            nivel -= 5;
            if (nivel < 0)
                nivel = 0;
        }

        posicion = transform.position;
    }
}
