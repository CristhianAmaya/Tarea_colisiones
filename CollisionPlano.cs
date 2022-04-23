using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlano : MonoBehaviour
{
    public Transform Sphere_1;
    public Transform plane;
    public GameObject Parent;
    private Vector3 velocidad_s1; //Velocidad para la esfera 1 (Roja)
    private Vector3 posicion_s1; //Posición para la esfera 1 (Roja)
    private Vector3 posicion_P; //Posición para el plano
    float angulo;
    float e = 1.0f;
    float radio_s = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        velocidad_s1 = new Vector3(2.0f, 0.0f, 0.0f);
        posicion_s1 = new Vector3(1.0f, 0.0f, 0.0f);
        posicion_P = new Vector3(6.0f, 0.0f, 0.0f);

        Sphere_1 = this.gameObject.transform.GetChild(0);
        plane = this.gameObject.transform.GetChild(0);

        //Para hallar el ángulo se hace la integral de la velocidad tantop en x como en y siendo que queda así (x/2) y (y/2)
        //La formula es la siguiente arctan = (((s1.x/2)+(s2.x/2))/((s1.y/2)+(s2.y/2)))
        // angulo = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_P.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_P.y, 2)/2)));

        Sphere_1.position = new Vector3(posicion_s1.x, posicion_s1.y, posicion_s1.z);
        plane.position = new Vector3(posicion_P.x, posicion_P.y, posicion_P.z);
        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //plano.SetNormalAndPosition(Vector3.forward, Vector3.forward * fieldLength / 2);


        float distancia = Mathf.Sqrt(Mathf.Pow(posicion_P.x - posicion_s1.x, 2) + Mathf.Pow(posicion_P.y - posicion_s1.y, 2));
        Vector3 vel1 = Sphere_1.GetComponent<Sphere>().getVelocidad();
        
        if (distancia <= radio_s) {
            velocidad_s1 = -e * vel1;

            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));

            print("D: " + distancia);
            print("V1: " + velocidad_s1);
        }

        posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;

        Sphere_1.position = new Vector3 (posicion_s1.x, posicion_s1.y, 0.0f);
    }
}
