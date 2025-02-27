using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

public class GooglePlayAuthentification : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject obj2;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI debugText; // Champ pour afficher les messages de debogage

    void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            text.text = PlayGamesPlatform.Instance.GetUserId();
            debugText.text = "Connexion reussie : " + PlayGamesPlatform.Instance.GetUserId();
        }
        else
        {
            text.text = "Non connecte";
            debugText.text = "Echec de la connexion : " + status.ToString();
            switch (status)
            {
                case SignInStatus.InternalError:
                    debugText.text += "\nErreur interne lors de la connexion.";
                    break;
                case SignInStatus.Canceled:
                    debugText.text += "\nConnexion annulee par l'utilisateur.";
                    break;
                default:
                    debugText.text += "\nStatut de connexion inconnu.";
                    break;
            }
        }
    }

    #region Instance
    private static GooglePlayAuthentification _instance;

    public static GooglePlayAuthentification Instance { get => _instance; }

    public void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    // Debloquer un achievement
    public void UnlockAchievement(string achievementID)
    {
        GameObject objet = Instantiate(obj);
        objet.transform.position = new Vector3(-8, 0, 0);
        PlayGamesPlatform.Instance.ReportProgress(achievementID, 100.0f, success =>
        {
            if (success)
            {
                debugText.text = "Achievement debloque !";
                GameObject objet = Instantiate(obj2);
                objet.transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject objet = Instantiate(obj);
                objet.transform.position = new Vector3(1, 0, 0);
                debugText.text = "echec du deblocage";
            }
        });
    }

    private void Connect()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    //exemple d'utilisation

    //UnlockAchievement("CgkIj9xxxxxxEAIQAQ"); // Remplace par l'ID de l'achievement

    //exemple pour voir les succes sur un bouton

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }
}