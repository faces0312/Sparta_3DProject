using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;

    public GameObject[] door;
    private Door[] doors;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        doors = new Door[door.Length];
    }

    public void Open_Door(int index)
    {
        StartCoroutine(OpenDoorCoroutine(index));
    }

    private IEnumerator OpenDoorCoroutine(int index)
    {
        for (float i = 0; i >= -3; i-=0.1f)
        {
            door[index].transform.localPosition = new Vector3(door[index].transform.localPosition.x, i, door[index].transform.localPosition.z);
            yield return new WaitForSeconds(0.03f);
        }

        door[index].gameObject.transform.localPosition = new Vector3(door[index].transform.localPosition.x, -3, door[index].transform.localPosition.z);
        doors[index] = door[index].GetComponent<Door>();
        doors[index].OpenEffect();
    }
}
