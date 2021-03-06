using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class SimpleLeaderboardManager : MonoBehaviour
{
    [SerializeField] private LeaderboardType _leaderboardType;
    [SerializeField] private GameObject _rowPrefab;
    [SerializeField] private Transform _tableContent;

    private SimpleFirebaseManager firebaseManager;

    private void Start()
    {
        firebaseManager = FindObjectOfType<SimpleFirebaseManager>();

        UpdateLeaderboardUI();
    }

    // Updates the Hologram Leader UI
    public async void UpdateLeaderboardUI()
    {
        //var leaderboardList = await firebaseManager.GetLeaderboard();
        int rankCounter = 1;

        //Clear all leaderboard entries in UI
        foreach (Transform item in _tableContent)
        {
            Destroy(item.gameObject);
        }

        switch (_leaderboardType)
        {
            // If leaderboard type is Skiing
            case LeaderboardType.Skiing:
                {
                    var leaderboardList = await firebaseManager.GetSkiiLeaderboard();

                    foreach (SkiingLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);

                        //Create prefabs in the position of tableContents
                        GameObject entry = Instantiate(_rowPrefab, _tableContent);
                        TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();

                        TimeSpan t = TimeSpan.FromSeconds(lb.fastestTime);

                        leaderboardDetails[0].text = rankCounter.ToString();
                        leaderboardDetails[1].text = lb.displayname;
                        leaderboardDetails[2].text = t.Minutes.ToString() + ":" + t.Seconds.ToString();

                        rankCounter++;
                    }
                }

                break;

            // If leaderboard type is Outdoor Shooting
            case LeaderboardType.OutdoorShooting:
                {
                    var leaderboardList = await firebaseManager.GetOutdoorLeaderboard();
                    leaderboardList.Reverse(); // Reverse list

                    foreach (OutdoorLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.outdoorPoints);

                        //Create prefabs in the position of tableContents
                        GameObject entry = Instantiate(_rowPrefab, _tableContent);
                        TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();
                        leaderboardDetails[0].text = rankCounter.ToString();
                        leaderboardDetails[1].text = lb.displayname;
                        leaderboardDetails[2].text = lb.outdoorPoints.ToString();

                        rankCounter++;
                    }
                }

                break;

            // If leaderboard type is Indoor Shooting
            case LeaderboardType.IndoorShooting:
                {
                    var leaderboardList = await firebaseManager.GetIndoorLeaderboard();
                    leaderboardList.Reverse(); // Reverse list

                    foreach (IndoorLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.indoorPoints);

                        //Create prefabs in the position of tableContents
                        GameObject entry = Instantiate(_rowPrefab, _tableContent);
                        TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();
                        leaderboardDetails[0].text = rankCounter.ToString();
                        leaderboardDetails[1].text = lb.displayname;
                        leaderboardDetails[2].text = lb.indoorPoints.ToString();

                        rankCounter++;
                    }
                }

                break;
        }
    }
    
    public void MainMenu()
    {
        AudioClipManager.PlaySound("button");
        SceneManager.LoadScene(1);
    }
}

public enum LeaderboardType
{
    Skiing,
    OutdoorShooting,
    IndoorShooting
}