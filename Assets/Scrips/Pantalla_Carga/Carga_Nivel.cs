using UnityEngine.SceneManagement;

public static class Carga_Nivel 
{
    public static string siguienteNivel;

    public static void Nivel_A_Cargar(string nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene("PantallaCarga");
    }
}
