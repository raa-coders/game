using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private GvrPointerPhysicsRaycaster _raycaster;

    public void Start()
    {
        _raycaster = transform.GetChild(0).GetComponent<GvrPointerPhysicsRaycaster>();
            
    }
}