using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// <para>Records the player movements and reproduce them.</para>
/// </summary>
[RequireComponent(typeof(Stats))]
public class PlayerRecorder: Controller
{
    [SerializeField][Tooltip("Used to get the checkpoint time")]
    private CheckPointerController checkPointerController;

    [SerializeField] [Tooltip("Seconds between each recording")]
    private float timeBetweenRecords;

    private Stats stats;
    private Record[] records;
    private int currentIndex;
    private float lastRecord;
    private bool recording;
    private float startingTime;
    private Vector2 lastPosition;

    /// <summary>
    /// <para>Starts recording the player movements</para>
    /// </summary>
    public void StartRecording()
    {
        currentIndex = 0;
        recording = true;
        startingTime = Time.time;
    }

    /// <summary>
    /// <para>Stops recording the player movements</para>
    /// </summary>
    public void StopRecording()
    {
        recording = false;
    }

    /// <summary>
    /// <para>Reproduces the records</para>
    /// </summary>
    public void ReproduceRecord()
    {
        stats.ResetStats();
        StartCoroutine(Reproduce());
    }

    /// <summary>
    /// <para>Execute each recording taking into account the time each record was made</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reproduce()
    {
        var startTime = Time.time;
        for(var i = 0; i < currentIndex; i++)
        {
            var record = records[i];
            while (Time.time < startTime + record.Time)
                yield return null;
            MoveToPosition(record.Position);
            if (record.Type == RecordType.Shoot) Shoot();
            yield return null;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();
        stats = GetComponent<Stats>();
        records = new Record[(int) (checkPointerController.checkPointTime / timeBetweenRecords)];
        lastRecord = timeBetweenRecords;
        controllable.OnShoot += RecordShoot;
    }

    /// <summary>
    /// <para>Records the position of the player</para>
    /// </summary>
    private void Update()
    {
        if (!recording) return;
        if (lastRecord >= timeBetweenRecords)
        {
            Vector2 position = transform.position;
            if (lastPosition == position) return;
            lastPosition = transform.position;
            SaveNewRecord(new Record(lastPosition, RecordType.Move, GetTime()));
            lastRecord = 0;
            return;
        };

        lastRecord += Time.deltaTime;
    }

    /// <summary>
    /// <para>Records the shoots</para>
    /// </summary>
    private void RecordShoot()
    {
        SaveNewRecord(new Record(transform.position, RecordType.Shoot, GetTime()));
    }

    /// <summary>
    /// <para>Returns the time passed between the beginning of the frame and the starting time</para>
    /// </summary>
    /// <returns></returns>
    private float GetTime()
    {
        return Time.time - startingTime;
    }
    
    /// <summary>
    /// <para>Adds a new record to the records array</para>
    /// </summary>
    /// <param name="record">The record to be added</param>
    private void SaveNewRecord(Record record)
    {
        records[currentIndex] =record;
        currentIndex++;
    }
    
    
}