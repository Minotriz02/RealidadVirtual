using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Dialogo : MonoBehaviour
{
    //public AudioClip dialogo;
    public AudioSource fuente;
    public GameObject personaje;
    float distancia;
    public Camera camarapersonaje;
    Vector3 originrc;
    Vector3 directionrc;
    public GameObject hud;




    SerialPort arduinoPort = new SerialPort("COM3",9600);


    void OnDisable(){
        arduinoPort.Close();

     }
    void Awake()
    {
        hud.SetActive(false);
        fuente = GetComponent<AudioSource>();
    }

    void Start(){
      arduinoPort.Open();
      arduinoPort.ReadTimeout=100;
    }


    // Update is called once per frame
    void Update()
    {

        hud.SetActive(false);
        RaycastHit hit;
        originrc = camarapersonaje.transform.position;
        directionrc = camarapersonaje.transform.forward;
        distancia = Vector3.Distance(this.transform.position, personaje.transform.position);

        if(Physics.Raycast(originrc,directionrc,out hit, 10f))
        {
            string name = hit.collider.gameObject.name;
            if (distancia <= 3f && name == this.name)
            {
                hud.SetActive(true);
                if (Input.GetKey(KeyCode.E)) {
                    fuente.Play();
                    vibeIn();
                    Invoke("vibeOut",2.5f);
                }

            }
            else
            {
                hud.SetActive(false);
            }
        }




    }
    public void vibeIn(){
              if(arduinoPort.IsOpen){
                 arduinoPort.Write("A");
              }
    }

      public void vibeOut(){
        if(arduinoPort.IsOpen){
          arduinoPort.Write("B");
        }


      }






    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//Color del rayo en el editor
        Gizmos.DrawRay(camarapersonaje.transform.position, camarapersonaje.transform.forward*10f);
    }
}
