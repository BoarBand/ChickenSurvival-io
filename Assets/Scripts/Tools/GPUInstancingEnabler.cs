using UnityEngine;

namespace SurvivalChicken.Tools.GPUInstancing
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GPUInstancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            SetGPUInstance();
        }

        public void SetGPUInstance()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
