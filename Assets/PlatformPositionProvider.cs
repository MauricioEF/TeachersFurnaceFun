using System.Collections.Generic;
using UnityEngine;

public class PlatformPositionProvider : MonoBehaviour
{
    public static PlatformPositionProvider Instance
    {
        get;
        private set;
    }
    public Vector3 currentPlatformPosition;
    [SerializeField] public DirectionPlatformPair initialMap;
    private Dictionary<Directions, Platform> _possiblePlatforms;

    //Lifecycle Methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _possiblePlatforms = new Dictionary<Directions, Platform>();
        _possiblePlatforms[initialMap.direction] = initialMap.platform;

    }
    //Handlers
    public Vector3? TryGetPossiblePosition(Directions direction)
    {
        if (_possiblePlatforms.ContainsKey(direction))
        {
            if (_possiblePlatforms[direction].Status == PlatformStatus.Completed || _possiblePlatforms[direction].Status == PlatformStatus.Open)
            {
                //First, get the position of the new platform in which the player will move.
                currentPlatformPosition = _possiblePlatforms[direction].transform.position;
                //Second. Update Dictionary with new Platforms.
                _possiblePlatforms = _possiblePlatforms[direction].GetAdjacentPlatforms();
                //Third. Return the position to the player so the player knows where to move.
                return currentPlatformPosition;
            }

        }
        return null;
    }
}
