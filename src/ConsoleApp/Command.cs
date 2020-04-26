using System.Threading.Tasks;

namespace ConsoleApp
{
    public abstract class Command
    {
        public abstract Task Execute();
    }
}
