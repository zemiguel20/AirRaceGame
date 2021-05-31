using AirRace.Core;
using AirRace.Core.GameEvent;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace.Race
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private List<Goal> goals;
        [SerializeField] private GameEvent PathFinished;

        private int currentGoalIndex;

        public void StartPath()
        {
            GameLogger.Debug("Path Started");

            if (goals.Count == 0)
            {
                EndPath();
                return;
            }
            else
            {
                //Turns off goals
                foreach (Goal goal in goals)
                {
                    goal.gameObject.SetActive(false);
                }
                currentGoalIndex = 0;
                SetGoalStatus(currentGoalIndex, true);
            }
        }

        public void ChangeActiveGoal()
        {
            if (currentGoalIndex < goals.Count)
            {
                SetGoalStatus(currentGoalIndex, false);
                currentGoalIndex++;
                GameLogger.Debug("Goal passed! Num of goals passed: " + currentGoalIndex);
            }

            if (currentGoalIndex == goals.Count)
            {
                EndPath();
            }
            else
            {
                SetGoalStatus(currentGoalIndex, true);
            }
        }

        private void SetGoalStatus(int index, bool status)
        {
            goals[index].gameObject.SetActive(status);
        }

        private void EndPath()
        {
            GameLogger.Debug("Path Finished");
            PathFinished.Raise();
        }

    }
}