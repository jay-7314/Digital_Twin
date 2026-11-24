using TMPro;
using UnityEngine;

public class AlarmUI : MonoBehaviour
{
    [SerializeField] private TMP_Text alarmText;

    private string alarmedId;

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
            alarmedId = machine.Id;
            Show("ALARM: " + machine.Id + " Error");
        }
        else if (machine.Id == alarmedId)
        {
            alarmedId = null;
            Hide();
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