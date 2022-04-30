using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispatchEventOnTrigger : MonoBehaviour
{
    
    [SerializeField] private bool _destroyOnGet = false;
    [SerializeField] private EventManager.EventType _event = EventManager.EventType.None;
    [SerializeField] private string _message = "";
    
    private void OnTriggerEnter(Collider collider)
    {
        EventManager.Instance.DispatchEvent(_event, gameObject);
        UIMessage.Instance.ShowTimedMessage(_message);
        if (_destroyOnGet)
        {
            Destroy(gameObject);
        }
    }

}
