using UnityEngine;

public class PlaceableObjects : MonoBehaviour {
    public Transform ghostTransform;
    public Transform placeableObject;
    private Transform ghostInstantiated;
    public Material validMaterial;
    public Material inValidMaterial;
    public bool setobject = false;
    public LayerMask layerMask;
    private BoxCollider objectCollider; // Renamed to avoid conflict with inherited 'collider'
    public LayerMask obstacles;
    public static PlaceableObjects Instance;


    private void Awake() {
        Instance = this;
       
    }

    void Update() {
        if (setobject) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, layerMask)) {
                ghostInstantiated.transform.position = hit.point;


              bool isOverlapping = Physics.CheckBox(
              ghostInstantiated.transform.position,
              objectCollider.size / 2f,
              Quaternion.identity,
              obstacles
  );

                if (isOverlapping) {
                    ghostInstantiated.GetComponent<MeshRenderer>().material = inValidMaterial;
                }
                else {
                    ghostInstantiated.GetComponent<MeshRenderer>().material = validMaterial;
                    if (Input.GetMouseButton(0)) {
                        Instantiate(placeableObject, hit.point, Quaternion.identity);
                        Destroy(ghostInstantiated.gameObject);
                        setobject = false;
                    }
                  
                }


            }

          
        }
    }

    public void CreateGhostPrefab() {
        ghostInstantiated = Instantiate(ghostTransform);

        objectCollider = placeableObject.GetComponent<BoxCollider>();
      

        setobject = true;
    }
}
