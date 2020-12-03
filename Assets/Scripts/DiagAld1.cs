using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using TMPro;

public class DiagAld1 : MonoBehaviour
{
    //public AudioClip dialogo;
    //public AudioSource fuente;
    public GameObject personaje;
    float distancia;
    public Camera camarapersonaje;
    Vector3 originrc;
    Vector3 directionrc;
    public GameObject hud;
    public TextMeshProUGUI subtitle;
    bool isTalking;
    bool hasfinished;

    //TextMeshProUGUI subtitle;

    SerialPort arduinoPort = new SerialPort("COM3", 9600);


    void OnDisable()
    {
        arduinoPort.Close();

    }
    void Awake()
    {
        //subtitlego.SetActive(false);
        hud.SetActive(false);
        //subtitle = null;
        //fuente = GetComponent<AudioSource>();

        subtitle.SetText("");
    }

    void Start()
    {

        arduinoPort.Open();
        arduinoPort.ReadTimeout = 100;
        hasfinished = false;
        isTalking = false;
        subtitle = GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {

        hud.SetActive(false);
        RaycastHit hit;
        originrc = camarapersonaje.transform.position;
        directionrc = camarapersonaje.transform.forward;
        distancia = Vector3.Distance(this.transform.position, personaje.transform.position);

        if (Physics.Raycast(originrc, directionrc, out hit, 10f) && isTalking == false)
        {

            string name = hit.collider.gameObject.name;
            if (distancia <= 3f && name == "Aldeano1")
            {

                hud.SetActive(true);
                if (Input.GetKey(KeyCode.E))
                {
                    isTalking = true;
                    hud.SetActive(false);
                    subtitle.SetText("Tú: Buenas don Fabio, venga es que le iba a preguntar, ¿usted de pronto no ha visto a mi hermano por acá?");
                    StartCoroutine(AldeanoAnswer1());
                    vibeIn();
                    Invoke("vibeOut", 11.5f);
                    Invoke("Hud", 11.5f);

                }

            }
        }




    }
    public void vibeIn()
    {
        if (arduinoPort.IsOpen)
        {
            arduinoPort.Write("A");
        }
    }

    public void vibeOut()
    {
        if (arduinoPort.IsOpen)
        {
            arduinoPort.Write("B");
        }


    }


    private void Hud()
    {
        isTalking = false;
        hud.SetActive(true);
        subtitle.SetText("");

    }

    private void CleanSubtitle()
    {
        subtitle.SetText("");
    }

    private IEnumerator AldeanoAnswer1()
    {
        yield return new WaitForSeconds(4.3f);
        subtitle.SetText("Don Fabio: No, sardino; yo por acá no lo he visto. Por lo que escuche la guerrilla está reclutando una gente. ¿No habrá sido eso?");
        StartCoroutine(UsertoAldeano1());
    }

    private IEnumerator UsertoAldeano1()
    {
        yield return new WaitForSeconds(5.2f);
        hasfinished = true;
        subtitle.SetText("Tú: Ay don Fabio, no me diga eso. Voy a seguir preguntando, gracias.");

    }

    




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//Color del rayo en el editor
        Gizmos.DrawRay(camarapersonaje.transform.position, camarapersonaje.transform.forward * 10f);
    }
}
