using UnityEngine;
using System.Collections;

    
public class BlendShapeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string blendShapeName = "Key 1";
    public float frequency = 4.0f;

    private void OnEnable()
    {
        if (skinnedMeshRenderer == null)
        {
            // Assuming the SkinnedMeshRenderer is on the same GameObject
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        // Ensure the blend shape exists in the mesh
        if (skinnedMeshRenderer && skinnedMeshRenderer.sharedMesh &&
            skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName) != -1)
        {
            // Start the blend shape animation
            StartCoroutine(AnimateBlendShape());
        }
        else
        {
            Debug.LogError("Blend shape not found or SkinnedMeshRenderer not assigned.");
        }
    }

    private IEnumerator AnimateBlendShape()
    {
        while (true)
        {
            // Calculate the blend shape value using a sinusoidal function
            float blendShapeValue = 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * frequency * Time.time);

            // Set the blend shape value
            skinnedMeshRenderer.SetBlendShapeWeight(
                skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName),
                blendShapeValue * 100f // Assuming the blend shape values are in the range 0 to 100
            );

            // Wait for the next frame
            yield return null;
        }
    }
}
