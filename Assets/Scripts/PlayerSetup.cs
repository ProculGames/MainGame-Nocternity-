using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] DisabledObjects;
    
    Camera SceneCamera;
    void Start()
    {
        
        if (!isLocalPlayer)
        {
            for (int i = 0; i < DisabledObjects.Length; i++)
            {
                DisabledObjects[i].enabled = false;
            }
        }
        else
        {
            SceneCamera = Camera.main;
            if(SceneCamera != null)
            {
                SceneCamera.gameObject.SetActive(false);
            }
            
        }
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }

    void OnDisable()
    {
        if(SceneCamera != null)
        {
            SceneCamera.gameObject.SetActive(true);
        }
    }

}
