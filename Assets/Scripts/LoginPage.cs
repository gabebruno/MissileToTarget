using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPage : MonoBehaviour
{
    [SerializeField]     private const string Login = "admin";
    [SerializeField]     private const string Password = "admin";
    [SerializeField]     private InputField userField = null;
    [SerializeField]     private InputField pwdField = null;
    [SerializeField]     private Text message = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Logar()
    {
        string user = userField.text.ToLower().Trim();
        string pwd = pwdField.text.ToLower().Trim();

        if (user == Login && pwd == Password)
        {
            message.CrossFadeAlpha(100f, 0f, false);
            message.color = Color.green;
            message.text = "Login success! \n Loading destroyer missile and first target...";
            StartCoroutine(LoadGame());
        }
        else
        {
            message.CrossFadeAlpha(100f, 2f, false);
            message.color = Color.red;
            message.text = "YOU SHALL NOT PASS!";
            message.CrossFadeAlpha(0f, 2f, false);
            userField.text = "";
            pwdField.text = "";
        }
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("main");
    }
}
