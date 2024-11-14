using System;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;  

public class CharacterSelection : MonoBehaviour
{
    public GameObject player1Cursor;
    public GameObject player2Cursor;
    public GameObject[] characterButtons;
    private int player1Index = 0;
    private int player2Index = 1;
    private bool player1Confirmed = false;
    private bool player2Confirmed = false;

    private InputAction player1MoveAction;
    private InputAction player2MoveAction;

    public PlayerInput player1Input;
    public PlayerInput player2Input;

    private void Awake()
    {
        // Associer chaque PlayerInput aux actions correspondantes
        player1MoveAction = player1Input.actions["Navigate"];
        player2MoveAction = player2Input.actions["Navigate"];

        // Associer les inputs aux d�placements de curseurs
        player1MoveAction.performed += ctx => MoveCursor(ref player1Index, ctx.ReadValue<Vector2>(), player1Cursor, player2Index);
        player2MoveAction.performed += ctx => MoveCursor(ref player2Index, ctx.ReadValue<Vector2>(), player2Cursor, player1Index);
    }

    private void MoveCursor(ref int index, Vector2 direction, GameObject cursor, int otherPlayerIndex)
    {
        // Calculer la direction du mouvement
        if (direction.x > 0.5f) index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // Droite
        else if (direction.x < -0.5f) index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // Gauche

        // V�rifier si le curseur du joueur se chevauche avec celui de l'autre joueur
        if (index == otherPlayerIndex)
        {
            // Si les deux curseurs sont sur le m�me personnage, on d�place automatiquement
            if (direction.x > 0.5f) // Si le mouvement est vers la droite
            {
                index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // D�placer vers la droite
            }
            else if (direction.x < -0.5f) // Si le mouvement est vers la gauche
            {
                index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // D�placer vers la gauche
            }
        }

        // Bloquer le d�placement si le bouton est d�j� pris par l'autre joueur
        if (IsButtonOccupied(index, otherPlayerIndex))
        {
            // Si le bouton est occup�, d�placer le curseur sur un autre bouton libre
            if (direction.x > 0.5f) index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // D�placer vers la droite
            else if (direction.x < -0.5f) index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // D�placer vers la gauche
        }

        // R�cup�rer la position du bouton s�lectionn�
        RectTransform buttonRect = characterButtons[index].GetComponent<RectTransform>();

        // D�placer le curseur au-dessus du bouton en ajustant la position verticale
        Vector3 newPosition = buttonRect.position;
        newPosition.y += buttonRect.rect.height / 2 + 80f; // Ajuster la position avec la hauteur du bouton

        cursor.transform.position = newPosition;

        // S�lectionner le bouton sous le curseur pour changer son �tat visuel
        SelectButton(index);
    }

    // S�lectionner un bouton et forcer son �tat s�lectionn� (highlight)
    private void SelectButton(int index)
    {
        Button button = characterButtons[index].GetComponent<Button>();

        // Forcer la s�lection et l'activation du bouton
        if (button != null)
        {
            // D�sactiver la s�lection du bouton pour le joueur oppos�
            if (player1Cursor.activeSelf && button != characterButtons[player1Index].GetComponent<Button>())
            {
                button.OnDeselect(null);  // D�s�lectionner les autres boutons
            }
            if (player2Cursor.activeSelf && button != characterButtons[player2Index].GetComponent<Button>())
            {
                button.OnDeselect(null);  // D�s�lectionner les autres boutons
            }

            // Mettre en surbrillance le bouton s�lectionn�
            button.Select();
        }
    }

    // V�rifier si un bouton est d�j� occup� par l'autre joueur
    private static bool IsButtonOccupied(int buttonIndex, int otherPlayerIndex)
    {
        return buttonIndex == otherPlayerIndex;
    }

  
    // Appel�e quand un joueur confirme sa s�lection
    public void ConfirmSelection(int numeroPersonnage)
    {
        if (!player1Confirmed && (numeroPersonnage - 1) == player1Index)
        {
            player1Confirmed = true;
            characterButtons[player1Index].GetComponent<Button>().interactable = false;
            player1Cursor.SetActive(false);
            Debug.Log("Le joueur 1 a choisi le personnage " + numeroPersonnage);
        }
        else if (!player2Confirmed && (numeroPersonnage - 1) == player2Index)
        {
            characterButtons[player2Index].GetComponent<Button>().interactable = false;
            player2Confirmed = true;
            player2Cursor.SetActive(false);
            Debug.Log("Le joueur 2 a choisi le personnage " + numeroPersonnage);
        }
         }

}
