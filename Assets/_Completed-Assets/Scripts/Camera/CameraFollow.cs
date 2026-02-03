using UnityEngine;

namespace Complete.Cameras {
	
    /// <summary>
    /// Makes the camera follow a specific target with smoothing and predictive offset.
    /// Used primarily in split-screen mode for individual tank cameras.
    /// </summary>
    public class CameraFollow : MonoBehaviour {
        
        private Camera m_Camera;                        // Camera reference              
        public float smooth = 0.5f;                     // Smoothness
		public float limitDist = 20.0f;                 // Distance limit
		public float m_PredictiveWeight = 0.5f;         // Multiplier for predictive offset based on target velocity
       
        private void FixedUpdate ()
        {
			if (m_Camera == null) {
				m_Camera = GetComponentInChildren<Camera>();
				return;
			}

            Follow();
        }


		private void Follow()
        {
			Vector3 targetPosition = transform.position;

			// Add predictive offset if the target (this GameObject) has a Rigidbody
			Rigidbody targetRigidbody = GetComponent<Rigidbody>();
			if (targetRigidbody != null)
			{
				targetPosition += targetRigidbody.linearVelocity * m_PredictiveWeight;
			}

			float currentDist = Vector3.Distance (targetPosition, m_Camera.transform.position);

            if (currentDist > limitDist)
            {
                m_Camera.transform.position = Vector3.Lerp (m_Camera.transform.position, targetPosition, Time.deltaTime * smooth);
            }
        }
	}
}