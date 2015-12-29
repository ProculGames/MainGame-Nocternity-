using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.Vehicles.Car
{
    public class PlayerDrive : MonoBehaviour
    {
        //player gameobjects
        public CarUserControl car2;
        
        public GameObject player;
        public GameObject car;
        void Start()
        {
            car2 = gameObject.GetComponent<CarUserControl>();
            car2.enabled = false;
        }
        void OnTriggerEnter(Collider col)
        {
          
            if (col.CompareTag("Player"))
            {
              
                    Debug.Log("enter");
                    player.transform.parent = car.transform;
                    player.SetActive(false);
                    car2.enabled = true;
            
            }

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.transform.parent = null;
                player.SetActive(true);
                car2.enabled = false;
                
            }
        }

    }
}

