using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostPokemon : MonoBehaviour
{
    public void Post()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        Pokemon pp = new Pokemon() { atk = 1, def = 2, hp = 3, name = "daddy", type = 0 };

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>(); //https://docs.unity3d.com/Manual/UnityWebRequest-SendingForm.html
        formData.Add(new MultipartFormDataSection(GetPokeman.SerializePokemon(pp))); //stuff that gose in parameter?

        UnityWebRequest www = UnityWebRequest.Post("https://localhost:44307/api/pokemon", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
