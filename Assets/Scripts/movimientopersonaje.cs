using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientopersonaje : MonoBehaviour
{
    public Camera camarafps;

    public float velocidadh;
    public float velocidadv;

    float h;
    float v;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento de la cámara
        h = velocidadh * Input.GetAxis("Mouse X");
        v = velocidadv * Input.GetAxis("Mouse Y");

        transform.Rotate(0, h, 0);
        camarafps.transform.Rotate(-v, 0, 0);


        //Movimiento del personaje
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 0.02f);
        }
        else
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, 0, -0.02f);
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(-0.02f, 0, 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.Translate(0.02f, 0, 0);
                    }
                }
            }

        }
    }
}
