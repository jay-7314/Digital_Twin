using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    private Dictionary<string, Machine> machines = new Dictionary<string, Machine>();

    private void Awake()
    {
        Machine[] found = FindObjectsByType<Machine>(FindObjectsSortMode.None);

        foreach (Machine machine in found)
        {
            machines[machine.Id] = machine;
        }
    }

    public Machine GetMachine(string id)
    {
        if (machines.TryGetValue(id, out Machine machine))
        {
            return machine;
        }

        Debug.LogWarning("없는 설비: " + id);
        return null;
    }

    public void ApplyJson(string json)
    {
        SensorMessage message = JsonUtility.FromJson<SensorMessage>(json);

        Machine machine = GetMachine(message.id);
        if (machine == null)
        {
            return;
        }

        if (!System.Enum.TryParse(message.state, out MachineState state))
        {
            Debug.LogWarning("알 수 없는 상태: " + message.state);
            return;
        }

        machine.SetData(message.temp, state);
    }
}