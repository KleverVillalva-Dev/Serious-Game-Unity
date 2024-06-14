using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador_Comportamiento : MonoBehaviour
{
    private float ejeX = 0f;
    private float ejeZ = 0f;
    
    private Animator animator;
    public Joystick joystick;
    public Button disparar;
    public ParticleSystem particulas;
    private Rigidbody rb;

    [Header("Variables jugabilidad")]
    [SerializeField] public float cDLanzamiento;
    public float cd;
    [SerializeField] private float velocidad;
    [SerializeField] GameObject matrazArma;
    [SerializeField] Transform puntoLanzamiento;
    [SerializeField] float fuerzaLanzamiento;

    [SerializeField] public int vidaMaxima;
    public int vidaActual;
    public bool muerte;
    string scene;

    private void Awake()
    {
        cd = cDLanzamiento;
    }

    private void Start()
    {
        scene =  SceneManager.GetActiveScene().name;
        cDLanzamiento = 0f;
        vidaActual = vidaMaxima;
        rb = GetComponent<Rigidbody>();
        disparar.onClick.AddListener(Disparar);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Moverse();
    }

    private void Update()
    {
        if (vidaActual <= 0 && !muerte)
        {
            muerte = true;
            animator.SetTrigger("Muerte");
            AudioManager.instance.ReproducirSonido(AudioManager.instance.sfx_muertePersonaje);
            //Guardar Datos en gameManager
            if (scene == "Nivel_1_Conceptos")
            {
                GameManager.instance.concepos_VirusEliminados += Nivel_Conceptos_Manager.Instance.virusMatados;
                GameManager.instance.conceptos_Intentos++;
            }else if(scene == "Nivel_2_Ejercicios")
            {
                GameManager.instance.ejercicios_VirusEliminados += Nivel_Ejercicio_Manager.Instance.virusMatados;
                GameManager.instance.ejercicios_Intentos++;
            }
        }

        //Bajar CD lanzamiento a 0
        if (cDLanzamiento > 0)
        {
            cDLanzamiento -= Time.deltaTime;
        }
        else
        {
            cDLanzamiento = 0;
        }
    }
    private void Moverse()
    {
        if (!muerte)
        {
            //Movimiento con joystick
            ejeX = joystick.Horizontal * velocidad;
            ejeZ = joystick.Vertical * velocidad;

            Vector3 movimiento = new Vector3(ejeX, 0, ejeZ) * Time.deltaTime * velocidad;
            rb.MovePosition(transform.position + movimiento);

            animator.SetFloat("Velocidad", movimiento.magnitude);

            if (movimiento.magnitude > 0)
            {
                animator.SetBool("Caminar", true);
            }
            else
            {
                animator.SetBool("Caminar", false);
            }

            //Rotar personaje
            if (movimiento != Vector3.zero)
            {
                Quaternion rotacion = Quaternion.LookRotation(movimiento, Vector3.up);

                transform.rotation = rotacion;
            }
        }
        else
        {        
            if (scene == "Nivel_1_Conceptos")
            {
                Nivel_Conceptos_Manager.Instance.ReiniciarEscena();
            }
            else if (scene == "Nivel_2_Ejercicios")
            {
                Nivel_Ejercicio_Manager.Instance.ReiniciarEscena();
            }          
        }  
    }

    public void Disparar()
    {
        if (cDLanzamiento <= 0 && !muerte)
        {
            animator.SetTrigger("Disparar");
            StartCoroutine(SimpleRetraso());
            cDLanzamiento = cd;
            StartCoroutine(VelocidadReducida());
        }
    }

    IEnumerator VelocidadReducida()
    {
        float v = velocidad;
        velocidad = velocidad / 2;
        yield return new WaitForSeconds(1.5f);
        velocidad = v;
    }
    //Coorrutina hardcodeada por el hecho de que las anims vienen de mixamo, fue la forma que pude hacer un lanzamiento creible
    IEnumerator SimpleRetraso()
    {
        yield return new WaitForSeconds(0.7f);
        //Instanciar matraz y lanzarlo hacia adelante con un poco de fuerza.
        GameObject objeto = Instantiate(matrazArma, puntoLanzamiento.position, puntoLanzamiento.rotation);
        Rigidbody rbObjeto = objeto.GetComponent<Rigidbody>();

        if (rbObjeto != null)
        {
            rbObjeto.AddForce(puntoLanzamiento.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }
    }
}
