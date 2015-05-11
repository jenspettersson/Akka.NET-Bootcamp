using Akka.Actor;

namespace WinTail
{
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();

            var consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

            Props validationProps = Props.Create(() => new ValidationActor(consoleWriterActor));

            var validationActor = MyActorSystem.ActorOf(validationProps, "validationActor");

            Props consoleReaderProps = Props.Create(() => new ConsoleReaderActor(validationActor));

            var consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            MyActorSystem.AwaitTermination();
        }
    }
}
