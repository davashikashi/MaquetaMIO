using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para manejar las escenas

public class ReLoadScene : MonoBehaviour
{
    // Este método se llamará para reiniciar el juego
    public void RestartGame()
    {
        // Obtén el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Carga la escena actual nuevamente
        SceneManager.LoadScene(currentSceneName);
    }
}

