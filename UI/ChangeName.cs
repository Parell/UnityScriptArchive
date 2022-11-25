using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Text messageText;

    //[Space]
    //[SerializeField] private PlayerManager playerManager;

    public void ChangeUsername()
    {
        if (nameInputField.text.Length < 3)
        {
            messageText.text = "Name must be between 3 and " + nameInputField.characterLimit + " characters";
            return;
        }

        //if (/*playerManager.playerName == nameInputField.text*/)
        //{
        //    messageText.text = "Name must be different from current name";
        //    return;
        //}

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInputField.text,
        }, result =>
        {
            //playerManager.playerName = nameInputField.text;
            messageText.text = "Name changed";
        }, error =>
        {
            messageText.text = "Please enter a vaild Name";
        });
    }

    public void BackButton()
    {
        messageText.text = default;
        nameInputField.text = default;
        gameObject.SetActive(false);
    }
}