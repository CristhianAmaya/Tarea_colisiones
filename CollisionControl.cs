using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    public Transform Sphere_1;
    public Transform Sphere_2;
    public GameObject Parent;
    
    float velocidad_s1 = 0.5f, velocidad_s2 = -0.5f;
    float posicion_s1 = 0.0f, posicion_s2 = 3.75f;
    float masa_s1 = 1.0f, masa_s2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;

    void Start() {
        Sphere_1 = this.gameObject.transform.GetChild(0);
        Sphere_2 = this.gameObject.transform.GetChild(1);

        Sphere_1.position = new Vector3(posicion_s1, 0, 0);
        Sphere_2.position = new Vector3(posicion_s2, 0, 0);
        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1, 0, 0));
        Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2, 0, 0));
    }

    void Update() {
        float distancia = Mathf.Abs(Sphere_1.position.x - Sphere_2.position.x);
        float aux = 1.0f / (masa_s1 + masa_s2);
        Vector3 vel1 = Sphere_1.GetComponent<Sphere>().getVelocidad();
        Vector3 vel2 = Sphere_2.GetComponent<Sphere>().getVelocidad();

        
        if (distancia <= 2.0 * radio_s) {
            velocidad_s1 = (masa_s1 - e * masa_s2) * vel1.x * aux + (1.0f + e) * masa_s2 * vel2.x * aux;
            velocidad_s2 = (1.0f + e) * masa_s1 * vel1.x * aux + (masa_s2 - e * masa_s1) * vel2.x * aux;

            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1, 0, 0));
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2, 0, 0));

            print("D: " + distancia);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s2);
        }

        posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;
        posicion_s2 = posicion_s2 + Time.deltaTime * velocidad_s2;

        Sphere_1.position = new Vector3 (posicion_s1 + Time.deltaTime * velocidad_s1, 0, 0);
        Sphere_2.position = new Vector3(posicion_s2 + Time.deltaTime * velocidad_s2, 0, 0);
    }
}