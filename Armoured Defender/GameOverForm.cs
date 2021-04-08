using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Armoured_Defender.Entities.Utils;

namespace Armoured_Defender
{
    public partial class GameOverForm : Form
    {
        //This is used to play music 
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        //arrUserscores will store the top 10 scores 
        public static string[] arrUserscores;
        //arrUsernames will sore the usernames of the people that got the top 10 scores  
        public static string[] arrUsernames;


        //This is the path of the text file that stores all the top 10 usernames
        public static string sUsernamesFilePath = "..\\..\\Saved Data -- DO NOT EDIT\\usernames.txt";
        //This is the path of the text file that stores all the top 10 scores
        public static string sUserscoresFilePath = "..\\..\\Saved Data -- DO NOT EDIT\\userscores.txt";

        public GameOverForm()
        {
            InitializeComponent();
            //Identifies the location of the wav file that is to be played 
            player.SoundLocation = "..\\..\\Resources\\Music and Sounds\\Dnj Music - Game Over No Copyright Music Free.wav";
        }

        private void GameOverForm_Load(object sender, EventArgs e)
        {
            //The wav file will play each time this form is loaded 
            player.Load();
            player.Play();

            //Will store the userNames of the players in an array
            arrUsernames = File.ReadAllLines(sUsernamesFilePath);
            //Will store the leader board scores in this array 
            arrUserscores = File.ReadAllLines(sUserscoresFilePath);


            //Checks to see if the user is in the top 10 range 
            //If the user is in the top 10 range then they are shown labels and textboxes that are usually hidden 
            if (EntityManager.score > Int32.Parse(arrUserscores[9]))
            {
                lblGetName.Show();
                lblLeadConfirm.Show();
                txbname.Show();
                btnEnter.Show();
            }
            //If the user is not in the top 10 then the special labels and textboxes are hidden 
            else
            {
                lblGetName.Hide();
                lblLeadConfirm.Hide();
                txbname.Hide();
                btnEnter.Hide();
            }

            //Shows the score of the user 
            lblScore.Text = "Your Score Was: " + EntityManager.score;

        }


        //When the user presses the enter button the following code is run
        private void btnEnter_Click(object sender, EventArgs e)
        {
            //If the user did not enter a 3 letter string the user is shown an error
            if (txbname.Text.Length != 3)
            {
                MessageBox.Show("Please enter a username that is 3 characters long",
                    "USERNAME MUST BE 3 CHARACTERS LONG", MessageBoxButtons.OK);
            }
            //If the user entered a 3 letter string that string is stored in userName --> userName is a global variable 
            else
            {
                string userName = txbname.Text;
                string userScore = EntityManager.score.ToString();

                //Replaces the 10 name and score with the current name and score of the user 
                arrUserscores[9] = userScore;
                arrUsernames[9] = userName;

                //Sorts the score and user name array in decreasing order 
                sortArray();

                //Makes a message string for the name and the score --> Pass that into the files to be overwritten
                string usernamesMessage = "";
                string userscoresMessage = "";
                


                //Puts all the usernames into a string that can be used to rewrite the leader board statistics on the file 
                foreach (string username in arrUsernames)
                {
                    usernamesMessage += username + "\n";
                }

                //Puts all the scores into a string that can be used to rewrite the leader board statistics on the file 
                foreach (string score in arrUserscores)
                {
                    userscoresMessage += score + "\n";
                }


                //Rewrites the scores and names to the corresponding files
                File.WriteAllText(sUsernamesFilePath, usernamesMessage);
                File.WriteAllText(sUserscoresFilePath, userscoresMessage);

                // After user enters score, go back to the start screen
                player.Stop();

                new StartForm().Show();
                Hide();
            }
        }




        /**
         * This method sorts the two arrays that store the leader board info in decreasing order
         * This method takes two array parameters --> The array with all the top 10 usernames and the array with all the top 10 scores
         * This method uses a modified bubble sort to sort the array with the scores --> It sorts the name array at the same time
         *
         * When this method has finished running both arrays will be sorted and they can easily be written to the file 
         */
        public void sortArray()
        {
            //Stores the length of the array in a variable --> In order to be more efficient 
            int lengthOfArr = arrUsernames.Length;
            //Will temporarily hold an element from the top 10 scores array 
            string tempScore = "";
            //Will temporarily hold an element from the top 10 usernames array 
            string tempName = "";


            //The following is the modified bubble sort that sorts the two arrays 

            //Iterates through all the top 10 scores --> This is stored in arrUserscores
            for (int i = 0; i < lengthOfArr; i++)
            {
                //Compares the score at index i with the scores that come after it in the array 
                for (int j = i; j < lengthOfArr; j++)
                {
                    //If the element at index i is less than the element at j then the scores switch places 
                    //The elements in the array are of string type and so they must be converted to an integer before comparing 
                    if (Int32.Parse(arrUserscores[i]) < Int32.Parse(arrUserscores[j]))
                    {
                        //Temporarily stores the score that had the smaller value 
                        tempScore = arrUserscores[i];
                        //Temporarily stores the username that is associated with the smaller score 
                        tempName = arrUsernames[i];

                        //The larger score value is moved to the spot of the smaller score --> Sorting the array in a decreasing fashion 
                        arrUserscores[i] = arrUserscores[j];
                        //Moves the username that is associated with the larger score to the same index that the larger score was moved to 
                        arrUsernames[i] = arrUsernames[j];

                        //The smaller score value is moved to the previous position of the greater score --> this is sorting the array in a decreasing fashion 
                        arrUserscores[j] = tempScore;
                        //Moves the username that is associated with the smaller score to the same index that the smaller score was moved to
                        arrUsernames[j] = tempName;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Terminates all operations 
            Application.Exit();
        }

        private void btnGoToMain_Click(object sender, EventArgs e)
        {
            player.Stop();

            ////Shows the main menu form and hides the current form 
            new StartForm().Show();
            Hide();
        }
    }
}
