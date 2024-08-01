using UnityEngine.EventSystems;
using UnityEngine;

public class TowerDragHandler : MonoBehaviour
{
    private TowerBase tower;
    private TowerSlotHolder potentialSlotHolder;
    private bool isDragging;
    private void Awake()
    {
        tower = GetComponent<TowerBase>();
        if (tower == null)
        {
            Debug.LogError("TowerDragHandler requires a TowerBase component on the same GameObject.");
        }
    }

    private void OnMouseDown()
    {
        if (!tower.GetIsSettled())
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            if (potentialSlotHolder != null && potentialSlotHolder.CanTowerPlace())
            {
                potentialSlotHolder.PlaceTower(tower);
                tower.SetIsSettled(true,potentialSlotHolder);
                this.enabled = false;
            }
           
            isDragging = false;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = -Camera.main.transform.position.z;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float distance))
            {
                worldPosition = ray.GetPoint(distance);
                worldPosition.y = 0;
                tower.transform.position = worldPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Slot"))
        {
            TowerSlotHolder slotHolder = other.GetComponentInParent<TowerSlotHolder>();
            if (slotHolder != null && slotHolder.CanTowerPlace())
            {
                potentialSlotHolder = slotHolder;
            }
        }
    }    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Slot"))
        {
            TowerSlotHolder slotHolder = other.GetComponentInParent<TowerSlotHolder>();
            if (slotHolder != null && slotHolder.CanTowerPlace())
            {
                potentialSlotHolder = slotHolder;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Slot"))
        {
            TowerSlotHolder slotHolder = other.GetComponentInParent<TowerSlotHolder>();
            if (potentialSlotHolder == slotHolder)
            {
                potentialSlotHolder = null;
            }
        }
    }
}
