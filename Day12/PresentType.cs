namespace Day12;

internal sealed class PresentType
{
    public int Size { get; set; }
    public char[][] Grid { get; set; }
    public char[][] FlippedVertically { get; set; }

    public char[][] FlippedHorizontally { get; set; }

    public char[][] Rotated90 { get; set; }
    public char[][] Rotated180 { get; set; }
    public char[][] Rotated270 { get; set; }

}
