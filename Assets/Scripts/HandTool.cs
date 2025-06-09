using UnityEngine;

public class HandTool : MonoBehaviour
{


   [SerializeField] private Collider tool;


    private void Start() {
        tool.enabled = false;
    }


    public void ActiveCollider() {

        tool.enabled = true;
     
    }

    public void DisableCollider() {

        tool.enabled = false;
    }
}
