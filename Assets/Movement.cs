/*
 *Pista para hacer el salto:
 *Utilizar Physics2D.CircleCast(origin, radius, direction(Vector2.up), distance(radius), layermask(mascara con la que colisionara))
 * te devuelve un bool, si es True ha colisionado si es false no lo ha hecho
 * 
 * Para hacer que salte aumenta la velocidad en el vector Y
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed = 100f;
    public Vector2 direction;
    public Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Se va a encaragr de leer los inputs y calcular la velocidad
    void Update()
    {
        //axis.Y es 0 porque no se modifica la velocidad vertical a excepcion del salto.
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        /*Tambien se puede hacer asi.
         * axis.x = Input.GetAxisRaw("Horizontal");
         * axis.y = Input.GetAxisRaw("Vertical");
         */

        //Haze que la velocidad este en un rango de -1 a 1
        direction.Normalize();
        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
    }

    //Se va a encargar de mover el player
    void FixedUpdate()
    {
        //Time.deltatime es el tiempo que ha pasado entre frames, al ser un numero muy pequeño hay que aumentar la variable movementSpeed
        rb.velocity = velocity;
    }
}
