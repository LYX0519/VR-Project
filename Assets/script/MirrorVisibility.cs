using UnityEngine;

public class MirrorVisibility : MonoBehaviour
{
    public GameObject mirrorSurface;       
    public Camera mirrorCam;              
    public Material mirrorMaterial;      
    public Material defaultMaterial;      

    void Start()
    {
        if (mirrorCam != null)
            mirrorCam.enabled = false;

        if (mirrorSurface != null && defaultMaterial != null)
            mirrorSurface.GetComponent<Renderer>().material = defaultMaterial;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mirrorCam != null)
                mirrorCam.enabled = true;

            if (mirrorSurface != null && mirrorMaterial != null)
                mirrorSurface.GetComponent<Renderer>().material = mirrorMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mirrorCam != null)
                mirrorCam.enabled = false;

            if (mirrorSurface != null && defaultMaterial != null)
                mirrorSurface.GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
