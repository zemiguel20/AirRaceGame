using Assets.Scripts.GameLogger;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    [SerializeField] private FloatVariable timeVariable;

    private bool active = false;


    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timeVariable.value += Time.deltaTime;
        }
    }

    public void ResetTime()
    {
        GameLogger.Debug("Chronometer Reset");
        timeVariable.value = 0;
    }

    public void StartChrono()
    {
        GameLogger.Debug("Chronometer started");
        active = true;
    }

    public void StopChrono()
    {
        GameLogger.Debug("Chronometer stopped");
        active = false;
    }

}
