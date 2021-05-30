using AirRace.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AirRace.Race
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private List<Goal> goals;
        [SerializeField] private UnityEvent PathFinished;

        private List<Goal> goalsPassed = new List<Goal>();

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

                ActivateNextGoal();
            }
        }

        public void ChangeActiveGoal()
        {


            if (goals.Count > 0)
            {
                RemoveCurrentGoal();
                GameLogger.Debug("Goal passed! Num of goals passed: " + goalsPassed.Count);
            }

            if (goals.Count == 0)
            {
                EndPath();
            }
            else
            {
                ActivateNextGoal();
            }
        }

        private void ActivateNextGoal()
        {
            goals[0].gameObject.SetActive(true);
        }

        private void RemoveCurrentGoal()
        {
            Goal goal = goals[0];
            goal.gameObject.SetActive(false);
            goals.Remove(goal);
            goalsPassed.Add(goal);
        }

        private void EndPath()
        {
            GameLogger.Debug("Path Finished");
            PathFinished.Invoke();
        }

    }
}