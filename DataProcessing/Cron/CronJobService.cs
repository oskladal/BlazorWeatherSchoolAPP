using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Cronos;

public abstract class CronJobService : IHostedService, IDisposable
{
    private System.Timers.Timer _timer;
    private readonly CronExpression _expression;
    private readonly TimeZoneInfo _timeZoneInfo;

    protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo)
    {
        // Nastavení CRONU 
        _expression = CronExpression.Parse(cronExpression);
        _timeZoneInfo = timeZoneInfo;
    }

    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        // Při startu programu
        await ScheduleJob(cancellationToken);
    }

    protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
    {   //naplánovnání jobu 
        var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
        if (next.HasValue)
        {
            var delay = next.Value.AddSeconds(15) - DateTimeOffset.Now;
            if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
            {
                await ScheduleJob(cancellationToken);
            }
            _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            _timer.Elapsed += async (sender, args) =>
            {
                _timer.Dispose();  // reset and dispose timer
                _timer = null;
                // Timer dojel
                if (!cancellationToken.IsCancellationRequested)
                {
                    // Spustí job
                    await DoWork(cancellationToken);
                }

                if (!cancellationToken.IsCancellationRequested)
                {
                    // Naplánuje novej timer
                    await ScheduleJob(cancellationToken);    // reschedule next
                }
            };
            _timer.Start();
        }
        await Task.CompletedTask;
    }

    public virtual async Task DoWork(CancellationToken cancellationToken)
    {
        await Task.Delay(5000, cancellationToken);  // do the work
    }

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Stop();
        await Task.CompletedTask;
    }

    public virtual void Dispose()
    {
        _timer?.Dispose();
    }
}

