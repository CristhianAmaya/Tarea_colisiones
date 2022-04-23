using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionXYZ : MonoBehaviour
{
    public Transform Sphere_1;
    public Transform Sphere_2;
    public GameObject Parent;
    private Vector3 velocidad_s1; //Velocidad para la esfera 1 (Roja)
    private Vector3 velocidad_s2; //Velocidad para la esfera 1 (Azul)
    private Vector3 posicion_s1; //Posición para la esfera 1 (Roja)
    private Vector3 posicion_s2; //Posición para la esfera 1 (Azul)
    float angulo;
    float masa_s1 = 1.0f, masa_s2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        velocidad_s1 = new Vector3(0.5f, 0.5f, 0.5f);
        velocidad_s2 = new Vector3(-2.0f, -2.0f, -2.0f);
        posicion_s1 = new Vector3(1.0f, 0.0f, 1.0f);
        posicion_s2 = new Vector3(3.75f, 3.75f, 3.75f);

        Sphere_1 = this.gameObject.transform.GetChild(0);
        Sphere_2 = this.gameObject.transform.GetChild(1);

        //Para hallar el ángulo se hace la integral de la velocidad tantop en x como en y siendo que queda así (x/2) y (y/2)
        //La formula es la siguiente arctan = (((s1.x/2)+(s2.x/2))/((s1.y/2)+(s2.y/2)))
        // angulo = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_s2.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_s2.y, 2)/2)));

        Sphere_1.position = new Vector3(posicion_s1.x, posicion_s1.y, posicion_s1.z);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, posicion_s2.z);
        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
        Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distancia = Mathf.Sqrt(Mathf.Pow(posicion_s2.x - posicion_s1.x, 2) + Mathf.Pow(posicion_s2.y - posicion_s1.y, 2) + Mathf.Pow(posicion_s2.z - posicion_s1.z, 2));
        float aux = 1.0f / (masa_s1 + masa_s2);

        //Producto punto
        float prod_punto = (posicion_s1.x * posicion_s2.x) + (posicion_s1.y * posicion_s2.y) + (posicion_s1.z * posicion_s2.z);
        //Norma de los dos vectores posicion_s1 (|u|) y posicion_s2 (|v|)
        float norma_s1 = Mathf.Sqrt(Mathf.Pow(posicion_s1.x, 2) + Mathf.Pow(posicion_s1.y, 2) + Mathf.Pow(posicion_s1.z, 2));
        float norma_s2 = Mathf.Sqrt(Mathf.Pow(posicion_s2.x, 2) + Mathf.Pow(posicion_s2.y, 2) + Mathf.Pow(posicion_s2.z, 2));
        
        if (distancia <= 2.0f * radio_s) {
            angulo = Mathf.Acos(prod_punto / (norma_s1 * norma_s2));
            //Para la esfera 1
            Vector3 vp1 = (velocidad_s1 * Mathf.Cos(angulo) * Mathf.Cos(angulo)) + (velocidad_s1 * Mathf.Cos(angulo)* Mathf.Sin(angulo)) + (velocidad_s1 * -Mathf.Sin(angulo));
            Vector3 vn1 = (velocidad_s1 * (-Mathf.Cos(angulo) * Mathf.Sin(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Cos(angulo))) + (velocidad_s1 * (Mathf.Cos(angulo) * Mathf.Cos(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Sin(angulo))) + (velocidad_s1 * (Mathf.Cos(angulo) * Mathf.Sin(angulo)));
            Vector3 vk1 = (velocidad_s1 * (-Mathf.Cos(angulo) * Mathf.Sin(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Cos(angulo))) + (velocidad_s1 * (Mathf.Cos(angulo) * Mathf.Cos(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Sin(angulo))) + (velocidad_s1 * (Mathf.Cos(angulo) * Mathf.Sin(angulo)));
            //Para la esfera 2
            Vector3 vp2 = (velocidad_s2 * Mathf.Cos(angulo) * Mathf.Cos(angulo)) + (velocidad_s2 * Mathf.Cos(angulo)* Mathf.Sin(angulo)) + (velocidad_s2 * -Mathf.Sin(angulo));
            Vector3 vn2 = (velocidad_s2 * (-Mathf.Cos(angulo) * Mathf.Sin(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Cos(angulo))) + (velocidad_s2 * (Mathf.Cos(angulo) * Mathf.Cos(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Sin(angulo))) + (velocidad_s2 * (Mathf.Cos(angulo) * Mathf.Sin(angulo)));
            Vector3 vk2 = (velocidad_s2 * (-Mathf.Cos(angulo) * Mathf.Sin(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Cos(angulo))) + (velocidad_s2 * (Mathf.Cos(angulo) * Mathf.Cos(angulo) + Mathf.Sin(angulo) * Mathf.Sin(angulo) * Mathf.Sin(angulo))) + (velocidad_s2 * (Mathf.Cos(angulo) * Mathf.Sin(angulo)));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s1.x = vp1_new.x * Mathf.Cos(angulo) - vn1.x * Mathf.Sin(angulo);
            velocidad_s1.y = vp1_new.y * Mathf.Sin(angulo) + vn1.x * Mathf.Cos(angulo);
            velocidad_s1.z = vp1_new.z * Mathf.Tan(angulo) + vn1.x * Mathf.Cos(angulo);
            //Para la esfera 2
            velocidad_s2.x = vp2_new.x * Mathf.Cos(angulo) - vn2.x * Mathf.Sin(angulo);
            velocidad_s2.y = vp2_new.y * Mathf.Sin(angulo) + vn2.x * Mathf.Cos(angulo);
            velocidad_s2.z = vp2_new.z * Mathf.Tan(angulo) + vn2.x * Mathf.Cos(angulo);


            //Otorgar nuevas velocidades
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));

            // vp1 = velocidad_s1 * Mathf.Cos(angulo) + velocidad_s1 * Mathf.Sin(angulo);
            // vp2 = velocidad_s2 * Mathf.Cos(angulo) + velocidad_s2 * Mathf.Sin(angulo);

            print("D: " + distancia);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s2);
        }

        posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;
        posicion_s2 = posicion_s2 + Time.deltaTime * velocidad_s2;

        // posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;
        // posicion_s2 = posicion_s2 + Time.deltaTime * velocidad_s2;

        Sphere_1.position = new Vector3 (posicion_s1.x, posicion_s1.y, posicion_s1.z);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, posicion_s2.z);
    }
}
