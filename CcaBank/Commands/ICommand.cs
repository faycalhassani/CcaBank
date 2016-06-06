using CcaBank.Security;

namespace CcaBank.Commands
{
    interface ICommand
    {
        string GetId();
        string DisplayText();
        bool Run(Session session);
    }
}
