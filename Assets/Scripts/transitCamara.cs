using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;


public class transitCamara : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5f;
    float distanceTravelled;
    public Camera camP;
    public TextMeshProUGUI subtitle;


    private void Awake()
    {
        camP.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        subtitle.SetText("Este es Tibú, un pueblo azotado por la guerrilla y los narco-paramilitares, donde se busca ganar las rutas " +
        "mas estrategicas para el narcotrafico. En esta experiencia encarnaras a alguien que vuelve a su pueblo natal donde " +
        "vive su familia, pero te encontraras con una terrible noticia.");
    }
    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
 
        StartCoroutine(CambiarCamara());
    }

    IEnumerator CambiarCamara() {
        yield return new WaitForSeconds(20f);
        gameObject.SetActive(false);
        subtitle.SetText("");
        camP.gameObject.SetActive(true);
    }
}
