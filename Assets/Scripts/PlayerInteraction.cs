using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    public GameObject wateringCan;
    Interactable currentInteractable;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            if (wateringCan.activeInHierarchy)
            {
                currentInteractable.Interact();
            }
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // If raycast hits collider
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            // Looks for an interactable object
            if (hit.collider.tag == "Interactable")
            {
                Interactable newObj = hit.collider.GetComponent<Interactable>();

                if (currentInteractable && newObj != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }

                if (newObj.enabled)
                {
                    SetNewCurrentInteractable(newObj);
                }
            }
            else // If not an interactable
            {
                DisableCurrentInteractable();
            }
        }
        else // If nothing in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable obj)
    {
        currentInteractable = obj;
        currentInteractable.EnableOutline();
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
