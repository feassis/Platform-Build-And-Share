using System.Collections.Generic;
using UnityEngine;

public class Door : Interactables
{
    [SerializeField] private GameObject closed;
    [SerializeField] private GameObject opened;

    [SerializeField] private GameObject colisionObj;
    [SerializeField] private AudioClip doorSound;

    private DoorMode currentDoor;

    private enum DoorMode
    {
        Opened,
        Closed
    }

    private List<Switch> mySwitches = new List<Switch>();

    private void Awake()
    {
        Close();
    }

    public void Connect(Switch switchObj)
    {
        mySwitches.Add(switchObj);

        switchObj.OnActivation += SwitchObj_OnActivation;
    }

    private void SwitchObj_OnActivation()
    {
        ToggleDoorStage();
    }

    private void ToggleDoorStage()
    {
        SoundManager.Instance.PlaySFX(doorSound, transform.position);
        if(currentDoor == DoorMode.Opened)
        {
            Close();
        }
        else
        {
            Open();
            
        }
    }

    private void Close()
    {
        opened.SetActive(false);
        closed.SetActive(true);
        colisionObj.SetActive(true);
        currentDoor = DoorMode.Closed;
    }

    private void Open()
    {
        Debug.Log("Try to Open");
        opened.SetActive(true);
        closed.SetActive(false);
        colisionObj.SetActive(false);
        currentDoor = DoorMode.Opened;
    }

    public void Disconnect(Switch switchObj)
    {
        mySwitches.Remove(switchObj);
        switchObj.OnActivation -= SwitchObj_OnActivation;
    }

}
