using System;

[Serializable]
public class HistoryRecord
{
    public float time;
    public string id;
    public float temp;
    public MachineState state;

    public HistoryRecord(float time, string id, float temp, MachineState state)
    {
        this.time = time;
        this.id = id;
        this.temp = temp;
        this.state = state;
    }
}