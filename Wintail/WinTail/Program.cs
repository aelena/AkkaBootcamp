namespace WinTail
{
    using System;

    using Akka.Actor;

    class Program
    {

        private static ActorSystem wintailActorSystem;

        static void Main(string[] args)
        {
            wintailActorSystem = ActorSystem.Create("WintailActorSys");

            PrintInstructions();

            CreateActors();

            wintailActorSystem.WhenTerminated.Wait();
        }

        private static void CreateActors()
        {
            CreateReaderActor();
        }
        
        private static void CreateReaderActor() =>
            wintailActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(CreateWriterActor())), "consoleReader").Tell(("start"));


        private static IActorRef CreateWriterActor() =>
            wintailActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()), "consoleWriter");


        private static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" red ");
            Console.ResetColor();
            Console.Write(" and others will appear as");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}
