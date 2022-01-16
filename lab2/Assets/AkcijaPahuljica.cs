using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkcijaPahuljica : MonoBehaviour
{

    GameObject kamera;
    GameObject oblak;
    float brzinaKamere = 5;
    Cestica[] cestice = new Cestica[100]; 

    public class Cestica
    {

        Vector3 pozicija;
        Vector3 smjer;
        float brzina;
        float starost;
        float duljinaZivota;

        GameObject cestica;

        public Cestica(Vector3 pozicija, Vector3 smjer, float brzina, float starost, float duljinaZivota)
        {
            this.pozicija = pozicija;
            this.smjer = smjer;
            this.brzina = brzina;
            this.starost = starost;
            this.duljinaZivota = duljinaZivota;
            stvoriCesticu();
        }

        public void stvoriCesticu()
        {

            cestica = GameObject.CreatePrimitive(PrimitiveType.Quad);
            Renderer r = cestica.GetComponent<Renderer>();
            string p = "snow";
            Texture2D tekstura = Resources.Load<Texture2D>(p);

            r.material.mainTexture = tekstura;
            r.material.shader = Shader.Find("Mobile/Particles/Additive");
            cestica.transform.position = pozicija;
            Destroy(cestica, duljinaZivota);

        }

        public bool ziva()
        {
            return cestica != null;
        }

        public void pomakni()
        {
            cestica.transform.Translate(smjer * brzina);
        }

        public void lookAt(Transform t, Vector3 w)
        {
            cestica.transform.LookAt(t, w);
        }

        public void smanji()
        {
            cestica.transform.localScale = new Vector3(Mathf.Sin(starost), Mathf.Sin(starost), Mathf.Sin(starost));
        }

        public void ostari()
        {
            starost += Time.deltaTime;
        }
    }


    float radijus = 20f;
    Vector3 centar;
    float kut;

    // Start is called before the first frame update
    void Start()
    {
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        oblak = GameObject.FindGameObjectWithTag("Oblak");
        centar = oblak.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 17) < 1)
        {
            for (int i = 0; i < cestice.Length; i++)
            {
                if (cestice[i] == null || !cestice[i].ziva())
                {

                    Vector3 pozicija = new Vector3(oblak.transform.position.x + Random.Range(-10, 10),
                        oblak.transform.position.y,
                        oblak.transform.position.z + Random.Range(-10, 10));

                    Vector3 smjer = new Vector3(0, -1, 0);
                    float brzina = 0.01f;
                    float starost = 0;
                    float duljinaZivota = Random.Range(3,5);

                    cestice[i] = new Cestica(pozicija, smjer, brzina, starost, duljinaZivota);

                    break;
                }
            }
        }
        

        foreach(Cestica c in cestice)
        {
            if (c == null || !c.ziva())
                continue;
            c.pomakni();
            c.smanji();
            c.ostari();

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            kamera.transform.Translate(new Vector3(brzinaKamere * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            kamera.transform.Translate(new Vector3(-brzinaKamere * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            kamera.transform.Translate(new Vector3(0, -brzinaKamere * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            kamera.transform.Translate(new Vector3(0, brzinaKamere * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            kamera.transform.Translate(new Vector3(0, 0, brzinaKamere * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            kamera.transform.Translate(new Vector3(0, 0, -brzinaKamere * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Keypad7))
        {
            kamera.transform.Rotate(new Vector3(0, 1, 0), -brzinaKamere *4* Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Keypad9))
        {
            kamera.transform.Rotate(new Vector3(0, 1, 0), +brzinaKamere *4* Time.deltaTime);
        }

        foreach(Cestica c in cestice)
        {
            if (c == null || !c.ziva())
                continue;
            Transform t = kamera.transform;
            Vector3 w = new Vector3(0, 1, 0);
            c.lookAt(t, w);

        }

        kut += brzinaKamere * 0.01f * Time.deltaTime;
        Vector3 pomak = new Vector3(Mathf.Sin(kut), 0, Mathf.Cos(kut)) * radijus;
        oblak.transform.position = centar + pomak;
    }
}
