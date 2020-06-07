using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GenerateLobbyCode : MonoBehaviour
{
    [SerializeField] [Range(4, 16)] private int codeLength = 6;
    //[SerializeField] bool avoidAmbiguousChars = false;

    private string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private string numbers = "0123456789";

    private StringBuilder sb;
    private string lobbyCode = null;

    private void Awake() => sb = new StringBuilder(codeLength);

    public void GenerateCode()
    {
        sb.Clear();

        for (int i = 0; i < codeLength; i++)
        {
            char c;

            // Generate a random character
            int j = Random.Range(0, 3);

            // Get a number, lowercase or uppercase character
            if (j == 0)
            {
                c = alphabet[Random.Range(0, alphabet.Length)];
            } else if (j == 1)
            {
                c = alphabet[Random.Range(0, alphabet.Length)];
                c = char.ToUpper(c);
            } else
            {
                c = numbers[Random.Range(0, numbers.Length)];
            }

            // Add the character to the string builder
            sb.Append(c);


            /* Is not working yet!
            
            // If we cannot have ambiguous characters .. 
            if (avoidAmbiguousChars)
            {
                // .. then check if the new one is already in the string
                for (int z = 0; z < sb.Length -1; z++)
                {
                    char character = sb[z];

                    // If the character is already in the lobby code .. 
                    if (c == character)
                    {
                        Debug.Log(character + " is already in the line");

                        // .. then remove the character from the stringbuilder
                        sb.Remove(sb.Length, 1);
                        // Iterate an extra time over the for loop to make up for this character
                        i--;
                    }
                }
            }*/
        }

        lobbyCode = sb.ToString();
    }

    public string GetLobbyCode()
    {
        return lobbyCode;
    }
}
