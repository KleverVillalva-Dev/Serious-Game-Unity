using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionesCamara : MonoBehaviour
{
    //Script para ocultar objetos entre la camara y el jugador. Se coloca en una camara en tercera persona.

    public Transform player; // El personaje
    public LayerMask obstacleLayer; // La capa de los obstáculos

    private HashSet<Renderer> transparentRenderers = new HashSet<Renderer>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        // Crear un rayo desde la cámara al jugador
        Ray ray = new Ray(transform.position, player.position - transform.position);
        RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, player.position), obstacleLayer);

        HashSet<Renderer> currentHits = new HashSet<Renderer>();

        // Recorrer todos los objetos que han sido impactados
        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();

            if (rend != null)
            {
                // Cambiar la visibilidad del objeto a translúcido
                SetMaterialTransparent(rend);
                currentHits.Add(rend);
            }
        }

        // Restaurar objetos a opaco si ya no están en el camino
        foreach (Renderer rend in transparentRenderers)
        {
            if (!currentHits.Contains(rend))
            {
                SetMaterialOpaque(rend);
            }
        }

        // Actualizar la lista de renderers transparentes
        transparentRenderers = currentHits;
    }

    private void SetMaterialTransparent(Renderer renderer)
    {
        Material[] materials = renderer.materials;
        foreach (Material mat in materials)
        {
            Color color = mat.color;
            color.a = 0.3f; // HardCodeado. Cambiar transparencia desde una variable si es necesario.
            mat.color = color;

            // Cambiar el render mode a transparente (Asegurarse de que sea compatible en su material, para ser translucido)
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    private void SetMaterialOpaque(Renderer renderer)
    {
        Material[] materials = renderer.materials;
        foreach (Material mat in materials)
        {
            Color color = mat.color;
            color.a = 1f; // Totalmente opaco
            mat.color = color;

            // Cambiar el render mode a opaco
            mat.SetFloat("_Mode", 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = -1;
        }
    }
}
