using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float pickUpRange = 2f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform handPoint;

    private GameObject pickedObject;
    private Rigidbody objectRb;       

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pickedObject == null)
            {
                TryPickUp();
            }
            else 
            {
                DropObject();
            }
        }

        if (pickedObject != null)
        {
            MoveObject();
        }
    }

    void TryPickUp()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                pickedObject = hit.collider.gameObject;
                objectRb = pickedObject.GetComponent<Rigidbody>();

                if (objectRb != null)
                {
                    objectRb.isKinematic = true;
                }
            }
        }
    }

    void MoveObject()
    {
        pickedObject.transform.position = Vector3.Lerp(pickedObject.transform.position, handPoint.position, Time.deltaTime * moveSpeed);
    }

    void DropObject()
    {
        if (objectRb != null)
        {
            objectRb.isKinematic = false;
            pickedObject = null;
        }
    }
}
