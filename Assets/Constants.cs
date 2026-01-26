
public enum Directions
{
    Up, Down, Right, Left, None
}

[System.Serializable]
public class DirectionPlatformPair
{
    public Directions direction;
    public Platform platform;
}
public enum PlatformStatus
{
    Open,
    Closed,
    Completed
}