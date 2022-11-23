using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMembresia
{

    public static float Booleana(float valor, float x0)
    {
        float membresia = 0f;

        if (valor <= x0)
            membresia = 0f;
        else
            membresia = 1f;

        return membresia;
    }

    public static float BoolInversa(float valor, float x0)
    {
        float membresia = 0f;

        if (valor <= x0)
            membresia = 1f;
        else
            membresia = 0f;

        return membresia;
    }

    public static float Grado(float valor, float x0, float x1)
    {
        float membresia = 0f;

        if (valor <= x0)
            membresia = 0f;
        else if (valor > x0 && valor < x1)
            membresia = (valor / (x1 - x0)) - (x0 / (x1 - x0));
        else if (valor >= x1)
            membresia = 1f;

        return membresia;
    }

    public static float GradoInversa(float valor, float x0, float x1)
    {
        float membresia = 0f;

        if (valor <= x0)
            membresia = 1f;
        else if (valor > x0 && valor < x1)
            membresia = (-valor / (x1 - x0)) + (x1 / (x1 - x0));
        else if (valor >= x1)
            membresia = 0f;

        return membresia;
    }

    public static float Triangulo(float valor, float x0, float x1, float x2)
    {
        float membresia = 0;

        if (valor <= x0)
            membresia = 0;
        else if ((valor > x0) && (valor < x1))
            membresia = (valor / (x1 - x0)) - (x0 / (x1 - x0));
        else if (valor == x1)
            membresia = 1;
        else if ((valor > x1) && (valor < x2))
            membresia = (-valor / (x2 - x1)) + (x2 / (x2 - x1));
        else if (valor >= x2)
            membresia = 0;

        return membresia;
    }

    public static float Trapezoide(float valor, float x0, float x1, float x2, float x3)
    {
        float membresia = 0;

        if (valor <= x0)
            membresia = 0;
        else if ((valor > x0) && (valor < x1))
            membresia = (valor / (x1 - x0)) - (x0 / (x1 - x0));
        else if ((valor >= x1) && (valor <= x2))
            membresia = 1;
        else if ((valor > x2) && (valor < x3))
            membresia = (-valor / (x3 - x2)) + (x3 / (x3 - x2));
        else if (valor >= x3)
            membresia = 0;

        return membresia;
    }

}
