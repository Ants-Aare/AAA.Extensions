using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class SetMaterial : MonoBehaviour
    {
        [SerializeField] private Renderer meshRenderer;
        [SerializeField] private Material material = null;
        [SerializeField] private int index = 0;

        public void SetRendererMaterial()
        {
            var mats = meshRenderer.materials;
            mats[index] = material;
            meshRenderer.materials = mats;
        }
    }
}