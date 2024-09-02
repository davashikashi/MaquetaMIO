using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMovement : MonoBehaviour
{
    BusMovement busMovement;
    public List<GameObject> people = new List<GameObject>();

    void Start()
    {
        // Encuentra el objeto que contiene BusMovement y accede a su variable pública
        busMovement = FindObjectOfType<BusMovement>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Verifica si el estado de 'stop' cambió a true
        if (busMovement.stop1)
        {
            people[0].transform.Translate(3.0f * Time.deltaTime * Vector3.forward);
        }
        if (busMovement.stop2)
        {
            people[1].transform.Translate(3.0f * Time.deltaTime * Vector3.forward);
        }
        
    }
       
}