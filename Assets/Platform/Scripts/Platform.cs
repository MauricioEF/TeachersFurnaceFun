using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{

    [SerializeField] private List<DirectionPlatformPair> directions;
    [SerializeField] private PlatformStatus status;
    private Dictionary<Directions, Platform> _adjacentPlatforms;

    public PlatformStatus Status => status;

    //Lifecycle methods
    private void Awake()
    {
        _adjacentPlatforms = new Dictionary<Directions, Platform>();
        foreach (var e in directions)
        {
            _adjacentPlatforms[e.direction] = e.platform;
        }
    }
    //Helpers
    public Dictionary<Directions, Platform> GetAdjacentPlatforms()
    {
        return _adjacentPlatforms;
    }

    public void FillAdjacentPlatformsFromData()
    {
        //TODO: Implement logic to fill from backend.
    }
}
