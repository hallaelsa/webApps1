namespace Oblig1theAteam.DependencyInjectionDemo
{
    public class DemoService : IDemoService
    {
        private int count = 0;

        public int Add()
        {
            return ++count;
        }
    }
}
