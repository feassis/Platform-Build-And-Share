using UnityEngine;

public class PassThroughPlatform : Interactables
{
    [SerializeField] private PlayerDetection lowerDetection;
    [SerializeField] private PlayerDetection higherDetection;
    [SerializeField] private GameObject platformColider;

    private Player playerAbove;


    private void Awake()
    {
        lowerDetection.OnPlayerEnter += LowerDetection_OnPlayerEnter;
        lowerDetection.OnPlayerExit += LowerDetection_OnPlayerExit;

        higherDetection.OnPlayerEnter += HigherDetection_OnPlayerEnter;
        higherDetection.OnPlayerExit += HigherDetection_OnPlayerExit;
    }

    private void Update()
    {
        if(playerAbove == null)
        {
            return;
        }


        if(playerAbove.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movemnt))
        {
            if(movemnt.IsPressingDown)
            {
                platformColider.gameObject.SetActive(false);
            }
        }

    }

    private void HigherDetection_OnPlayerExit(Player obj)
    {
        playerAbove = null;
        platformColider.SetActive(true);
    }

    private void HigherDetection_OnPlayerEnter(Player obj)
    {
        playerAbove = obj;  
    }

    private void LowerDetection_OnPlayerExit(Player obj)
    {
        platformColider.SetActive(true);
    }

    private void LowerDetection_OnPlayerEnter(Player obj)
    {
        platformColider.SetActive(false);
    }
}
