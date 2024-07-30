using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
   
   
        private const string TutorialShownKey = "TutorialShown";
        [SerializeField] GameObject tutorialUI;
        [SerializeField] GameObject howtoPlayUI;
    [SerializeField] GameObject story;

        void Start()
        {
        story.SetActive(false);
        howtoPlayUI.SetActive(false);
            if (IsFirstTime())
            {
                ShowTutorial();
            }
        }

        private bool IsFirstTime()
        {
            return PlayerPrefs.GetInt(TutorialShownKey, 0) == 0;
        }

    public void howtoplayWindow()
    {
        howtoPlayUI.SetActive(true);
    }
    public void storyview()
    {
        story.gameObject.SetActive(true);
    }
    public void endstory()
    {
        story.SetActive(false);
    }

       

        private void ShowTutorial()
        {
            // Enable your tutorial UI elements
            tutorialUI.SetActive(true);

            // Mark the tutorial as shown
            PlayerPrefs.SetInt(TutorialShownKey, 1);
            PlayerPrefs.Save();
        }

        public void HideTutorial()
        {
            // Disable your tutorial UI elements
            tutorialUI.SetActive(false);
        }
    public void howtoplayback()
    {
        howtoPlayUI?.SetActive(false);
    }
   public void quit()
    {
        Application.Quit(); 
    }   
}
