using AirRace.Core;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace.Race
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private List<Goal> _goals;
        public List<Goal> Goals => new List<Goal>(_goals);

        private int currentGoalIndex;

        public void StartPath()
        {
            GameLogger.Debug("Path Started");

            //Turns off goals
            foreach (Goal goal in _goals)
            {
                goal.gameObject.SetActive(false);
            }

            currentGoalIndex = 0;
            SetGoalStatus(currentGoalIndex, true);
        }

        public void ChangeActiveGoal()
        {
            if (currentGoalIndex < _goals.Count)
            {
                SetGoalStatus(currentGoalIndex, false);
                currentGoalIndex++;
                GameLogger.Debug("Goal passed! Num of goals passed: " + currentGoalIndex);
            }

            if (currentGoalIndex < _goals.Count)
            {
                SetGoalStatus(currentGoalIndex, true);
            }
        }

        private void SetGoalStatus(int index, bool status)
        {
            _goals[index].gameObject.SetActive(status);
        }

        public bool IsFinished()
        {
            return currentGoalIndex >= _goals.Count;
        }

        public Goal GetCurrentGoal()
        {
            if (currentGoalIndex < _goals.Count)
            {
                return _goals[currentGoalIndex];
            }
            else
            {
                // If race finished just return last one
                return _goals[_goals.Count - 1];
            }
        }
    }
}