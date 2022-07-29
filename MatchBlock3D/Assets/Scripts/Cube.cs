using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public int value;
    public GameObject indicator;
    public GameObject nextCube;
    public Rigidbody rb;
    public bool canSpawn;
    public int ID;
    public bool didAdded;

    private void Awake()
    {
        ID = GetInstanceID();
        rb = gameObject.GetComponent<Rigidbody>();
        if (canSpawn) return;
        rb.freezeRotation = false;
        indicator.SetActive(false);
}
    public void SendCube()
    {
        rb.freezeRotation = false;
        indicator.SetActive(false);
        rb.AddForce(-transform.right * 700);
    }
    public void CreatingAnim()
    {
        rb.AddForce((Vector3.forward+Vector3.up) * 100);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            if (collision.gameObject.TryGetComponent(out Cube cube))
            {
                if (cube.value == value)
                {
                    if (ID<cube.ID) return;
                    Destroy(cube.gameObject);
                    Destroy(this.gameObject);
                    if (nextCube)
                    {
                        GameObject temp = Instantiate(nextCube, transform.position, Quaternion.identity);
                        if (temp.TryGetComponent(out Cube tempCube))
                        {
                            tempCube.CreatingAnim();
                        }
                    }
                }
            }
        }
    }
}
