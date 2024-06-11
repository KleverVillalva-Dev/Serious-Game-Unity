public class Ejercicio
{
    public int ejercicio_id;
    public string pregunta;
    public string imagen;  // Puede ser null
   //Cambiamos string "tipo" por entero tipo_id, 1 para multiple 2 para punnet.
    public int tipo;
    public string detalles;
    public bool mostrar_solucion;
    public string explicacion_solucion;
    public Opcion[] opcionesMultiples;
    public Matriz[] matrizPunnett;
}

public class Opcion
{
    public int opcion_id;
    public int ejercicio_id;
    public string texto_opcion;
    public bool es_correcta;
    public string tipo;
    public string tipo_interactivo;
}

public class Matriz
{
    public int matriz_id;
    public int ejercicio_id;
    public string alelo1;
    public string alelo2;
    public string resultado;
}

//Para nivel 1 conceptos
public class Conceptos
{
    public int concepto_id;
    public string titulo;
    public string descripcion;
    public string imagen;
    public string categoria;
}

// Para evaluacion

public class PreguntasEvaluacion
{
    public int pregunta_id;
    public string texto_pregunta;
    public string tipo_pregunta;
    public string detalles;
    public string explicacion_solucion;
    public OpcionesEvaluacion[] opcionesEvaluacion;
}

public class OpcionesEvaluacion
{
   public int opcion_id;
   public int pregunta_id;
   public string texto_opcion;
   public bool es_correcta;
}

