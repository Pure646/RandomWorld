using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomWorld
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance { get; private set; }

        public Vector3 CameraAimingPoint { get; private set; }

        public LayerMask aimingLayerMask;


        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, aimingLayerMask, QueryTriggerInteraction.Ignore))
            {
                CameraAimingPoint = hitInfo.point;
            }
            else
            {
                CameraAimingPoint = ray.GetPoint(1000f);
            }
        }
    }
}
