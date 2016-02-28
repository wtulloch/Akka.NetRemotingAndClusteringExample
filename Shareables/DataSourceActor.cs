using System;
using System.Threading;
using Akka.Actor;
using Shareables.Messages;
using NLipsum;
using NLipsum.Core;

namespace Shareables
{
    public class DataSourceActor : ReceiveActor
    {
        private readonly int _loopCount;
        private readonly TimeSpan _waitInterval;
        private readonly IActorRef _targetActor;

        public DataSourceActor(int loopCount, TimeSpan waitInterval, IActorRef targetActor)
        {
            _loopCount = loopCount;
            _waitInterval = waitInterval;
            _targetActor = targetActor;

            Become(Waiting);
        }

        private void Waiting()
        {
            Receive<StartGeneratingData>(message => Become(CanGenerateData));
        }

        private void CanGenerateData()
        {
            Receive<StopGeneratingData>(message => Become(Waiting));

            GenerateData();


        }

        private void GenerateData()
        {
            var generator = new LipsumGenerator();
            
            for (int i = 1; i < _loopCount; i++)
            {
                _targetActor.Tell(new DataToBeProcessedMessage(i, generator.RandomWord()));

                Thread.Sleep(_waitInterval);
            }


        }
    }
}