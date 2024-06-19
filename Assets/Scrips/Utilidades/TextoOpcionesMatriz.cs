using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class TextoOpcionesMatriz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoPregunta;
    private string texto;
    private char result;

    [SerializeField] TextMeshProUGUI[] arrastrables;

    private void OnEnable()
    {
        StartCoroutine(SimpleRetraso());
    }

    private IEnumerator SimpleRetraso()
    {
        yield return new WaitForSeconds(0.1f);

        texto = "";
        texto = textoPregunta.text;
        result = EncontrarPrimerCaracterEntreParentesis(texto);

        Debug.Log($"El primer car�cter encontrado entre par�ntesis es: {result}");

        if (arrastrables.Length >= 5)
        {
            arrastrables[0].text = result.ToString().ToUpper();              // A
            arrastrables[1].text = result.ToString().ToUpper() + result.ToString().ToLower(); // Aa
            arrastrables[2].text = result.ToString().ToUpper() + result.ToString().ToUpper(); // AA
            arrastrables[3].text = result.ToString().ToLower();              // a
            arrastrables[4].text = result.ToString().ToLower() + result.ToString().ToLower(); // aa
        }
    }

    char EncontrarPrimerCaracterEntreParentesis(string texto)
    {
        char primerCaracter = '\0'; // Valor por defecto 

        // Buscar el primer par�ntesis abierto '('
        int indiceParentesisAbierto = texto.IndexOf('(');

        if (indiceParentesisAbierto != -1)
        {
            // Buscar el primer par�ntesis cerrado ')' despu�s del par�ntesis abierto
            int indiceParentesisCerrado = texto.IndexOf(')', indiceParentesisAbierto);

            if (indiceParentesisCerrado != -1 && indiceParentesisCerrado > indiceParentesisAbierto)
            {
                // Extraer el texto entre par�ntesis
                string textoEntreParentesis = texto.Substring(indiceParentesisAbierto + 1, indiceParentesisCerrado - indiceParentesisAbierto - 1);

                // Obtener el primer car�cter dentro del texto entre par�ntesis
                primerCaracter = textoEntreParentesis.FirstOrDefault();
            }
        }

        return primerCaracter;
    }
}
