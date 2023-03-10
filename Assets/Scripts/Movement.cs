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

    public float movementSpeed = 10f;
    public Vector2 direction;
    public Vector2 velocity;
    public LayerMask floorMask;
    public bool isgrounded;

    public GameObject hitObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Se va a encaragr de leer los inputs y calcular la velocidad
    void Update()
    {
        //axis.Y es 0 porque no se modifica la velocidad vertical a excepcion del salto.
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        /*Tambien se puede hacer asi.
         * axis.x = Input.GetAxisRaw("Horizontal");
         * axis.y = Input.GetAxisRaw("Vertical");
         */

        //Haze que la velocidad este en un rango de -1 a 1
        direction.Normalize();
        velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
        if (isgrounded && Input.GetAxisRaw("Vertical") > 0)
        {
            velocity = new Vector2(direction.x * movementSpeed, direction.y * movementSpeed);


        }




    }

    //Se va a encargar de mover el player
    void FixedUpdate()
    {
        hitObject = Physics2D.CircleCast(transform.position, 0.5f, (Vector2.up * -1), 1f, floorMask).collider.gameObject;
        if (hitObject != null)
        {
            isgrounded = true;
        }
        else
            isgrounded = false;
        

        //Time.deltatime es el tiempo que ha pasado entre frames, al ser un numero muy pequeño hay que aumentar la variable movementSpeed
        rb.velocity = velocity;
    }
    void OnDrawGizmos()
    {
        
        Vector2 groundCheckPosition = new Vector2(transform.position.x, transform.position.y) + (Vector2.up * -1);
        float groundCheckRadius = 0.5f;
        Gizmos.DrawWireSphere(groundCheckPosition, groundCheckRadius);

    }


}
