using UnityEngine;

namespace MerchantOfBohemia
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        public LayerMask selectionMask;

        private void OnEnable()
        {
            EventHandler.PointerClickEvent += HandleClick;
        }
        
        private void OnDisable()
        {
            EventHandler.PointerClickEvent -= HandleClick;
        }

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void HandleClick(Vector3 mousePosition)
        {
            Debug.Log("Clickled 1");
            GameObject result;
            if (FindTarget(mousePosition, out result))
            {
                Debug.Log("Clickled 2");
                if (IsPlayerSelected(result))
                {
                    Debug.Log("Clickled 3");
                    EventHandler.CallPlayerSelectedEvent(result);
                }
                else if (IsTerrainSelected(result))
                {
                    Debug.Log("Clickled 4");
                    EventHandler.CallTerrainSelectedEvent(result);
                }
            }
        }

        private bool IsPlayerSelected(GameObject result)
        {
            return result.GetComponent<Unit>() != null;
        }
        
        private bool IsTerrainSelected(GameObject result)
        {
            return result.GetComponent<Hex>() != null;
        }

        private bool FindTarget(Vector3 mousePosition, out GameObject result)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, 100, selectionMask))
            {
                Debug.Log("Mask'a uygun obje bulundu");
                result = hit.collider.gameObject;
                return true;
            }
            else
            {
                Debug.Log("Mask'a uygun obje bulunamadı");
                result = null;
                return false;    
            }
            
        }
    }
}
