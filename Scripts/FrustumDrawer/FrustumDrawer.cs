using UnityEngine;

[ExecuteInEditMode]
public class FrustumDrawer : MonoBehaviour
{
	[SerializeField] private Camera cameraComponent;

	[SerializeField] private Color color = Color.cyan;

	[SerializeField] private bool isDrawingGizmos = false;

	private void Awake()
	{
		if (cameraComponent == null)
		{
			cameraComponent = gameObject.GetComponent<Camera>();
		}
	}

	private void OnDrawGizmos()
	{
		if (cameraComponent != null && isDrawingGizmos)
		{
			Gizmos.matrix = cameraComponent.transform.localToWorldMatrix;
			Gizmos.color = color;
			Gizmos.DrawFrustum(
				Vector3.zero,
				cameraComponent.fieldOfView,
				cameraComponent.farClipPlane,
				cameraComponent.nearClipPlane,
				cameraComponent.sensorSize.x / cameraComponent.sensorSize.y);
		}
	}
}