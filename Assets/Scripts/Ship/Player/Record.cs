
using UnityEngine;


/// <summary>
/// <para>Struct used to save the player movements</para>
/// </summary>
public struct Record
{
    private Vector2 position;
    private RecordType type;
    private float time;

    public Vector2 Position => position;
    public RecordType Type => type;
    public float Time => time;

    public Record(Vector2 position, RecordType type, float time)
    {
        this.position = position;
        this.type = type;
        this.time = time;
    }
}

/// <summary>
/// <para>The record type represent what type of action the player made when the record was made</para>
/// </summary>
public enum RecordType
{
    Move,
    Shoot,
    Die,
    Ability
}
