using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInjector : MonoBehaviour
{
    public Color injectionColor = Color.red;
    public float transitionDuration = 1f;
    public float raycastDistance = 100f;

    private bool isInjectingColor = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isInjectingColor)
        {
            StartCoroutine(ChangeMeshColorSmoothly());
        }
    }

    IEnumerator ChangeMeshColorSmoothly()
    {
        isInjectingColor = true;

        // Cast a ray from the mouse position into the scene
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // Check if the ray hit a mesh
            MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Get the current material color
                Color originalColor = meshRenderer.material.color;

                // Smoothly transition the color
                float elapsedTime = 0f;

                while (elapsedTime < transitionDuration)
                {
                    float t = elapsedTime / transitionDuration;
                    meshRenderer.material.color = Color.Lerp(originalColor, injectionColor, t);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Ensure final color is set
                meshRenderer.material.color = injectionColor;
            }
        }

        isInjectingColor = false;
    }
}
