using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public Transform hookTarget; // Objeto al que el gancho se enganchará
    public float hookSpeed; // Velocidad del gancho
    public float hookRetractSpeed; // Velocidad de retracción del gancho
    public float hookLength; // Longitud máxima del gancho
    public float swingForce; // Fuerza de oscilación del gancho
    public float gravityScale; // Escala de la gravedad del personaje mientras está enganchado
    public LayerMask hookableLayerMask; // Máscara de capa de objetos a los que se puede enganchar el gancho

    private bool hooked; // Indica si el gancho está enganchado
    private bool retracting; // Indica si el gancho está retractándose
    private Vector2 hookPosition; // Posición del gancho actual
    private Vector3 hookTargetPosition; // Posición del objetivo del gancho
    private Vector2 swingDirection; // Dirección de oscilación del gancho
    private float distanceToTarget; // Distancia al objetivo del gancho
    private Rigidbody2D playerRigidbody; // Rigidbody2D del jugador


    void Start()
    {
        hooked = false;
        retracting = false;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!hooked)
            {
                // Lanzamos el gancho
                RaycastHit2D hit = Physics2D.Raycast(transform.position, GetMouseDirection(), hookLength, hookableLayerMask);
                if (hit.collider != null)
                {
                    hooked = true;
                    hookPosition = transform.position;
                    hookTargetPosition = hit.point;
                    swingDirection = Vector3.Cross(GetMouseDirection(), Vector3.forward).normalized;
                    distanceToTarget = Vector3.Distance(hookPosition, hookTargetPosition);
                    playerRigidbody.gravityScale = gravityScale;
                }
            }
            else if (!retracting)
            {
                // Empezamos a retractar el gancho
                retracting = true;
                playerRigidbody.gravityScale = 1f;
            }
        }

        if (hooked)
        {
            // Movemos el gancho hacia el objetivo
            hookPosition = Vector2.MoveTowards(hookPosition, hookTargetPosition, hookSpeed * Time.deltaTime);
            transform.position = hookPosition;

            // Si el gancho alcanza el objetivo, enganchamos al jugador
            if (Vector2.Distance(transform.position, hookTargetPosition) < 1f)
            {
                playerRigidbody.velocity = Vector2.zero;
                playerRigidbody.gravityScale = gravityScale;
                retracting = false;
            }
            else if (retracting)
            {
                // Retraemos el gancho
                hookPosition = Vector3.MoveTowards(hookPosition, transform.position, hookRetractSpeed * Time.deltaTime);
                transform.position = hookPosition;

                // Si el gancho regresa al jugador, desengancharlo
                if (Vector3.Distance(transform.position, hookTargetPosition) > distanceToTarget)
                {
                    RetractHook();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (hooked && !retracting)
        {
            // Aplicamos una fuerza de oscilación al jugador
            playerRigidbody.AddForce(swingDirection * swingForce);
        }
    }

    Vector3 GetMouseDirection()
    {
        // Devuelve la dirección del ratón desde la posición del jugador
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return (mousePos - transform.position).normalized;
    }

    void RetractHook()
    {
        // Desengancha al jugador y regresa el gancho al jugador
        hooked = false;
        retracting = false;
        playerRigidbody.gravityScale = 1f;
        transform.position = hookPosition;
    }

}
