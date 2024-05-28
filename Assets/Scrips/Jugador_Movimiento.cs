using UnityEngine;
using UnityEngine.UI;

public class Jugador_Movimiento : MonoBehaviour
{
    private float ejeX = 0f;
    private float ejeZ = 0f;

    Animator animator;
    [SerializeField] private float velocidad;

    public Joystick joystick;
    public Button disparar;
    public ParticleSystem particulas;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        disparar.onClick.AddListener(Disparar);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
       
    }
    private void FixedUpdate()
    {
        Moverse();
    }
    private void Moverse()
    {
        //Movimiento con joystick
        ejeX = joystick.Horizontal * velocidad;
        ejeZ = joystick.Vertical * velocidad;

        Vector3 movimiento = new Vector3(ejeX, 0, ejeZ) * Time.deltaTime * velocidad;
        rb.MovePosition(transform.position + movimiento);

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

    public void Disparar()
    {
        animator.SetTrigger("Disparar");
        particulas.Emit(250);
    }
}
