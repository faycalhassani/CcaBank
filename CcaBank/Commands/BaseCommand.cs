using CcaBank.Security;

namespace CcaBank.Commands
{
    /// <summary>
    /// classe de base pour toute les commandes
    /// Elle implemente les proprietes et les fonction que toute les commandes doivent partager
    /// </summary>
    abstract class BaseCommand : ICommand
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public string GetId()
        {
            return Id;
        }

        /// <summary>
        /// Test a afficher dans le menu
        /// </summary>
        /// <returns></returns>
        public string DisplayText()
        {
            // string.Concat sert a concatener des chaines de caractere
            return string.Concat(Id, " - ", Description); // equivalent a : Id + " - " + Description
        }

        public abstract bool Run(Session session);
    }
}
