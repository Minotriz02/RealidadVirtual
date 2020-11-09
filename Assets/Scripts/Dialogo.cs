using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    //public AudioClip dialogo;
    public AudioSource fuente;
    public GameObject personaje;
    float distancia;
    public Camera camarapersonaje;
    

    // Start is called before the first frame update
    void Awake()
    {
        fuente = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        distancia = Vector3.Distance(this.transform.position, personaje.transform.position);

        if (distancia <= 2.75f && Input.GetKey(KeyCode.E))
        {
            fuente.Play();
        }
    }
}
