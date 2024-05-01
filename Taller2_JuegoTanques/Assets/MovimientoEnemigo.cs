using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del enemigo

    private Transform tanque; // Referencia al transform del tanque

    void Start()
    {
        // Encontrar el objeto con el tag "Tanque"
        GameObject tanqueObject = GameObject.FindGameObjectWithTag("Tanque");
        if (tanqueObject != null)
        {
            tanque = tanqueObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró el objeto con el tag 'Tanque'");
        }
    }

    void FixedUpdate()
    {
        if (tanque != null)
        {
            // Obtener la dirección hacia la que debe moverse el enemigo
            Vector3 direccion = (tanque.position - transform.position).normalized;

            // Calcular la posición a la que se debe mover el enemigo
            Vector3 posicionDestino = transform.position + direccion * velocidad * Time.fixedDeltaTime;

            // Mover al enemigo gradualmente hacia la posición del tanque
            transform.position = Vector3.MoveTowards(transform.position, tanque.position, velocidad * Time.fixedDeltaTime);

            // Rotar al enemigo para que mire hacia el tanque (opcional)
            transform.LookAt(tanque);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con el objeto que tiene el tag "Tanque"
        if (collision.gameObject.CompareTag("Tanque"))
        {
            // Destruir el gameObject del tanque
            Destroy(collision.gameObject);

            // Terminar el juego
            FinalizarJuego();
        }

        // Si colisiona con el objeto que tiene el tag "Bala"
        if (collision.gameObject.CompareTag("Bala"))
        {
            // Destruir el gameObject de la bala
            Destroy(collision.gameObject);

            // Destruir el gameObject del enemigo
            Destroy(gameObject);
        }
    }


    void FinalizarJuego()
    {
        // Salir de la aplicación (terminar el juego)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
