namespace WebApplication2.Classes
{
    public class Atributos
    {
        public int STR  {get; set;}

        public int INT { get; set;}

        public int VIT { get; set;}

        public int AGI { get; set;}  

        public int SORT { get; set;}

        public int CRIT => 4 * (int)(STR + SORT);

        public int VELO => 2 * (int)(AGI);

        public int VIDA => 2 * (int)(VIT);

        public int MANA => 2 * (int)(INT);


    }
}
