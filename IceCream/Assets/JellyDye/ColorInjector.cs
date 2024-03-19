using UnityEngine;

public class ColorInjector : MonoBehaviour
{
    public float maxRadius = 1f; 
    public float maxRadiusIncrement = 0.1f; 
    public float maxMaxRadius = 5f; 
    public float colorChangeSpeed = 1f;
    public Color targetColor = Color.red;

    private bool isMouseDown = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            maxRadius = 0f; 
        }

        if (isMouseDown)
        {
            maxRadius = Mathf.Min(maxRadius + maxRadiusIncrement * Time.deltaTime, maxMaxRadius);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Collider>() != null)
                {
                    Vector3 worldPosition = hit.point;

                    Collider[] colliders = Physics.OverlapSphere(worldPosition, maxRadius);
                    foreach (Collider collider in colliders)
                    {
                        MeshRenderer meshRenderer = collider.GetComponent<MeshRenderer>();
                        if (meshRenderer != null)
                        {
                            meshRenderer.material.color = targetColor;
                        }
                    }
                }
            }
        }
    }
}
