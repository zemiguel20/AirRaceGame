using AirRace.Core;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace.Race
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private List<Goal> goals;

        private int currentGoalIndex;

        public void StartPath()
        {
            GameLogger.Debug("Path Started");

            //Turns off goals
            foreach (Goal goal in goals)
            {
                goal.gameObject.SetActive(false);
            }

            currentGoalIndex = 0;
            SetGoalStatus(currentGoalIndex, true);
        }

        public void ChangeActiveGoal()
        {
            if (currentGoalIndex < goals.Count)
            {
                SetGoalStatus(currentGoalIndex, false);
                currentGoalIndex++;
                GameLogger.Debug("Goal passed! Num of goals passed: " + currentGoalIndex);
            }

            if (currentGoalIndex < goals.Count)
            {
                SetGoalStatus(currentGoalIndex, true);
            }
        }

        private void SetGoalStatus(int index, bool status)
        {
            goals[index].gameObject.SetActive(status);
        }

        public bool IsFinished()
        {
            return currentGoalIndex >= goals.Count;
        }
    }
}