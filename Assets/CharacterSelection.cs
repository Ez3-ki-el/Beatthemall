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

        // Associer les inputs aux déplacements de curseurs
        player1MoveAction.performed += ctx => MoveCursor(ref player1Index, ctx.ReadValue<Vector2>(), player1Cursor, player2Index);
        player2MoveAction.performed += ctx => MoveCursor(ref player2Index, ctx.ReadValue<Vector2>(), player2Cursor, player1Index);
    }

    private void MoveCursor(ref int index, Vector2 direction, GameObject cursor, int otherPlayerIndex)
    {
        // Calculer la direction du mouvement
        if (direction.x > 0.5f) index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // Droite
        else if (direction.x < -0.5f) index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // Gauche

        // Vérifier si le curseur du joueur se chevauche avec celui de l'autre joueur
        if (index == otherPlayerIndex)
        {
            // Si les deux curseurs sont sur le même personnage, on déplace automatiquement
            if (direction.x > 0.5f) // Si le mouvement est vers la droite
            {
                index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // Déplacer vers la droite
            }
            else if (direction.x < -0.5f) // Si le mouvement est vers la gauche
            {
                index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // Déplacer vers la gauche
            }
        }

        // Bloquer le déplacement si le bouton est déjà pris par l'autre joueur
        if (IsButtonOccupied(index, otherPlayerIndex))
        {
            // Si le bouton est occupé, déplacer le curseur sur un autre bouton libre
            if (direction.x > 0.5f) index = Mathf.Clamp(index + 1, 0, characterButtons.Length - 1); // Déplacer vers la droite
            else if (direction.x < -0.5f) index = Mathf.Clamp(index - 1, 0, characterButtons.Length - 1); // Déplacer vers la gauche
        }

        // Récupérer la position du bouton sélectionné
        RectTransform buttonRect = characterButtons[index].GetComponent<RectTransform>();

        // Déplacer le curseur au-dessus du bouton en ajustant la position verticale
        Vector3 newPosition = buttonRect.position;
        newPosition.y += buttonRect.rect.height / 2 + 80f; // Ajuster la position avec la hauteur du bouton

        cursor.transform.position = newPosition;

        // Sélectionner le bouton sous le curseur pour changer son état visuel
        SelectButton(index);
    }

    // Sélectionner un bouton et forcer son état sélectionné (highlight)
    private void SelectButton(int index)
    {
        Button button = characterButtons[index].GetComponent<Button>();

        // Forcer la sélection et l'activation du bouton
        if (button != null)
        {
            // Désactiver la sélection du bouton pour le joueur opposé
            if (player1Cursor.activeSelf && button != characterButtons[player1Index].GetComponent<Button>())
            {
                button.OnDeselect(null);  // Désélectionner les autres boutons
            }
            if (player2Cursor.activeSelf && button != characterButtons[player2Index].GetComponent<Button>())
            {
                button.OnDeselect(null);  // Désélectionner les autres boutons
            }

            // Mettre en surbrillance le bouton sélectionné
            button.Select();
        }
    }

    // Vérifier si un bouton est déjà occupé par l'autre joueur
    private static bool IsButtonOccupied(int buttonIndex, int otherPlayerIndex)
    {
        return buttonIndex == otherPlayerIndex;
    }

  
    // Appelée quand un joueur confirme sa sélection
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
