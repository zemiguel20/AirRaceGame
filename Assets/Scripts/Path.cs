using AirRace.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace.Race
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _goals;

        private int _currentGoalIndex;

        public void Initialize()
        {
            //Turns off goals
            foreach (GameObject goal in _goals)
            {
                goal.SetActive(false);
            }

            _currentGoalIndex = 0;
            _goals[_currentGoalIndex].SetActive(true);
        }

        public void NextGoal()
        {
            if (_currentGoalIndex < _goals.Count)
            {
                _goals[_currentGoalIndex].SetActive(false);
                _currentGoalIndex++;
            }

            if (_currentGoalIndex < _goals.Count)
            {
                _goals[_currentGoalIndex].SetActive(true);
            }
        }

        public bool IsFinished()
        {
            return _currentGoalIndex >= _goals.Count;
        }

        public GameObject GetCurrentGoal()
        {
            if (_currentGoalIndex < _goals.Count)
            {
                return _goals[_currentGoalIndex];
            }
            else
            {
                // If race finished just return last one
                return _goals[_goals.Count - 1];
            }
        }
    }
}