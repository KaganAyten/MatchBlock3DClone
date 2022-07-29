using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public List<Cube> cubeList = new List<Cube>();
    public Cube currentCube;
    public Transform spawnPoint;
    private Touch touch;

    private Vector3 downPos, upPos;
    private bool dragStarted;
    private void Start()
    {

        currentCube = PickRandomCube();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                dragStarted = true;
                upPos = touch.position;
                downPos = touch.position;
            }
        }
        if (dragStarted)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                downPos = touch.position;
            }
            if (currentCube)
            {
                currentCube.rb.velocity += CalculateDirection();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                downPos = touch.position;
                dragStarted = false;
                if (!currentCube) return;
                currentCube.rb.velocity = Vector3.zero;
                currentCube.SendCube();
                StartCoroutine(SetCube());
            }
            //currentCube?.gameObject.transform.Translate(CalculateDirection() * 10 * Time.deltaTime);
            

        }

    }
    private IEnumerator SetCube()
    {
        yield return new WaitForSeconds(1);
        currentCube = PickRandomCube();
    }
    public Cube PickRandomCube()
    {
        GameObject temp = Instantiate(cubeList[Random.Range(0, cubeList.Count)].gameObject,spawnPoint.position,Quaternion.identity);
        return temp.GetComponent<Cube>();
    }
    Vector3 CalculateDirection()
    {
        Vector3 temp = (downPos - upPos).normalized;
        temp.z = temp.x;
        temp.x = 0;
        temp.y = 0;
        return temp;
    }
}
