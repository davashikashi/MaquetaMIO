using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BusMovement : MonoBehaviour
{
    public float speed = 80.0f;
    public float RotationSpeed = 2.0f; // Velocidad de rotación en grados por segundo
    public float targetAngle;  // Ángulo objetivo para detener la rotación

    int direction = 1;
    int people = 0;

    private bool shouldRotate = false;
    private bool goDirection = true;
    public bool stop1 = false;
    public bool stop2 = false;
    public bool fin = false;

    private Rigidbody rb;
    public List<BoxCollider> boxColliders = new List<BoxCollider>();
    public TextMeshProUGUI textElement;
    public GameObject boton;

    public AudioSource busMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        busMovement.Play();
    }
    void OnTriggerEnter(Collider other)
    {
        Quaternion rotacion = transform.rotation;
        // Convertir la rotación a ángulos eulerianos
        Vector3 angulos = rotacion.eulerAngles;
        targetAngle = Mathf.RoundToInt(angulos.y);

        if (other.gameObject.CompareTag("turnRight"))
        {
            direction = 1; 
            shouldRotate = true;
        }
        if (other.gameObject.CompareTag("turnLeft"))
        {
            direction = -1;
            shouldRotate = true;
        }
        if (other.gameObject.CompareTag("back"))
        {
            goDirection = false;
            fin = true;
            direction = -1;
            shouldRotate = true;
            textElement.text = "Fin del transcurso";
            busMovement.Stop();
            boton.SetActive(true);
        }
        if (other.gameObject.CompareTag("stop1"))
        {
            stop1 = true;
            textElement.text = "Av 7 Cl 6";
            boxColliders[0].enabled = false;
            boxColliders[1].enabled = true;
        }
        if (other.gameObject.CompareTag("stop2"))
        {
            stop2 = true;
            textElement.text = "Av 7 Cl 8";
            boxColliders[0].enabled = false;
            boxColliders[1].enabled = true;
        }
        if (other.gameObject.CompareTag("person"))
        {
            people += 1;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Cuando el objeto sale de la colisión, resetea la bandera
        if (other.gameObject.CompareTag("stop1"))
        {
            boxColliders[0].enabled = true;
            boxColliders[1].enabled = false;
        }
        if (other.gameObject.CompareTag("stop2"))
        {
            boxColliders[0].enabled = true;
            boxColliders[1].enabled = false;
        }
    }

    void Movement()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.left);
    }

    void TurnBus()
    {
        float rotationStep = direction * 10 * Time.deltaTime * RotationSpeed;
        
        // Crear un quaternion para la rotación incremental
        Quaternion deltaRotation = Quaternion.Euler(0, rotationStep, 0);
        // Aplicar la rotación incremental al Rigidbody
        rb.MoveRotation(rb.rotation * deltaRotation);
        
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldRotate == false && stop1 == false && stop2 == false && fin == false)
        {
            Movement();
        }
        if (people == 3)
        {
            stop1 = false;
            stop2 = false;
            people = 0;
        }
    }

    void FixedUpdate()
    {
        Quaternion rotacion = transform.rotation;
        // Convertir la rotación a ángulos eulerianos
        Vector3 angulos = rotacion.eulerAngles;
        float RoY = Mathf.RoundToInt(angulos.y);

        
        if(goDirection && shouldRotate && direction == 1 && RoY < targetAngle + 90)
        {
            TurnBus();
        }
        else if (goDirection && shouldRotate && direction == -1 && RoY > targetAngle - 90)
        {
            TurnBus();
        }
        else
        {
            shouldRotate = false;
        }
    
    }
}
