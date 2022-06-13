using UnityEngine;

public class UIStates : MonoBehaviour
{
    [SerializeField] GameObject firstState;
    private GameObject currentState;

    private void Awake()
    {
        ChangeState(firstState);
    }

    public void ChangeState(GameObject newState)
    {
        if(currentState != null)
        {
            currentState.SetActive(false);
        }

        currentState = newState;

        if(currentState != null)
        {
            currentState.SetActive(true);
        }
    }
}
