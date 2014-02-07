using UnityEngine;
using System.Collections;

public class Dance : MonoBehaviour {

	public float maxAngleRange = 5f;
	public float rotateSpeed = 15f;

	public float maxVerticalChange = 0.5f;
	public float upDownSpeed = 8f;

	public bool dancing = true;

	Quaternion targetRotation;
	Vector3 targetPosition;
	
	// Use this for initialization
	void Start () {
		targetRotation = transform.localRotation;
		targetPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

		if(!dancing || !Menu.gotToMenu)
			return;

		if(Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
		{
			targetRotation = Quaternion.AngleAxis((Random.value-0.5f)*2 * maxAngleRange, Vector3.forward); 
		}

		transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
	

		if(Mathf.Abs(transform.localPosition.y - targetPosition.y) < 0.1f)
		{
			targetPosition = new Vector3(0f,(Random.value-0.5f)*2 * maxVerticalChange,0);
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * upDownSpeed);
	}
}
