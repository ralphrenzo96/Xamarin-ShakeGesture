using System;
namespace cprapp.Helpers.ShakeService
{
    public interface IShakeService
    {
        void GetSpeed();
        void Stop();
        event EventHandler<IShakeServiceEventArgs> speedObtained;
    }

    public interface IShakeServiceEventArgs
    {
        double speed { get; set; }
    }
}
