using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class GlassBehaviour : MonoBehaviour
{
    private Camera _mainCamera;
    private EventTrigger _eventTrigger;


    private bool _isGrabbed;
    
    /// <summary>
    /// Used to move when grabbed.
    /// </summary>
    private float _baseDistToCamera;

    // TODO PJ: use this to get the glass closer when you look down
    private float _distToCamera;
    
    
    
    
    public void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _eventTrigger = GetComponent<EventTrigger>();
        
        
        
        
        
        
        
        // ON POINTER EVENTS //
        
        EventTrigger.TriggerEvent evOnPointerEnter = new EventTrigger.TriggerEvent();
        evOnPointerEnter.AddListener(OnPointerEnter);
        _eventTrigger.triggers.Add(new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter,
            callback = evOnPointerEnter
        });
        
        EventTrigger.TriggerEvent evOnPointerExit = new EventTrigger.TriggerEvent();
        evOnPointerExit.AddListener(OnPointerExit);
        _eventTrigger.triggers.Add(new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerExit,
            callback = evOnPointerExit
        });
        
        
        EventTrigger.TriggerEvent evOnPointerClick = new EventTrigger.TriggerEvent();
        evOnPointerClick.AddListener(OnPointerClick);
        _eventTrigger.triggers.Add(new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerClick,
            callback = evOnPointerClick
        });
    }



    public void Update()
    {
        if (_isGrabbed)
        {
            Transform cam = _mainCamera.transform;
            Vector3 camRot = cam.localEulerAngles;
            Vector3 newPos = cam.position;

            float pitch = camRot.x;
            float yaw = 180 - camRot.y;

            float radius = Mathf.Abs(Util.DCos(pitch)) * _baseDistToCamera;
            newPos.y -= Util.DSin(pitch) * _baseDistToCamera;

            newPos.x -= Util.DSin(yaw) * radius;
            newPos.z += Util.DCos(yaw) * radius;

            this.transform.position = newPos;
            
            // TODO PJ: get the glass closer when looking down
        }
    }





    public void OnPointerEnter(BaseEventData eventData)
    {
        Debug.Log("POINTER ENTER!!!!");
    }
    
    public void OnPointerExit(BaseEventData eventData)
    {
        Debug.Log("POINTER EXIT!!!!");
    }
    
    public void OnPointerClick(BaseEventData eventData)
    {
        Debug.Log("POINTER CLICK!!!!");
        
        _isGrabbed = !_isGrabbed;
        if (_isGrabbed)
        {
            _baseDistToCamera = Vector3.Distance(
                    _mainCamera.transform.position, 
                    this.transform.position
                );
        }
    }
}