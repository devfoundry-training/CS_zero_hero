using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{
    using System;


    namespace CustomExceptionDemo
    {
        /// <summary>
        /// Represents errors that occur when a vehicle operation is invalid.
        /// </summary>
        public class InvalidVehicleOperationException : Exception
        {
            /// <summary>
            /// Parameterless constructor.
            /// </summary>
            public InvalidVehicleOperationException()
            {
            }

            /// <summary>
            /// Constructs with a custom message.
            /// </summary>
            public InvalidVehicleOperationException(string message)
                : base(message)
            {
            }

            /// <summary>
            /// Constructs with a custom message and an inner exception for chaining.
            /// </summary>
            public InvalidVehicleOperationException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }

        public class Vehicle
        {
            public bool EngineRunning { get; private set; }

            public void StartEngine()
            {
                if (EngineRunning)
                {
                    // Throw our custom exception when someone tries to start an already-running engine
                    throw new InvalidVehicleOperationException("Engine is already running!");
                }
                EngineRunning = true;
                Console.WriteLine("Engine started.");
            }

            public void StopEngine()
            {
                if (!EngineRunning)
                {
                    // Demonstrate exception chaining: wrap a low-level InvalidOperationException
                    var lowLevelEx = new InvalidOperationException("Engine was not running.");
                    throw new InvalidVehicleOperationException(
                        "Cannot stop engine because it isn't running.", lowLevelEx
                    );
                }
                EngineRunning = false;
                Console.WriteLine("Engine stopped.");
            }
        }
    }
}
     