using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlarmUI : MonoBehaviour
{
    [SerializeField] private TMP_Text alarmText;

    private readonly HashSet<string> errorIds = new HashSet<string>();

    private void Awake()
    {
        Hide();
    }

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
        if (machine.State == MachineState.Error)
        {
            errorIds.Add(machine.Id);
        }
        else
        {
            errorIds.Remove(machine.Id);
        }

        Refresh();
    }

    private void Refresh()
    {
        if (errorIds.Count == 0)
        {
            Hide();
        }
        else
        {
            Show("ALARM: " + string.Join(", ", errorIds) + " Error");
        }
    }

    public void Show(string message)
    {
        alarmText.text = message;
        alarmText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        alarmText.gameObject.SetActive(false);
    }
}