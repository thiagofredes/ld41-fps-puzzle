using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{

    public Vector3 oscilationAxis;

	public float oscilationAmplitude = 0.5f;

	public float oscilationFrequency = 2f;

    public Vector3 rotationAxis;

	public float rotationSpeed = 15f;

    public Renderer[] pickupRenderers;

    public Canvas pickupCanvas;

    private SphereCollider _collider;

    private Rigidbody _rigidbody;

	protected bool _canContact;



	void OnEnable(){
		_canContact = true;
	}

    void OnTriggerEnter(Collider other)
    {
        FPSPlayerController playerController = other.GetComponent<FPSPlayerController>();
        if (playerController != null)
        {
            OnContactDo();
        }
    }

    void Update()
    {
        transform.position += Time.deltaTime * transform.TransformDirection(oscilationAxis) * oscilationAmplitude * Mathf.Sin(oscilationFrequency * Time.time);
        transform.Rotate(rotationAxis * Time.deltaTime * rotationSpeed);
    }

    protected virtual void OnContactDo() { }
}
