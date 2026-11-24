using System.Collections.Generic;
using UnityEngine;

public class HistoryRecorder : MonoBehaviour
{
    [SerializeField] private List<HistoryRecord> records = new List<HistoryRecord>();

    private void OnEnable()
    {
        Machine.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        Machine.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(Machine machine)
    {
        HistoryRecord record = new HistoryRecord(Time.time, machine.Id, machine.Temperature, machine.State);
        records.Add(record);
    }
}