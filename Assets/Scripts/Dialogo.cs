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
    Vector3 originrc;
    Vector3 directionrc;
    
    void Awake()
    {
        fuente = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        originrc = camarapersonaje.transform.position;
        directionrc = camarapersonaje.transform.forward;
        distancia = Vector3.Distance(this.transform.position, personaje.transform.position);

        if(Physics.Raycast(originrc,directionrc,out hit, 10f))
        {
            string name = hit.collider.gameObject.name;
            if (distancia <= 2.75f && Input.GetKey(KeyCode.E)&&name==this.name)
            {
                fuente.Play();
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//Color del rayo en el editor
        Gizmos.DrawRay(camarapersonaje.transform.position, camarapersonaje.transform.forward*10f);
    }
}
