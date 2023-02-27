using UnityEngine;

public class SpriteShadowComponent : MonoBehaviour
{
    private void Start()
    {
        EnableShadowShader();
    }

    private void EnableShadowShader()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }
}