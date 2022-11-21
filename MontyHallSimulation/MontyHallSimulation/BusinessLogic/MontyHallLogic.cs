using MontyHallSimulation.Models;

namespace MontyHallSimulation.BusinessLogic
{
    public  class MontyHallLogic
    {
        /// <summary>
        /// Play given no of games and return the wins and loss count
        /// </summary>
        /// <param name="NoChances"></param>
        /// <param name="SwitchSelection"></param>
        /// <returns>the wins and loss count</returns>
        public virtual Result PlayGames(int NoChances, bool SwitchSelection)
        {
            Random random = new Random();
            int wins = 0;
            int losses = 0;

            for (int i = 0; i < NoChances; i++)            {
                
                bool changeDoor = SwitchSelection;

                // calculate whether or not the contestant wins the Car               
                bool result = IsAWin(random.Next(3), changeDoor,
                                            random.Next(3), random.Next(1));

                if (result)
                    wins++;
                else
                    losses++;
            }

            Result result1 = new Result();
            result1.NoOfChances = NoChances;
            result1.Wins = wins;
            result1.Loss = losses;

            return result1;
        }
        /// <summary>
        /// Return whether the player won or not with given pick and choice
        /// </summary>
        /// <param name="pickedDoor"></param>
        /// <param name="changeDoor"></param>
        /// <param name="carDoor"></param>
        /// <param name="goatDoorToRemove"></param>
        /// <returns>Won or Not</returns>
        public virtual bool IsAWin(int pickedDoor, bool changeDoor,
                                      int carDoor, int goatDoorToRemove)
        {
            bool win = false;

            // randomly remove one of the *goat* doors except contestant door,
            
            int leftGoat = 0;
            int rightGoat = 2;
            switch (pickedDoor)
            {
                case 0: leftGoat = 1; rightGoat = 2; break;
                case 1: leftGoat = 0; rightGoat = 2; break;
                case 2: leftGoat = 0; rightGoat = 1; break;
            }

            int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;

            // would the contestant win with the switch or the stay?
            if (!changeDoor)
            {
                // not changing the initially picked door
                win = carDoor == pickedDoor;
            }
            else
            {
                // changing picked door to the other door remaining
                win = carDoor != keepGoat;
            }

            return win;
        }
    }
}
