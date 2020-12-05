using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEsc : MonoBehaviour
{
    // Start is called before the first frame update


    public void cambiarescena(string nombredeescena)
{

    SceneManager.LoadScene(nombredeescena);
}
}
