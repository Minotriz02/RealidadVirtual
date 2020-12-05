using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using TMPro;

public class Dialogo : MonoBehaviour
{
    //public AudioClip dialogo;
    //public AudioSource fuente;
    public GameObject Aldeano1;
    public GameObject Aldeano2;
    public GameObject Aldeano3;
    public GameObject Mom;
    float distanciaA1;
    float distanciaA2;
    float distanciaA3;
    float distanciaMom;
    public Camera camarapersonaje;
    Vector3 originrc;
    Vector3 directionrc;
    public GameObject hud;
    public TextMeshProUGUI subtitle;
    bool isTalking;
    bool ismothered;

    //TextMeshProUGUI subtitle;

    SerialPort arduinoPort = new SerialPort("COM3",9600);


    void OnApplicationQuit ()
    {

        if ( arduinoPort != null )
        {
            arduinoPort.Close();
        }
    }


    void Awake()
    {
        //subtitlego.SetActive(false);
        hud.SetActive(false);
        //subtitle = null;
        //fuente = GetComponent<AudioSource>();

        subtitle.SetText("");
    }

    void Start(){


      if ( arduinoPort != null )
        {
            if ( arduinoPort.IsOpen ) // close if already open
            {
                arduinoPort.Close();
                Debug.Log ("Closed stream");
            }

            arduinoPort.Open();
            Debug.Log ("Opened stream");
        }
        else
        {
            Debug.Log ("ERROR: Uninitialized stream");
        }



        arduinoPort.ReadTimeout=100;
        ismothered = false;
        isTalking = false;
        subtitle = GetComponent<TextMeshProUGUI>();

    }


    // Update is called once per frame
    void Update()
    {

        //hud.SetActive(false);
        RaycastHit hit;
        originrc = camarapersonaje.transform.position;
        directionrc = camarapersonaje.transform.forward;
        distanciaA1 = Vector3.Distance(Aldeano1.transform.position, this.transform.position);
        distanciaA2 = Vector3.Distance(Aldeano2.transform.position, this.transform.position);
        distanciaA3 = Vector3.Distance(Aldeano3.transform.position, this.transform.position);
        distanciaMom = Vector3.Distance(Mom.transform.position, this.transform.position);

        if (Physics.Raycast(originrc,directionrc,out hit, 10f) && isTalking == false)
        {

            string name = hit.collider.gameObject.name;
            Debug.Log(name);
            if (distanciaMom <= 3f && name.Equals("Mom") )
            {

                hud.SetActive(true);
                if (Input.GetKey(KeyCode.E)) {
                    ismothered = true;
                    isTalking = true;
                    hud.SetActive(false);
                    subtitle.SetText("Tú: Hola madre, nombre de Dios. ¿como esta?");
                    StartCoroutine(MomAnswer1());
                    vibeIn();
                    Invoke("vibeOut",17.3f);
                    Invoke("Hud", 17.3f);
                }

            }
            else if (distanciaA1 <= 3f && name.Equals("Aldeano1"))
            {
                hud.SetActive(true);
                if (ismothered)
                {

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
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isTalking = true;
                        hud.SetActive(false);
                        subtitle.SetText("Don Fabio: Mijo, su mamá lo anda buscando");
                        Invoke("Hud", 2.5f);
                    }
                }
            }
            else if (distanciaA2 <= 3f && name.Equals("Aldeano2"))
            {
                hud.SetActive(true);
                if (ismothered)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isTalking = true;
                        hud.SetActive(false);
                        subtitle.SetText("Tú: Buenas vecina, ¿usted no ha visto a mi hermano pasar por acá? Es que hace como dos semanas que no aparece");
                        StartCoroutine(Aldeano2Answer1());
                        vibeIn();
                        Invoke("vibeOut", 9.6f);
                        Invoke("Hud", 9.6f);

                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isTalking = true;
                        hud.SetActive(false);
                        subtitle.SetText("Vecina: Mijo, su mamá lo anda buscando");
                        Invoke("Hud", 2.5f);
                    }
                }

            }
            else if (distanciaA3 <= 3f && name.Equals("Aldeano3"))
            {
                hud.SetActive(true);
                if (ismothered)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isTalking = true;
                        hud.SetActive(false);
                        subtitle.SetText("Tú: Hablame. Ve, ¿No has visto a mi hermano por acá? Mi mamá ya anda toda asarada.");
                        StartCoroutine(Aldeano3Answer1());
                        vibeIn();
                        Invoke("vibeOut", 16f);
                        Invoke("Hud", 16f);

                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        isTalking = true;
                        hud.SetActive(false);
                        subtitle.SetText("Vecino: Parcero, su mamá lo anda buscando");
                        Invoke("Hud", 2.5f);
                    }
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

        if (arduinoPort.IsOpen){
          arduinoPort.Write("B");
        }

     }


    private void Hud()
    {
        isTalking = false;
        //hud.SetActive(true);
        subtitle.SetText("");
    }


    private IEnumerator UsertoMom()
    {
        yield return new WaitForSeconds(9.2f);
        subtitle.SetText("Tú: ¿Cómo así ma’? Déjeme averiguo por aca a ver.");
        StartCoroutine(MomAnswer2());

    }

    private IEnumerator MomAnswer1()
    {
        yield return new WaitForSeconds(2.8f);
        subtitle.SetText("Mamá: Dios lo bendiga mijo. Pues aquí más preocupada porque su hermano nada que aparece, van dos semanas que no viene y lo último que supe es que se fue a un pueblo que a cobrar una plata.");
        StartCoroutine(UsertoMom());
    }

    private IEnumerator MomAnswer2()
    {
        yield return new WaitForSeconds(3.2f);
        subtitle.SetText("Madre: Bueno mijo, se cuida mucho que por acá las cosas están como feas.");

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
        subtitle.SetText("Tú: Ay don Fabio, no me diga eso. Voy a seguir preguntando, gracias.");

    }

    private IEnumerator Aldeano2Answer1()
    {
        yield return new WaitForSeconds(5f);
        subtitle.SetText("Vecina: No hijo, yo a su hermano no lo he visto hace uh.");
        StartCoroutine(UsertoAldeano2());
    }

    private IEnumerator UsertoAldeano2()
    {
        yield return new WaitForSeconds(3f);
        subtitle.SetText("Tú: Muchas gracias vecina, cualquier cosa me avisa.");
    }

    private IEnumerator Aldeano3Answer1()
    {
        yield return new WaitForSeconds(4.0f);
        subtitle.SetText("Vecino: No papi, yo a su hermano no lo veo desde que me dijo que iba a cobrar una plata pero yo se quien le puede tener ese dato; vea, usted sale del pueblo y allá hay una casa con un murito como verde, allí hay unos supuestos paramilitares, pregúntele a alguno de ellos a ver si le colaboran.");
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;//Color del rayo en el editor
        Gizmos.DrawRay(camarapersonaje.transform.position, camarapersonaje.transform.forward*10f);
    }
}
