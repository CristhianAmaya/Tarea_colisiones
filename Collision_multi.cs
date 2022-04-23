using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_multi : MonoBehaviour
{
    public Transform Sphere_1;
    public Transform Sphere_2;
    public Transform Sphere_3;
    public Transform Sphere_4;
    public GameObject Parent;
    private Vector3 velocidad_s1; //Velocidad para la esfera 1 (Roja)
    private Vector3 velocidad_s2; //Velocidad para la esfera 1 (Azul)
    private Vector3 velocidad_s3; //Velocidad para la esfera 1 (Amarillo)
    private Vector3 velocidad_s4; //Velocidad para la esfera 1 (Verde)
    private Vector3 posicion_s1; //Posición para la esfera 1 (Roja)
    private Vector3 posicion_s2; //Posición para la esfera 1 (Azul)
    private Vector3 posicion_s3; //Posición para la esfera 1 (Amarillo)
    private Vector3 posicion_s4; //Posición para la esfera 1 (Verde)
    float masa_s1 = 1.0f, masa_s2 = 1.0f;
    float e = 1.0f;
    float radio_s = 0.5f;
    // Start is called before the first frame update
    void Start()//getdistance.point
    {
        velocidad_s1 = new Vector3(0.5f, -0.2f, 0.0f);
        velocidad_s2 = new Vector3(-1.0f, -1.0f, 0.0f);
        velocidad_s3 = new Vector3(0.2f, 0.5f, 0.0f);
        velocidad_s4 = new Vector3(-1.0f, 0.0f, 0.0f);

        posicion_s1 = new Vector3(-4.0f, 2.0f, 0.0f);
        posicion_s2 = new Vector3(4.0f, 4.0f, 0.0f);
        posicion_s3 = new Vector3(-4.0f, -2.0f, 0.0f);
        posicion_s4 = new Vector3(4.0f, 0.0f, 0.0f);

        Sphere_1 = this.gameObject.transform.GetChild(0);
        Sphere_2 = this.gameObject.transform.GetChild(1);
        Sphere_3 = this.gameObject.transform.GetChild(3);
        Sphere_4 = this.gameObject.transform.GetChild(4);

        //Para hallar el ángulo se hace la integral de la velocidad tantop en x como en y siendo que queda así (x/2) y (y/2)
        //La formula es la siguiente arctan = (((s1.x/2)+(s2.x/2))/((s1.y/2)+(s2.y/2)))
        // angulo = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_s2.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_s2.y, 2)/2)));

        Sphere_1.position = new Vector3(posicion_s1.x, posicion_s1.y, posicion_s1.z);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, posicion_s2.z);
        Sphere_3.position = new Vector3(posicion_s3.x, posicion_s3.y, posicion_s3.z);
        Sphere_4.position = new Vector3(posicion_s4.x, posicion_s4.y, posicion_s4.z);

        Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
        Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));
        Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s3.x, velocidad_s3.y, velocidad_s3.z));
        Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s4.x, velocidad_s4.y, velocidad_s4.z));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float distancia12 = Mathf.Sqrt(Mathf.Pow(posicion_s2.x - posicion_s1.x, 2) + Mathf.Pow(posicion_s2.y - posicion_s1.y, 2));
        float distancia13 = Mathf.Sqrt(Mathf.Pow(posicion_s3.x - posicion_s1.x, 2) + Mathf.Pow(posicion_s3.y - posicion_s1.y, 2));
        float distancia14 = Mathf.Sqrt(Mathf.Pow(posicion_s4.x - posicion_s1.x, 2) + Mathf.Pow(posicion_s4.y - posicion_s1.y, 2));
        float distancia23 = Mathf.Sqrt(Mathf.Pow(posicion_s3.x - posicion_s2.x, 2) + Mathf.Pow(posicion_s3.y - posicion_s2.y, 2));
        float distancia24 = Mathf.Sqrt(Mathf.Pow(posicion_s4.x - posicion_s2.x, 2) + Mathf.Pow(posicion_s4.y - posicion_s2.y, 2));
        float distancia34 = Mathf.Sqrt(Mathf.Pow(posicion_s4.x - posicion_s3.x, 2) + Mathf.Pow(posicion_s4.y - posicion_s3.y, 2));
        float aux = 1.0f / (masa_s1 + masa_s2);
        float angulo12;
        float angulo13;
        float angulo14;
        float angulo23;
        float angulo24;
        float angulo34;
        
        //esfera roja contra azul (1,2)
        if (distancia12 <= 2.0f * radio_s) {
            angulo12 = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_s2.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_s2.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s1 * Mathf.Cos(angulo12) + velocidad_s1 * Mathf.Sin(angulo12);
            Vector3 vn1 = -(velocidad_s1 * Mathf.Sin(angulo12)) + (velocidad_s1 * Mathf.Cos(angulo12));
            //Para la esfera 2
            Vector3 vp2 = velocidad_s2 * Mathf.Cos(angulo12) + velocidad_s2 * Mathf.Sin(angulo12);
            Vector3 vn2 = -(velocidad_s2 * Mathf.Sin(angulo12)) + (velocidad_s2 * Mathf.Cos(angulo12));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s1.x = vp1_new.x * Mathf.Cos(angulo12) - vn1.x * Mathf.Sin(angulo12);
            velocidad_s1.y = vp1_new.y * Mathf.Sin(angulo12) + vn1.x * Mathf.Cos(angulo12);
            //Para la esfera 2
            velocidad_s2.x = vp2_new.x * Mathf.Cos(angulo12) - vn2.x * Mathf.Sin(angulo12);
            velocidad_s2.y = vp2_new.y * Mathf.Sin(angulo12) + vn2.x * Mathf.Cos(angulo12);


            //Otorgar nuevas velocidades
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));

            // vp1 = velocidad_s1 * Mathf.Cos(angulo) + velocidad_s1 * Mathf.Sin(angulo);
            // vp2 = velocidad_s2 * Mathf.Cos(angulo) + velocidad_s2 * Mathf.Sin(angulo);

            print("D: " + distancia12);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s2);
        }

        //Esfera Roja contra Amarilla (1,3)
        if (distancia13 <= 2.0f * radio_s) {
            angulo13 = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_s3.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_s3.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s1 * Mathf.Cos(angulo13) + velocidad_s1 * Mathf.Sin(angulo13);
            Vector3 vn1 = -(velocidad_s1 * Mathf.Sin(angulo13)) + (velocidad_s1 * Mathf.Cos(angulo13));
            //Para la esfera 3
            Vector3 vp2 = velocidad_s3 * Mathf.Cos(angulo13) + velocidad_s3 * Mathf.Sin(angulo13);
            Vector3 vn2 = -(velocidad_s3 * Mathf.Sin(angulo13)) + (velocidad_s3 * Mathf.Cos(angulo13));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s1.x = vp1_new.x * Mathf.Cos(angulo13) - vn1.x * Mathf.Sin(angulo13);
            velocidad_s1.y = vp1_new.y * Mathf.Sin(angulo13) + vn1.x * Mathf.Cos(angulo13);
            //Para la esfera 3
            velocidad_s3.x = vp2_new.x * Mathf.Cos(angulo13) - vn2.x * Mathf.Sin(angulo13);
            velocidad_s3.y = vp2_new.y * Mathf.Sin(angulo13) + vn2.x * Mathf.Cos(angulo13);


            //Otorgar nuevas velocidades
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
            Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s3.x, velocidad_s3.y, velocidad_s3.z));

            print("D: " + distancia13);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s3);
        }

        //Esfera Roja contra Verde (1,4)
        if (distancia14 <= 2.0f * radio_s) {
            angulo14 = Mathf.Atan(((Mathf.Pow(posicion_s1.x, 2)/2)+(Mathf.Pow(posicion_s4.x, 2)/2))/((Mathf.Pow(posicion_s1.y, 2)/2)+(Mathf.Pow(posicion_s4.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s1 * Mathf.Cos(angulo14) + velocidad_s1 * Mathf.Sin(angulo14);
            Vector3 vn1 = -(velocidad_s1 * Mathf.Sin(angulo14)) + (velocidad_s1 * Mathf.Cos(angulo14));
            //Para la esfera 3
            Vector3 vp2 = velocidad_s4 * Mathf.Cos(angulo14) + velocidad_s4 * Mathf.Sin(angulo14);
            Vector3 vn2 = -(velocidad_s4 * Mathf.Sin(angulo14)) + (velocidad_s4 * Mathf.Cos(angulo14));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s1.x = vp1_new.x * Mathf.Cos(angulo14) - vn1.x * Mathf.Sin(angulo14);
            velocidad_s1.y = vp1_new.y * Mathf.Sin(angulo14) + vn1.x * Mathf.Cos(angulo14);
            //Para la esfera 3
            velocidad_s4.x = vp2_new.x * Mathf.Cos(angulo14) - vn2.x * Mathf.Sin(angulo14);
            velocidad_s4.y = vp2_new.y * Mathf.Sin(angulo14) + vn2.x * Mathf.Cos(angulo14);


            //Otorgar nuevas velocidades
            Sphere_1.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s1.x, velocidad_s1.y, velocidad_s1.z));
            Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s4.x, velocidad_s4.y, velocidad_s4.z));

            print("D: " + distancia14);
            print("V1: " + velocidad_s1);
            print("V2: " + velocidad_s4);
        }

        //Esfera Azul contra Amarilla (2,3)
        if (distancia23 <= 2.0f * radio_s) {
            angulo23 = Mathf.Atan(((Mathf.Pow(posicion_s2.x, 2)/2)+(Mathf.Pow(posicion_s3.x, 2)/2))/((Mathf.Pow(posicion_s2.y, 2)/2)+(Mathf.Pow(posicion_s3.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s2 * Mathf.Cos(angulo23) + velocidad_s2 * Mathf.Sin(angulo23);
            Vector3 vn1 = -(velocidad_s2 * Mathf.Sin(angulo23)) + (velocidad_s2 * Mathf.Cos(angulo23));
            //Para la esfera 3
            Vector3 vp2 = velocidad_s3 * Mathf.Cos(angulo23) + velocidad_s3 * Mathf.Sin(angulo23);
            Vector3 vn2 = -(velocidad_s3 * Mathf.Sin(angulo23)) + (velocidad_s3 * Mathf.Cos(angulo23));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s2.x = vp1_new.x * Mathf.Cos(angulo23) - vn1.x * Mathf.Sin(angulo23);
            velocidad_s2.y = vp1_new.y * Mathf.Sin(angulo23) + vn1.x * Mathf.Cos(angulo23);
            //Para la esfera 3
            velocidad_s3.x = vp2_new.x * Mathf.Cos(angulo23) - vn2.x * Mathf.Sin(angulo23);
            velocidad_s3.y = vp2_new.y * Mathf.Sin(angulo23) + vn2.x * Mathf.Cos(angulo23);


            //Otorgar nuevas velocidades
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));
            Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s3.x, velocidad_s3.y, velocidad_s3.z));

            print("D: " + distancia23);
            print("V1: " + velocidad_s2);
            print("V2: " + velocidad_s3);
        }

        //Esfera Azul contra Verde (2,4)
        if (distancia24 <= 2.0f * radio_s) {
            angulo24 = Mathf.Atan(((Mathf.Pow(posicion_s2.x, 2)/2)+(Mathf.Pow(posicion_s4.x, 2)/2))/((Mathf.Pow(posicion_s2.y, 2)/2)+(Mathf.Pow(posicion_s4.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s2 * Mathf.Cos(angulo24) + velocidad_s2 * Mathf.Sin(angulo24);
            Vector3 vn1 = -(velocidad_s2 * Mathf.Sin(angulo24)) + (velocidad_s2 * Mathf.Cos(angulo24));
            //Para la esfera 3
            Vector3 vp2 = velocidad_s4 * Mathf.Cos(angulo24) + velocidad_s4 * Mathf.Sin(angulo24);
            Vector3 vn2 = -(velocidad_s4 * Mathf.Sin(angulo24)) + (velocidad_s4 * Mathf.Cos(angulo24));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s2.x = vp1_new.x * Mathf.Cos(angulo24) - vn1.x * Mathf.Sin(angulo24);
            velocidad_s2.y = vp1_new.y * Mathf.Sin(angulo24) + vn1.x * Mathf.Cos(angulo24);
            //Para la esfera 3
            velocidad_s4.x = vp2_new.x * Mathf.Cos(angulo24) - vn2.x * Mathf.Sin(angulo24);
            velocidad_s4.y = vp2_new.y * Mathf.Sin(angulo24) + vn2.x * Mathf.Cos(angulo24);


            //Otorgar nuevas velocidades
            Sphere_2.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s2.x, velocidad_s2.y, velocidad_s2.z));
            Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s4.x, velocidad_s4.y, velocidad_s4.z));

            print("D: " + distancia24);
            print("V1: " + velocidad_s2);
            print("V2: " + velocidad_s4);
        }

        //Esfera Amarilla contra Verde (3,4)
        if (distancia34 <= 2.0f * radio_s) {
            angulo34 = Mathf.Atan(((Mathf.Pow(posicion_s3.x, 2)/2)+(Mathf.Pow(posicion_s4.x, 2)/2))/((Mathf.Pow(posicion_s3.y, 2)/2)+(Mathf.Pow(posicion_s4.y, 2)/2)));
            //Para la esfera 1
            Vector3 vp1 = velocidad_s3 * Mathf.Cos(angulo34) + velocidad_s3 * Mathf.Sin(angulo34);
            Vector3 vn1 = -(velocidad_s3 * Mathf.Sin(angulo34)) + (velocidad_s3 * Mathf.Cos(angulo34));
            //Para la esfera 3
            Vector3 vp2 = velocidad_s4 * Mathf.Cos(angulo34) + velocidad_s4 * Mathf.Sin(angulo34);
            Vector3 vn2 = -(velocidad_s4 * Mathf.Sin(angulo34)) + (velocidad_s4 * Mathf.Cos(angulo34));

            //----------------------------------------------------------------------------------------//

            //calculo el vp1 new y el vp2 new
            Vector3 vp1_new = (masa_s1 - e * masa_s2) * vp1 * aux + (1.0f + e) * masa_s2 * vp2 * aux;
            Vector3 vp2_new = (1.0f + e) * masa_s1 * vp1 * aux + (masa_s2 - e * masa_s1) * vp2 * aux;

            //----------------------------------------------------------------------------------------//

            //Rotación inversa del eje de referencia
            //Para la esfera 1
            velocidad_s3.x = vp1_new.x * Mathf.Cos(angulo34) - vn1.x * Mathf.Sin(angulo34);
            velocidad_s3.y = vp1_new.y * Mathf.Sin(angulo34) + vn1.x * Mathf.Cos(angulo34);
            //Para la esfera 3
            velocidad_s4.x = vp2_new.x * Mathf.Cos(angulo34) - vn2.x * Mathf.Sin(angulo34);
            velocidad_s4.y = vp2_new.y * Mathf.Sin(angulo34) + vn2.x * Mathf.Cos(angulo34);


            //Otorgar nuevas velocidades
            Sphere_3.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s3.x, velocidad_s3.y, velocidad_s3.z));
            Sphere_4.GetComponent<Sphere>().setVelocidad(new Vector3(velocidad_s4.x, velocidad_s4.y, velocidad_s4.z));

            print("D: " + distancia34);
            print("V1: " + velocidad_s3);
            print("V2: " + velocidad_s4);
        }

        posicion_s1 = posicion_s1 + Time.deltaTime * velocidad_s1;
        posicion_s2 = posicion_s2 + Time.deltaTime * velocidad_s2;
        posicion_s3 = posicion_s3 + Time.deltaTime * velocidad_s3;
        posicion_s4 = posicion_s4 + Time.deltaTime * velocidad_s4;

        Sphere_1.position = new Vector3 (posicion_s1.x, posicion_s1.y, 0.0f);
        Sphere_2.position = new Vector3(posicion_s2.x, posicion_s2.y, 0.0f);
        Sphere_3.position = new Vector3 (posicion_s3.x, posicion_s3.y, 0.0f);
        Sphere_4.position = new Vector3(posicion_s4.x, posicion_s4.y, 0.0f);
    }
}
