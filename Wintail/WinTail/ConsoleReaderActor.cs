
namespace WinTail
{

    using Akka.Actor;

    using System;


    internal class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
        public const string continueCommand = "continue";

        private readonly IActorRef _consoleWriterActor;

        /// <summary>
        /// receives a reference to the ohter actor this actor has to communicate with.
        /// </summary>
        /// <param name="consoleWriterActor"></param>
        public ConsoleReaderActor(IActorRef consoleWriterActor) => this._consoleWriterActor = consoleWriterActor;

        /// <summary>
        /// Defines what to do upon receiving a message.
        /// </summary>
        /// <param name="message"></param>
        protected override void OnReceive(object message)
        {
            var consoleInput = Console.ReadLine();
            if(!String.IsNullOrEmpty(consoleInput) && String.Equals(consoleInput, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // shut down the system via the Akka Context
                Context.System.Terminate();
                return;
            }

            // else tell the other actor
            this._consoleWriterActor.Tell(consoleInput);

            Self.Tell(continueCommand);
        }
    }
}