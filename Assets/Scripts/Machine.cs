using TMPro;
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
    private TMP_Text label;

    public string Id => id;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        label = GetComponentInChildren<TMP_Text>();
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

        if (label != null)
        {
            label.text = id + "\n" + temperature.ToString("F1") + " C\n" + state;
        }
    }
}