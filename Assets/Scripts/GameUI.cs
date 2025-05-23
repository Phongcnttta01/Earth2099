using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
   [SerializeField] private GameManager gm;

   public void StartGame()
   {
      gm.StartGame();
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   public void PauseGameQuit()
   {
      gm.MainMenu();
   }
   public void ContinueGame()
   {
      gm.ResumeGame();
   }

   public void WeaponChoose()
   {
      gm.WeaponGame();
   }

   public void MainMenu()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
       
   }

   public void Info()
   {
      gm.Info();
   }
   public void ChooseGun()
   {
      gm.ChooseGun();
      gm.StartGame();
   }

   public void ChooseSword()
   {
      gm.ChooseSword();
      gm.StartGame();
   }
}
