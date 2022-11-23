using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeFuzzy : MonoBehaviour
{

    public JugadorFuzzy jugador;

    private float distancia = 0;
    private float nivel = 0;
    private float salud = 100;

    // Conjunto difuso del tipo ataque
    private float ataquePuno = 0;
    private float ataquePatada = 0;
    private float ataquePiedra = 0;

    // Conjunto difuso de la salud
    private float saludMuerto = 0;
    private float saludLastimado = 0;
    private float saludSaludable = 0;

    // Conjunto difuso nivel enemigo
    private float nivelFacil = 0;
    private float nivelMedio = 0;
    private float nivelDificil = 0;

    // Conjunto difuso de las reglas difusas
    private float gradoPatrullar = 0;
    private float gradoAtaque = 0;
    private float gradoHuir = 0;

    private int accion = 0;
    private float salida = 0;


    void Start()
    {
        salud = 100;
    }

    void Update()
    {
        // Obtenemos la distancia
        distancia = Vector3.Magnitude(transform.position - jugador.posicion);

        // Obtenemos el nivel
        nivel = jugador.nivel;

        if (Input.GetKeyDown(KeyCode.T))
        {
            salud += 5;
            if (salud > 100)
                salud = 100;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            salud -= 5;
            if (salud < 0)
                salud = 0;
        }

        // Fuzzificamos las entradas

        // Relacionadas con ataque
        ataquePuno = FMembresia.GradoInversa(distancia, 0, 6);
        ataquePatada = FMembresia.Trapezoide(distancia, 2, 10, 12, 16);
        ataquePiedra = FMembresia.Grado(distancia, 14, 18);
        string ataques = string.Format("Ataque <- Puno:{0}, Pat:{1}, Pied:{2}", ataquePuno, ataquePatada, ataquePiedra);
        Debug.Log(ataques);

        //Relacionadas con salud 
        saludMuerto = FMembresia.GradoInversa(salud, 0, 35);
        saludLastimado = FMembresia.Trapezoide(salud, 15, 40, 50, 65);
        saludSaludable = FMembresia.Grado(salud, 45, 80);
        string saluds = string.Format("Salud <- Muer:{0}, Last:{1}, Sal:{2}", saludMuerto, saludLastimado, saludSaludable);
        Debug.Log(saluds);

        nivelFacil = FMembresia.GradoInversa(nivel, 20, 50);
        nivelMedio = FMembresia.Triangulo(nivel, 30, 60, 90);
        nivelDificil = FMembresia.Grado(nivel, 70, 100);
        string niveles = string.Format("Nivel <- Fac:{0}, Med:{1}, Dif:{2}", nivelFacil, nivelMedio, nivelDificil);
        Debug.Log(niveles);

        // Reglas difusas
        gradoAtaque = 0;
        gradoHuir = 0;
        gradoPatrullar = 0;

        // Ataca cuando (saludable && medio) || (lastimado && facil)
        gradoAtaque = OperadorF.OR(OperadorF.AND(saludSaludable, nivelMedio),
                                   OperadorF.AND(saludLastimado, nivelFacil));

        // Huye cuando (lastimado && puno) || (lastimado && patada)
        gradoHuir = OperadorF.OR(OperadorF.AND(saludLastimado, ataquePuno),
                                   OperadorF.AND(saludLastimado, ataquePatada));

        //Patrullar cuando (muerto && piedra) || (dificil || muerto)
        gradoPatrullar = OperadorF.OR(OperadorF.AND(saludMuerto, ataquePiedra),
                                   OperadorF.OR(nivelDificil, saludMuerto));

        string grados = string.Format("At {0}, Hu {1}, Pat {2}", gradoAtaque, gradoHuir, gradoPatrullar);
        Debug.Log(grados);

        // Decidimos que accion tomar
        if (gradoAtaque > gradoHuir && gradoAtaque > gradoPatrullar)
            accion = 0;
        else if (gradoHuir > gradoAtaque && gradoHuir > gradoPatrullar)
            accion = 1;
        else
            accion = 2;

        // Defuzzificamos

        salida = (gradoAtaque * 10 + gradoHuir * -10 + gradoPatrullar * 5) / (gradoAtaque + gradoHuir + gradoPatrullar);
        if (salida < 0)
            salida = -1;

        Debug.Log(accion + "  " + salida);

        if (accion == 0)//ataque
        {
            transform.LookAt(jugador.posicion);
            transform.Translate(Vector3.forward * salida * Time.deltaTime);
        }
        if (accion == 1)//huir
        {
            transform.LookAt(jugador.posicion);
            transform.Translate(Vector3.forward * salida * Time.deltaTime);
        }
        if (accion == 2)//patrullar
        {
            transform.LookAt(new Vector3(1, 0, 0));
            transform.Translate(Vector3.forward * salida * Time.deltaTime);
        }

    }
}
