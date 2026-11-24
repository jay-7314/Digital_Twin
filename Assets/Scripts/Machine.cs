using UnityEngine;

public enum MachineState
{
    Running,
    Stopped,
    Error
}

public class Machine : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private float temperature;
    [SerializeField] private MachineState state;

    private Renderer rend;

    public string Id => id;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        UpdateVisual();
    }

    public void SetData(float newTemperature, MachineState newState)
    {
        temperature = newTemperature;
        state = newState;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        switch (state)
        {
            case MachineState.Running:
                rend.material.color = Color.green;
                break;
            case MachineState.Stopped:
                rend.material.color = Color.gray;
                break;
            case MachineState.Error:
                rend.material.color = Color.red;
                break;
        }
    }
}