using UnityEngine;

[RequireComponent(typeof(Camera))]  
[ExecuteInEditMode]
public class NoFogOnCamera : MonoBehaviour
{
    public bool AllowFog = false;

    private bool FogOn;

    private void OnPreRender()
    {
        FogOn = RenderSettings.fog;
        RenderSettings.fog = AllowFog;

    }

    private void OnPostRender()
    {
        RenderSettings.fog = FogOn;
    }
}
