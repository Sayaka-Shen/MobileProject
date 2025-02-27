using System;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "AchievementManager", menuName = "ScriptableObjects/AchievementManager", order = 2)]
public class AchievementManager : ScriptableObject
{
    [SerializeField] Achievement[] achievements;

    public Achievement GetAchievementByName(string name)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.Name == name) return achievement;
        }
        throw new ArgumentNullException("No achiement named "+name+" found.");
    }

    public void UnlockAchievementByName(string name)
    {
        Achievement achievement = GetAchievementByName(name);
        // unlock achievement (achievement ID)
        PlayGamesPlatform.Instance.UnlockAchievement(achievement.ID, (bool success) => {
            // handle success or failure
            if (success) Debug.Log("Succes");
            else Debug.Log("Failed");
        });
        achievement.Completed = true;
    }
    public void IncrementStepAchievementByName(string name, int step)
    {
        Achievement achievement = GetAchievementByName(name);
        // increment achievement (achievement ID) by 1 steps
        PlayGamesPlatform.Instance.IncrementAchievement(achievement.ID, step, (bool success) => {
            // handle success or failure
            if (success) Debug.Log("Succes");
            else Debug.Log("Failed");
        });
        achievement.Completed = true;
    }
}

public class Achievement
{
    [SerializeField] string _name;
    public string Name { get { return _name; } }
    [SerializeField] string _id;
    public string ID { get { return _id; } }
    [SerializeField] string _description;
    public string Description { get { return _description; } }
    [SerializeField] Sprite _sprite;
    public Sprite Sprite { get { return _sprite; } }
    [SerializeField] bool _completed;
    public bool Completed {  get { return _completed; } set { _completed = value; } }
}