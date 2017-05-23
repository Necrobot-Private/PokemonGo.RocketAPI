﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Helpers
{
    public class KillSwitchTask
    {
        private CancellationTokenSource _killswitchCancellation;

        private Task _killswitchTask;

        private Client _client;

        internal KillSwitchTask(Client client)
        {
            _client = client;
        }

        /// <summary>
        /// Checks every once in a while if we need to activate killswitch.
        /// </summary>

        private async Task CheckKillSwitch(TaskCompletionSource<bool> firstCheckCompleted)
        {
            while (!_killswitchCancellation.IsCancellationRequested)
            {

                Version version = Client.GetMinimumRequiredVersionFromUrl();
                if (version != null)
                    _client.MinimumClientVersion = version;
            
                // after first check, signal as complete
                firstCheckCompleted?.TrySetResult(true);
                firstCheckCompleted = null;

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(30), _killswitchCancellation.Token).ConfigureAwait(false); // Check every 30s
                }
                // cancelled
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
        
        internal async Task Start()
        {
            if (_killswitchTask != null)
            {
                // Task is already running, so just return.
                return;
            }

            var firstCheckCompleted = new TaskCompletionSource<bool>();
            _killswitchCancellation = new CancellationTokenSource();
            _killswitchTask = CheckKillSwitch(firstCheckCompleted);

            // wait for first check to complete
            await firstCheckCompleted.Task.ConfigureAwait(false);
        }

        internal void Stop()
        {
            _killswitchCancellation?.Cancel();
            _killswitchTask = null;
        }
    }
}
