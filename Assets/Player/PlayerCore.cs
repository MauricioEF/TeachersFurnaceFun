using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public bool IsMoving
    {
        get;
        private set;
    }

    public void StartMoving()
    {
        IsMoving = true;
    }
    public void Stop()
    {
        Debug.Log("Stopped!");
        IsMoving = false;
    }
}
