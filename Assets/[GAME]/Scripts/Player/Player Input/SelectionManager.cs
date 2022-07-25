using UnityEngine;

namespace MerchantOfBohemia
{
    public class SelectionManager : MonoBehaviour
    {
        private Camera _mainCamera;
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
            _mainCamera = Camera.main;
        }

        private void HandleClick(Vector3 mousePosition)
        {
            GameObject result;
            if (FindTarget(mousePosition, out result))
            {
                if (IsPlayerSelected(result))
                {
                    EventHandler.CallPlayerSelectedEvent(result);
                }
                else if (IsTerrainSelected(result))
                {
                    EventHandler.CallTerrainSelectedEvent(result);
                }
            }
        }

        private bool IsPlayerSelected(GameObject result)
        {
            return result.GetComponent<PlayerMovement>() != null;
        }
        
        private bool IsTerrainSelected(GameObject result)
        {
            return result.GetComponent<Hex>() != null;
        }

        private bool FindTarget(Vector3 mousePosition, out GameObject result)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, 1000, selectionMask))
            {
                result = hit.collider.gameObject;
                return true;
            }
            else
            {
                result = null;
                return false;    
            }
            
        }
    }
}
