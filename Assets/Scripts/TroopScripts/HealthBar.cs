using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    bool isVisible;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        // Only display on partial health
        if (isVisible) {
            meshRenderer.enabled = true;
            AlignCamera();
        } else {
            meshRenderer.enabled = false;
        }
    }

    public void UpdateDamage(int CurrentHealth, int MaxHealth) {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", CurrentHealth / (float)MaxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

    public void SetVisible(bool visible)
    {
        isVisible = visible;
    }
}
