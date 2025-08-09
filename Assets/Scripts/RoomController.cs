using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject livingRoom;
    public GameObject hallRoom;
    public GameObject kitchenRoom;
    public GameObject kidRoom;
    public GameObject bathroomRoom;

    public GameObject currentRoom;

    private void Start()
    {
        currentRoom = kidRoom;
        GameManager.instance.DetectRoom += DetectRoom;
        GameManager.instance.ResetValues += ResetValues;
    }

    private void DetectRoom(GameObject room)
    {
        currentRoom = room;
        PlayerController.stepCount = 0;
        GameManager.instance.stepCounter.text = "00";
    }

    public void ResetValues()
    {
        currentRoom = kidRoom;
    }
}
