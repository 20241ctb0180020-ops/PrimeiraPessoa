using UnityEngine;

public class Distancia : MonoBehaviour
{
    public GameObject golfinho;
    public GameObject musica;
    public GameObject Particulas;
    public Transform objetoA;
    public Transform objetoB;

    void Start()
    {

    }

    void Update()
    {
        // Calcula a distância em linha reta entre os dois objetos
        float distancia = Vector3.Distance(objetoA.position, objetoB.position);
        if (distancia <= 25)
        {
            musica.SetActive(true);
            Particulas.SetActive(true);
            golfinho.SetActive(true);
        }

        Debug.Log("A distância é: " + distancia);
    }

}
